using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public ScoreController scoreController;
    public float speed;
    private Rigidbody2D rb2d;
    public float jump;

    private bool isGrounded = false;


    public BoxCollider2D boxCol;

    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset; 

    // Start is called before the first frame update
    private void Awake() {
        rb2d = gameObject.GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;
    }

    // Update is called once per frame
    void Update()
    {
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
                rb2d.AddForce(new Vector2(0f,jump), ForceMode2D.Force);
            }
        }
        
        private void OnCollisionStay2D( Collision2D other )
    {
        if ( other.transform.tag == "Platform" )
        { 
            isGrounded = true;
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
        Debug.Log("Player killed by enemy");
        //Destroy(gameObject);
        //Play the death animation
        //Reset the scene
    }
}
