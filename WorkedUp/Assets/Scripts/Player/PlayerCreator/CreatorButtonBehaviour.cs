using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreatorButtonBehaviour : MonoBehaviour
{
    public PlayerEditor Creator;

    public MaterialType Type;

    public bool isColorButton;
    public int ColorIndex;

    public bool isHair;
    public bool backwards;

    public void OnButtonPress()
    {
        if (isColorButton)
            ChangeColor();
        else
        {
            if(!isHair)
                Creator.ChangeFace(backwards);
            else
                Creator.ChangeHair(backwards);
        }
    }

    public void ChangeColor()
    {
        if(Type == MaterialType.Skin)
            Creator.Skin.color = Creator.SkinColors[ColorIndex];

        if (Type == MaterialType.Hair)
        {
            Creator.currentHairColor = Creator.HairColors[ColorIndex];
            Creator.Hair.color = Creator.HairColors[ColorIndex];
        }

        if (Type == MaterialType.Torso)
        {
            Creator.Torso.color = Creator.TorsoColors[ColorIndex];
            Creator.Limbs.color = Creator.LimbColors[ColorIndex];
        }

        if (Type == MaterialType.Apron)
        {
            Creator.ApronOne.color = Creator.ApronColorsOne[ColorIndex];
            Creator.ApronTwo.color = Creator.ApronColorsTwo[ColorIndex];
        }
    }

    public void OnSave()
    {
        Creator.SaveSettings();
    }

}
