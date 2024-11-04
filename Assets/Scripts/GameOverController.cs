using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{

    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonBackToMenu;

    private void Awake() {
        buttonRestart.onClick.AddListener(ReloadLevel);
        buttonBackToMenu.onClick.AddListener(BackToMenu);
    }
    public void PlayerDied()
    {
        gameObject.SetActive(true);
    }

    
    private void ReloadLevel(){
        SceneManager.LoadScene(1);
    }

    private void BackToMenu(){
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
