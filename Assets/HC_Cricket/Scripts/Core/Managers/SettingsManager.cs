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
    [SerializeField] private GameObject settingsPanel;


    [Header(" Settings ")]
    private bool soundsState = true;
    private bool hapticsState = true;




    private void Awake()
    {
        soundsState = PlayerPrefs.GetInt("sounds", 1) == 1;
        hapticsState = PlayerPrefs.GetInt("haptics", 1) == 1;

    }


    private void Start()
    {
        Setup();

        settingsPanel.SetActive(false);
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

        PlayerPrefs.SetInt("sounds", soundsState ? 1 : 0);
    }


    private void ChangeHapticsState()
    {
        if (hapticsState)
        {
            DisableHaptics();
        }

        else
        {
            EnableHaptics();
        }

        hapticsState = !hapticsState;

        // Save the value of the sounds State

        PlayerPrefs.SetInt("haptics", hapticsState ? 1 : 0);
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


    private void DisableHaptics()
    {

    }


    private void EnableHaptics()
    {

    }


    public void ShowSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }


    public void HideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }
}
