using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public ScoreController scoreController;
    public HeartController heartController;
    public float speed;
    private Rigidbody2D rb2d;
    public float jump;

    private bool isGrounded = false;


    public BoxCollider2D boxCol;

    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset; 

    public SpriteRenderer[] hearts;

    public bool isDead = false;
    private int health;

    // Start is called before the first frame update
    private void Awake() {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;

        health = hearts.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        MoveCharacter(horizontal, vertical);
        PlayMovementAnimation(horizontal, vertical);


        if ( Input.GetKey( KeyCode.LeftControl ) )
        {
            Crouch( true );
        }
        else
        {
            Crouch( false );
        }
    }

        public void Crouch( bool crouch )
        {
            if (crouch == true){
                float offX = -0.003144771f;     //Offset X
                float offY = 0.6106966f;      //Offset Y

                float sizeX = 0.683868f;     //Size X
                float sizeY = 1.342233f;     //Size Y

                boxCol.size = new Vector2( sizeX, sizeY );   //Setting the size of collider
                boxCol.offset = new Vector2( offX, offY );   //Setting the offset of collider
            }
            else {
                boxCol.size = boxColInitSize;
                boxCol.offset = boxColInitOffset;
            }
        
        animator.SetBool( "Crouch", crouch );
        }

        private void PlayMovementAnimation(float horizontal, float vertical){
            animator.SetFloat("Speed",Mathf.Abs(horizontal));

            Vector3 scale = transform.localScale;
                if(horizontal <0){
                    scale.x = -1f*Mathf.Abs(scale.x);
                } else if(horizontal>0){
                    scale.x = Mathf.Abs(scale.x);
                }
                transform.localScale = scale;

                
                if (vertical > 0 && isGrounded )
                {
                    animator.SetBool( "Jump", true);
                } else {
                    animator.SetBool("Jump", false);
                }
        }

        private void MoveCharacter(float horizontal, float vertical)
        {

            // move character horizontally
            Vector3 position = transform.position;
            position.x += horizontal * speed * Time.deltaTime;
            transform.position = position;

            // move character vertically
            if(vertical > 0 && isGrounded){

                rb2d.AddForce(new Vector2(0f,jump), ForceMode2D.Impulse);
            }

            if (rb2d.velocity.y > 10f)
    {
        rb2d.velocity = new Vector2(rb2d.velocity.x, 10f);  // Cap the vertical velocity
    }
        }
        
        private void OnCollisionStay2D( Collision2D other )
    {
        if ( other.transform.tag == "Platform" )
        { 
            if (other.transform.tag == "Platform")
    {
        // Loop through all contact points of the collision
        foreach (ContactPoint2D contact in other.contacts)
        {
            // Check if the contact point normal is pointing upwards (i.e., player is on top)
            if (contact.normal.y > 0.5f)
            {
                isGrounded = true;
                return;  // Exit once we've confirmed it's grounded on the top
            }
        }
        
        // If none of the contacts are valid for grounding
        isGrounded = false;
    }
        }
    }

    private void OnCollisionExit2D( Collision2D other )
    {
        if ( other.transform.tag == "Platform" )
        {
            isGrounded = false;
        }
    }

    public void PickUpKey()
    {
        Debug.Log("Player picked up the key");
        scoreController.IncreaseScore(10);
    }

    public void KillPlayer()
    {
        if(!isDead){
Debug.Log("Player attacked by enemy");
        //Play the death animation
        DecreaseHealth();

        }
    }

    private void ReloadLevel(){
        SceneManager.LoadScene(0);
    }

    public void DecreaseHealth( )
    {
        health--;

        heartController.RefreshUI(health);
        if ( health <= 0 )
        {
            PlayDeathAnimation( );
            PlayerDeath( );
            StartCoroutine(PlayerDeath());
        }
    }

    public void PlayDeathAnimation( )
    {
        animator.SetTrigger("Death");
    }

    IEnumerator PlayerDeath( )
    {
        isDead = true;
        rb2d.velocity = Vector2.zero;
        rb2d.isKinematic = true;

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);  // Wait for the length of the death animation

        ReloadLevel( );
    }

}
