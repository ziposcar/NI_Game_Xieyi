using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour {
    public bool ifmoved=false;
    public bool IfDecideDirection = false;
    public GameObject player;
    public PlayerControl pc;
    public Color originalColor;
    public bool ifopen = false;
    public GameObject nextdoor;
    public Vector3 transitionto;
    public AudioClip[] dooropenvoice;
    public GameObject came;
    public CameraBlack cb;
    public float curColor;
    // Use this for initialization
    void Start () {
        
        cb = came.GetComponent<CameraBlack>();
        pc = player.GetComponent<PlayerControl>();
        if(nextdoor!=null)
            transitionto = nextdoor.GetComponent<Transform>().position;
        originalColor = came.GetComponent<Camera>().backgroundColor;
        curColor = 0f;
    }
	
	// Update is called once per frame
	void Update () {
        if (ifopen&&nextdoor!=null)
        {

            GetComponent<Animator>().enabled = true;
            nextdoor.GetComponent<Animator>().enabled = true;
            //AudioSource.PlayClipAtPoint(dooropenvoice[0], Vector3.zero, 0.6f);
            //came.GetComponent<Camera>().backgroundColor = new Color(0.2f, 0.2f, 0.2f);
            if (curColor >= -0.9f)
            {
                curColor -= Time.deltaTime/4;
                cb.ma.SetFloat("_Float1", curColor);
            }
            Invoke("ChangePosition", 2f);
            //player.GetComponent<Transform>().position = transitionto;
            //ifopen = false;
        }
    }
    void ChangePosition()
    {
        if (curColor <= 0f)
        {
            curColor += Time.deltaTime / 4;
            cb.ma.SetFloat("_Float1", curColor);
        }
        if (!ifmoved)
        {
            player.GetComponent<Transform>().position = transitionto;
            player.GetComponent<ResPutUp>().Reses.Ability1.clear();
            ifmoved = true;
        }
        GetComponent<Animator>().enabled = false;
        
        ifopen = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (pc.facingRight && !IfDecideDirection)
            {
                transitionto.x += 0.1f;
                IfDecideDirection = true;
            }
            else if (!IfDecideDirection)
            {
                transitionto.x -= 0.1f;
                IfDecideDirection = true;
            }
            ifopen = true;
        }
    }
}
