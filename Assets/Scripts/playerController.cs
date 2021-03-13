using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    //movment variables
    public float maxspeed;

    //jumping variables
    bool grounded = false;
    float groundcircleradius = 0.2f;  //circle right underneath john that helps check if hes grounded
    public LayerMask groundLayer;
    public Transform groundCheck;     //location of the circle
    public float jumpHeight;          //how much force you can jump with

    
    Rigidbody2D myRB;
    Animator myani;
    bool facingRight;




    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();     //body collision object
        myani = GetComponent<Animator>();       //animator object
        facingRight = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && Input.GetAxis("Jump") > 0)
        {
            grounded = false;
            myani.SetBool("isGrounded", grounded);
            myRB.AddForce(new Vector2(0, jumpHeight));
        } 

    }


    void FixedUpdate()
    {

        //check if grounded if not then we are falling
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundcircleradius, groundLayer);
        myani.SetBool("isGrounded", grounded);

        myani.SetFloat("verticalSpeed",myRB.velocity.y);


        float move = Input.GetAxis("Horizontal");
        myani.SetFloat("speed", Mathf.Abs(move));
        myRB.velocity = new Vector2(move * maxspeed, myRB.velocity.y);

        //if you are going right and not facing right then you flip
        if(move>0 && !facingRight)
        {
            flip();

        }else if (move < 0 && facingRight)
        {
            flip();
        }
    }

    void flip()
    {
        facingRight = !facingRight;
        Vector3 lscale = transform.localScale;
        lscale.x  *= -1;
        transform.localScale = lscale;
    }

}
