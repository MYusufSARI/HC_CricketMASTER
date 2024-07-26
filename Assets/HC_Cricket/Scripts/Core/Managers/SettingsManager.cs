using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private Sprite optionsOnSprite;
    [SerializeField] private Sprite optionsOffSprite;
    [SerializeField] private Image soundsButtonImage;
    [SerializeField] private Image hapticsButtonImage;


    [Header(" Settings ")]
    private bool soundsState =true;
    private bool hapticsState = true;




    private void Start()
    {
        Setup();
    }


    private void Setup()
    {
        if (soundsState)
        {
            EnableSounds();
        }

        else
        {
            DisableSounds();
        }
    }


    public void ChangeSoundsState()
    {
        if (soundsState)
        {
            DisableSounds();
        }

        else
        {
            EnableSounds();
        }

        soundsState = !soundsState;

        // Save the value of the sounds State
    }


    private void DisableSounds()
    {
        // Tell the soudsn manager to set the vlolme of All the sounds to 0
        soundManager.DisableSounds();

        // Changed the image of the soudns button
        soundsButtonImage.sprite = optionsOffSprite;

    }


    private void EnableSounds()
    {
        soundManager.EnableSounds();

        // Changed the image of the soudns button
        soundsButtonImage.sprite = optionsOnSprite;
    }
}
