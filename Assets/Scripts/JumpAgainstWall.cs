using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAgainstWall : MonoBehaviour {

    public bool IfOnTheWall = false;
    public float WallJumpForce = 10f;
    public bool JumpAgainst = false;
    public Animator anim;
    public PlayerControl pc;
    public DeathControl dc;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerControl>();
        dc = GetComponent<DeathControl>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!pc.allowable)
            return;
        if (IfOnTheWall && Input.GetButtonDown("Jump"))
        {
            JumpAgainst = true;
        }
        if (JumpAgainst)
        {
            
           
            bool iffacingright = pc.facingRight;
            if (!pc.ifUpSideDown)
            {
                if (!iffacingright)
                {
                    //GetComponent<PlayerControl>().Flip();
                    anim.SetBool("WallRide", false);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, 1) * WallJumpForce);
                    anim.SetTrigger("Jump");
                    JumpAgainst = false;
                }
                else
                {
                    //GetComponent<PlayerControl>().Flip();
                    anim.SetBool("WallRide", false);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 1) * WallJumpForce);
                    anim.SetTrigger("Jump");
                    JumpAgainst = false;
                }
            }
            else
            {
                if (!iffacingright)
                {
                    //GetComponent<PlayerControl>().Flip();
                    anim.SetBool("WallRide", false);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(1, -1) * WallJumpForce);
                    anim.SetTrigger("Jump");
                    JumpAgainst = false;
                }
                else
                {
                    //GetComponent<PlayerControl>().Flip();
                    anim.SetBool("WallRide", false);
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-1, -1) * WallJumpForce);
                    anim.SetTrigger("Jump");
                    JumpAgainst = false;
                }
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Walls") && !dc.isgrounded&&!JumpAgainst)
        {
            anim.SetBool("WallRide",true);
            if (!IfOnTheWall)
                pc.Flip();
            IfOnTheWall = true;
        }
    }
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Walls") && !dc.isgrounded&&!JumpAgainst)
        {
            anim.SetBool("WallRide",true);
            if(!IfOnTheWall)
                pc.Flip();
            IfOnTheWall = true;
            GetComponent<Rigidbody2D>().gravityScale = 0.5f;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        anim.SetBool("WallRide", false);
        //anim.Play("Idle");
        GetComponent<Rigidbody2D>().gravityScale = 1f;
        IfOnTheWall = false;
    }
}
