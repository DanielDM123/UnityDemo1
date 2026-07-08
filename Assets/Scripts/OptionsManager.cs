using UnityEngine;
using Project;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    public bool muteAudio = false;

    public List<TMP_FontAsset> fontList;
    public static event Action FontUpdated;

    private AudioManager AudioManager;

    void Start()
    {
        AudioManager = GameManager.Instance.AudioManager;
    }

    public TMP_FontAsset GetFontClass(string classID)
    {
        switch (classID)
        {
            case "MenuText":
                return fontList[0];
            case "CardTitle":
                return fontList[1];
            case "CardBody":
                return fontList[2];
            case "CardBodyBold":
                return fontList[3];
            case "MenuTextBold":
                return fontList[4];

            default:
                return fontList[0];
        }
    }

    public void UpdateFont()
    {
        
        FontUpdated?.Invoke();
    }


}
