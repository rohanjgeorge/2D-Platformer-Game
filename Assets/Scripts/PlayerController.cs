using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public BoxCollider2D boxCol;

    private Vector2 boxColInitSize;
    private Vector2 boxColInitOffset; 

    // Start is called before the first frame update
    void Start()
    {
        boxColInitSize = boxCol.size;
        boxColInitOffset = boxCol.offset;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = Input.GetAxisRaw("Horizontal");
        animator.SetFloat("Speed",Mathf.Abs(speed));

        Vector3 scale = transform.localScale;
            if(speed <0){
                scale.x = -1f*Mathf.Abs(scale.x);
            } else if(speed>0){
                scale.x = Mathf.Abs(scale.x);
            }
            transform.localScale = scale;

        float VerticalInput = Input.GetAxis( "Vertical" );

        PlayJumpAnimation( VerticalInput );

        if ( Input.GetKey( KeyCode.LeftControl ) )
        {
            Crouch( true );
        }
        else
        {
            Crouch( false );
        }
    }
        public void PlayJumpAnimation( float vertical )
        {
                if ( vertical > 0 )
                {
                    animator.SetTrigger( "Jump" );
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
}
