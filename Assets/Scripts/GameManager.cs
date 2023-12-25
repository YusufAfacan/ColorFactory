using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Button openGlossaryButton;
    public Button closeGlossaryButton;
    public Button nextLevelButton;
    public Button startButton;
    public Button stopButton;
    public Button resetButton;

    public GameObject glossaryPanel;
    public GameObject levelClearedPanel;
    public GameObject tryAgainPanel;

    public CommandLines CommandLines;
    public SoundManager SoundManager;

    public AudioClip victory;
    public AudioClip fail;
    public AudioClip popup;

    public bool canPlay;

    private void Awake()
    {
        startButton.onClick.AddListener(() => { CommandLines.StartCommanding(); });
        openGlossaryButton.onClick.AddListener(() => { OpenGlossary(); });
        closeGlossaryButton.onClick.AddListener(() => { CloseGlossary(); });
        nextLevelButton.onClick.AddListener(() => { NextLevel(); });
        resetButton.onClick.AddListener(() => { ResetLevel(); });
        SoundManager = FindObjectOfType<SoundManager>();

        canPlay = true;
    }

    public void OpenGlossary()
    {
        if (glossaryPanel.activeInHierarchy)
        {
            glossaryPanel.SetActive(false);
        }
        else
        {
            glossaryPanel.SetActive(true);
        }

        SoundManager.PlayAudioClip(popup);
        canPlay = !canPlay;
    }
    public void CloseGlossary()
    {
        if (glossaryPanel.activeInHierarchy)
        {
            glossaryPanel.SetActive(false);
        }
        else
        {
            glossaryPanel.SetActive(true);
        }

        SoundManager.PlayAudioClip(popup);
        canPlay = !canPlay;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LevelCleared()
    {
        levelClearedPanel.SetActive(true);
        SoundManager.PlayAudioClip(victory);
        canPlay = false;
    }

    public void TryAgain()
    {
        tryAgainPanel.SetActive(true);
        SoundManager.PlayAudioClip(fail);
        canPlay = false;
    }


    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
