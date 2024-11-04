using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
public float speed;
public GameObject groundDetector;
public float rayDistance;
    private int directionChanger = 1;
    public LayerMask groundLayer;

    private void Update()
    {
        Patrol();
    }

    void Patrol(){
        
            transform.Translate( directionChanger * Vector2.right * speed * Time.deltaTime );

            RaycastHit2D hit = Physics2D.Raycast( groundDetector.transform.position, Vector2.down, rayDistance,groundLayer );
                    
        Debug.DrawRay(groundDetector.transform.position, Vector2.down * rayDistance, Color.red);
if (hit.collider != null)
        {
            // Log the point and name of the object hit
            Debug.Log("Ray hit point: " + hit.point + " | Hit object: " + hit.collider.gameObject.name);
        }

        // If no ground is detected, turn around
        if (!hit)
        {
            Vector3 scaleVector = transform.localScale;
            scaleVector.x *= -1;
            transform.localScale = scaleVector;
            directionChanger *= -1;

        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
     if (collision.gameObject.GetComponent<PlayerController>()!=null)
     {
        PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
        playerController.KillPlayer();
     }   
    }
}
