using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MobilePhone : MonoBehaviour
{
    public Vector3 EndPosition;
    public Vector3 EndRotation;

    private Vector3 startPos;
    private Vector3 startRot;
    private Vector3 startScale;

    [HideInInspector]
    public bool isOpen;
    private bool isMoving;

    public float TimeToMove;

    [Header("UI")]
    public CanvasGroup NotificationCanvas;

    public GameObject UICamera;

    [Header("Other")]
    public PingPongScale Scale;

    private void Start()
    {
        startPos = transform.localPosition;
        startRot = transform.localEulerAngles;
        startScale = transform.localScale;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (!isMoving)
            {
                if (isOpen)
                {
                    transform.parent = null;

                    StartCoroutine(MoveToPosition(transform, startPos, TimeToMove));
                    StartCoroutine(ScaleToSize(transform, startScale, TimeToMove));
                    StartCoroutine(RotateTo(transform, startRot, TimeToMove));

                    transform.gameObject.GetComponent<BoxCollider>().enabled = true;
                    SetLayerRecursively(transform.gameObject, LayerMask.NameToLayer("Interactable"));
                }
            }
        }
    }

    public void Interaction()
    {
        StopAllCoroutines();

        print("DOE HET");

        if (!isMoving)
        {
            if (!isOpen)
            {
                transform.parent = UICamera.transform;

                StartCoroutine(MoveToPosition(transform, EndPosition, TimeToMove));
                StartCoroutine(ScaleToSize(transform, Vector3.one, TimeToMove));
                StartCoroutine(RotateTo(transform, EndRotation, TimeToMove));

                transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                SetLayerRecursively(transform.gameObject, LayerMask.NameToLayer("UI"));

                MobileManager.Mobile.HideNotification();
            }
        }
    }

    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.localPosition;
        var t = 0f;

        isMoving = true;

        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.localPosition = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }

        isMoving = false;

        if (!isOpen)
            isOpen = true;
        else
            isOpen = false;
    }

    public IEnumerator ScaleToSize(Transform transform, Vector3 size, float timeToMove)
    {
        var currentSize = transform.localScale;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.localScale = Vector3.Lerp(currentSize, size, t);
            yield return null;
        }
    }

    public IEnumerator RotateTo(Transform transform, Vector3 rotation, float timeToMove)
    {
        var currentRot = transform.localEulerAngles;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.localEulerAngles = Vector3.Lerp(currentRot, rotation, t);
            yield return null;
        }
    }

    public static void SetLayerRecursively(GameObject go, int layerNumber)
    {
        if (go == null) return;
        foreach (Transform trans in go.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}
