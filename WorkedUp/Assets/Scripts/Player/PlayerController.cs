using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Rigidbody PlayerRigid;

    [Header("Movement")]
    public float MovementSpeed;
    public float SpeedModifier = 1;

    public float TurnSpeed;
    public bool ignoreY;

    [Header("Input")]
    public Vector2 InputAxis;

    [Header("Grabbing")]
    public GameObject ObjectPivot;
    public LayerMask GrabMask;

    public Vector3 GrabArea;
    public Vector3 GrabOffset;

    public Vector2 ThrowForce;
    public float ThrowSpin;

    [Header("Interaction")]
    public LayerMask InteractMask;
    public InteractableObject ObjectInRange;
    public bool AllowPickup = true;

    [HideInInspector]
    public bool isInteracting, interactOnce;

    [HideInInspector]
    public bool allowMovement = true;
    [HideInInspector]
    public bool isDisabled = false;
    [HideInInspector]
    public GameObject holdObj;

    void Update()
    {
        if (!isDisabled)
        {
            //TIJDELIJK
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SceneManager.LoadScene(1);

            if (Input.GetKeyDown(KeyCode.Return))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


            if (allowMovement)
                InputAxis = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Input.GetKeyDown(KeyCode.Space))
                PlayerAction();

            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (isInteracting)
                {
                    isInteracting = false;
                    allowMovement = true;
                }
            }

            //rotates rigidbody to face its current velocity public void RotateToVelocity(float turnSpeed, bool ignoreY) {
            Vector3 dir; if (ignoreY) dir = new Vector3(PlayerRigid.velocity.x, 0f, PlayerRigid.velocity.z); else dir = PlayerRigid.velocity;

            if (dir.magnitude > 0.1)
            {
                Quaternion dirQ = Quaternion.LookRotation(dir);
                Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, dir.magnitude * TurnSpeed * Time.deltaTime);
                PlayerRigid.MoveRotation(slerp);
            }
        }
    }

    void FixedUpdate()
    {
        if(allowMovement)
            PlayerRigid.velocity = new Vector3(InputAxis.x * MovementSpeed * SpeedModifier, PlayerRigid.velocity.y, InputAxis.y * MovementSpeed * SpeedModifier);
    }

    void PlayerAction()
    {
        if(CheckForObject() != null)
        {
            if (holdObj == null)
            {
                if (AllowPickup)
                {
                    PickUp();
                    return;
                }
            }
        }

        if (ObjectInRange != null)
        {
            ObjectInRange.Interact();
            return;
        }

        if (holdObj != null)
            Drop();
    }

    void PickUp()
    {
        PlayerManager.Player.GiveHoldItem(CheckForObject().gameObject, false);
    }

    public void Drop()
    {
        holdObj.transform.parent = null;

        PlayerManager.Player.DropHoldItem(ThrowForce, ThrowSpin);
    }

    Collider CheckForObject()
    {
        Collider[] hitColliders = Physics.OverlapBox(ObjectPivot.transform.position + GrabOffset, new Vector3(GrabArea.x/2, GrabArea.y/2, GrabArea.z/2), transform.localRotation, GrabMask);

        if (hitColliders.Length > 0)
            return hitColliders[0];
        else
            return null;
    }

    Collider CheckForInteraction()
    {
        Collider[] hitColliders = Physics.OverlapBox(ObjectPivot.transform.position + GrabOffset, new Vector3(GrabArea.x / 2, GrabArea.y / 2, GrabArea.z / 2), transform.localRotation, InteractMask);

        if (hitColliders.Length > 0)
            return hitColliders[0];
        else
            return null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(ObjectPivot.transform.position + GrabOffset, GrabArea);
    }
}