using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum MaterialType
{
    Skin,
    Hair,
    Torso,
    Apron,
    Limbs
}

public class PlayerEditor : MonoBehaviour
{
    [Header("Colors")]
    public List<Color> HairColors;
    public List<Color> SkinColors;
    public List<Color> TorsoColors;
    public List<Color> LimbColors;
    public List<Color> ApronColorsOne;
    public List<Color> ApronColorsTwo;

    [Header ("Materials")]
    public Material Skin;
    public Material Hair;
    public Material Torso;
    public Material ApronOne;
    public Material ApronTwo;
    public Material Limbs;

    [Header("Hair")]
    public GameObject HairPivot;
    public List<GameObject> AllHair;
    public int HairIndex;

    private GameObject currentHair;
    private bool hasHair;
    //[HideInInspector]
    public Color currentHairColor;


    [Header("Face")]
    public GameObject FacePivot;
    public List<GameObject> AllFaces;
    public int FaceIndex;

    private GameObject currentFace;
    private bool hasFace;

    public void Start()
    {
        ChangeFace(false);
        ChangeHair(false);

        currentHairColor = HairColors[0];
    }

    public void Update()
    {
        // HAIR
        if (HairIndex > AllHair.Count - 1)
        {
            hasHair = false;
            HairIndex = 0;

            ChangeHair(false);
        }

        if (HairIndex < 0)
        {
            hasHair = false;
            HairIndex = AllHair.Count - 1;

            ChangeHair(false);
        }

        // FACE
        if (FaceIndex > AllFaces.Count - 1)
        {
            hasFace = false;
            FaceIndex = 0;

            ChangeFace(false);
        }

        if (FaceIndex < 0)
        {
            hasFace = false;
            FaceIndex = AllFaces.Count-1;

            ChangeFace(false);
        }
    }

    public void ChangeFace(bool isBackwards)
    {
        if (FacePivot.transform.childCount > 0)
            Destroy(FacePivot.transform.GetChild(0).gameObject);

        if (!hasFace)
            hasFace = true;
        else
        {
            if (!isBackwards)
                FaceIndex++;
            else
                FaceIndex--;
        }

        GameObject obj = (GameObject)Instantiate(AllFaces[FaceIndex], FacePivot.transform.position, Quaternion.identity);
        currentFace = obj;
        obj.transform.parent = FacePivot.transform;
    }

    public void ChangeHair(bool isBackwards)
    {
        if (HairPivot.transform.childCount > 0)
            Destroy(HairPivot.transform.GetChild(0).gameObject);

        if (!hasHair)
            hasHair = true;
        else
        {
            if (!isBackwards)
                HairIndex++;
            else
                HairIndex--;
        }

        GameObject obj = (GameObject)Instantiate(AllHair[HairIndex], HairPivot.transform.position, Quaternion.identity);
        currentHair = obj;
        obj.transform.parent = HairPivot.transform;
    }

    public void SaveSettings()
    {
        string faceName = currentFace.name.Replace("(Clone)", "");
        string hairName = currentHair.name.Replace("(Clone)", "");

        print(faceName + " saved as face.");
        print(hairName + " saved as hair.");

        PlayerPrefs.SetString("FacePrefabName", faceName);
        PlayerPrefs.SetString("HairPrefabName", hairName);

        PlayerPrefs.SetFloat("HairColorR", currentHairColor.r);
        PlayerPrefs.SetFloat("HairColorG", currentHairColor.g);
        PlayerPrefs.SetFloat("HairColorB", currentHairColor.b);

        SceneManager.LoadScene(0);
    }

}
