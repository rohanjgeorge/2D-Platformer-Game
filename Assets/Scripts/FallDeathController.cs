using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeathController : MonoBehaviour


{
    public GameOverController gameOverController;
    public PlayerController playerController;
    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( collision.gameObject.GetComponent<PlayerController>( ) != null )
        {
            //Reloading the Current Scene.
            //int currentSceneIndex = SceneManager.GetActiveScene( ).buildIndex;
            //SceneManager.LoadScene( currentSceneIndex );
            playerController.PlayDeathAnimation( );
            playerController.PlayerDeath( );
            gameOverController.PlayerDied();
        }
    }
}
