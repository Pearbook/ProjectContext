using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couch : MonoBehaviour
{
    public GameObject PlayerContainer;

    private bool isOccupied;

    // Disable player control
    // Child player to couch
    // move player to container (and rotate it accordingly)
    // Increase energy over time
    // allow player to leave whenever.

    private void Update()
    {
        if(isOccupied)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                ReleasePlayer();
        }
    }

    public void Interaction()
    {
        if (!isOccupied)
        {
            PlayerManager.Player.DisablePlayer();

            PlayerManager.Player.PlayerObject.transform.parent = PlayerContainer.transform;
            PlayerManager.Player.PlayerObject.transform.localPosition = Vector3.zero;
            PlayerManager.Player.PlayerObject.transform.localEulerAngles = Vector3.zero;

            PlayerManager.Player.PlayerObject.GetComponent<Rigidbody>().isKinematic = true;

            PlayerManager.Player.Stats.IsResting = true;
            isOccupied = true;
        }
    }

    void ReleasePlayer()
    {
        PlayerManager.Player.PlayerObject.transform.parent = null;
        PlayerManager.Player.PlayerObject.transform.position = new Vector3(PlayerContainer.transform.position.x, PlayerContainer.transform.position.y, PlayerContainer.transform.position.z - 1);

        PlayerManager.Player.PlayerObject.GetComponent<Rigidbody>().isKinematic = false;

        PlayerManager.Player.EnablePlayer();
        PlayerManager.Player.Stats.IsResting = false;

        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        isOccupied = false;
    }
}
