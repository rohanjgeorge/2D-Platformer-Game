using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{

    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonQuit;

    [SerializeField] private GameObject LevelSelection;
    // Start is called before the first frame update
    private void Awake() {
        
        buttonPlay.onClick.AddListener(PlayGame);
        buttonQuit.onClick.AddListener(QuitGame);
    }

    private void PlayGame()
    {
        LevelSelection.SetActive(true);
    }

    private void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application quit - won't close the editor");
    }
}
