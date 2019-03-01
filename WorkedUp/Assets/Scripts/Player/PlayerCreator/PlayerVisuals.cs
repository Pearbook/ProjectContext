using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{

    public GameObject FacePivot;
    public GameObject HairPivot;

    public void Start()
    {
        LoadFace();
        LoadHair();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            PlayerPrefs.DeleteAll();
    }

    public void LoadFace()
    {
        if (FacePivot.transform.childCount > 0)
            Destroy(FacePivot.transform.GetChild(0).gameObject);

        if (PlayerPrefs.HasKey("FacePrefabName"))
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("Player/Faces/" + PlayerPrefs.GetString("FacePrefabName")), FacePivot.transform.position, Quaternion.identity);
            obj.transform.parent = FacePivot.transform;
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("Player/Faces/obj_face01"), FacePivot.transform.position, Quaternion.identity);
            obj.transform.parent = FacePivot.transform;
        }
    }

    public void LoadHair()
    {
        if (HairPivot.transform.childCount > 0)
            Destroy(HairPivot.transform.GetChild(0).gameObject);

        if (PlayerPrefs.HasKey("HairPrefabName"))
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("Player/Hair/" + PlayerPrefs.GetString("HairPrefabName")), HairPivot.transform.position, Quaternion.identity);
            obj.transform.parent = HairPivot.transform;

            if (PlayerPrefs.HasKey("HairColorR"))
                obj.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(PlayerPrefs.GetFloat("HairColorR"), PlayerPrefs.GetFloat("HairColorG"), PlayerPrefs.GetFloat("HairColorB"));
        }
        else
        {
            GameObject obj = (GameObject)Instantiate(Resources.Load("Player/Hair/obj_hair01"), HairPivot.transform.position, Quaternion.identity);
            obj.transform.parent = HairPivot.transform;

            if (PlayerPrefs.HasKey("HairColorR"))
                obj.transform.GetChild(0).GetComponent<Renderer>().material.color = new Color(PlayerPrefs.GetFloat("HairColorR"), PlayerPrefs.GetFloat("HairColorG"), PlayerPrefs.GetFloat("HairColorB"));
        }
    }
}
