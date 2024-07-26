using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private CanvasGroup transitionCG;
    [SerializeField] private TextMeshProUGUI transitionGameModeText;


    private void Start()
    {
        GameManager.onGameSet += GameSetCallback;
    }


    private void OnDestroy()
    {
        GameManager.onGameSet -= GameSetCallback;
    }


    private void GameSetCallback()
    {
        if (GameManager.instance.IsBowler())
        {
            transitionGameModeText.text = "BOWL";
        }

        else
        {
            transitionGameModeText.text = "BAT";
        }

        // Show the TransitionPanel
        LeanTween.alphaCanvas(transitionCG, 1, 0.5f);
        transitionCG.blocksRaycasts = true;
        transitionCG.interactable = true;
    }
}
