using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Button openGlossaryButton;
    public Button closeGlossaryButton;
    public Button muteButton;
    //public Button closeSettingsButton;
    public Button nextLevelButton;
    public Button startButton;
    public Button stopButton;
    public Button resetButton;

    public GameObject glossaryPanel;
    public GameObject levelClearedPanel;
    public GameObject tryAgainPanel;
    public GameObject settingsPanel;

    public CommandLines CommandLines;
    public SoundManager SoundManager;

    public AudioClip victory;
    public AudioClip fail;
    public AudioClip popup;

    public bool canPlay;

    private int currentLevel;

    private void Awake()
    {
        Instance = this;
        


        startButton.onClick.AddListener(() => { CommandLines.StartCommanding(); });
        openGlossaryButton.onClick.AddListener(() => { OpenGlossary(); });
        closeGlossaryButton.onClick.AddListener(() => { CloseGlossary(); });
        muteButton.onClick.AddListener(() => { Mute(); });
        //closeSettingsButton.onClick.AddListener(() => { CloseSettings(); });
        nextLevelButton.onClick.AddListener(() => { NextLevel(); });
        resetButton.onClick.AddListener(() => { ResetLevel(); });
        SoundManager = FindObjectOfType<SoundManager>();

        canPlay = true;
    }

    private void Start()
    {
        if (!PlayerPrefs.HasKey("CurrentLevel"))
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
        }

        if (PlayerPrefs.GetInt("CurrentLevel") != SceneManager.GetActiveScene().buildIndex)
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentLevel"));
        }

        
        
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

    public void Mute()
    {
        if (SoundManager.audioSource.volume == 1)
        {
            SoundManager.audioSource.volume = 0;
        }
        else
        {
            SoundManager.audioSource.volume = 1;
            SoundManager.PlayAudioClip(popup);
        }

        
    }

    public void CloseSettings()
    {
        if (settingsPanel.activeInHierarchy)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
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
        if ((SceneManager.GetActiveScene().buildIndex + 1) >= SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("CurrentLevel", 0);
            SceneManager.LoadScene(0);
        }
        else
        {
            PlayerPrefs.SetInt("CurrentLevel", SceneManager.GetActiveScene().buildIndex + 1);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        
    }
}
