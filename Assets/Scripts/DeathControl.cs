﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathControl: MonoBehaviour
{
    private string text = "You Died";
    public bool OnceAnim = true;
    public bool isgrounded;
    public bool ifdead = false;
    //public double speed;
    //public float Yangle;
    private Animator anim;
    public PlayerControl pc;
    public MonoBehaviour mbm;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        pc = GetComponent<PlayerControl>();
        mbm = GetComponent<MonitoredByMonster>();
    }

    // Update is called once per frame
    void Update()
    {
        isgrounded = GetComponent<PlayerControl>().grounded;
        /*speed = GetComponent<Rigidbody2D>().velocity.magnitude;
        Yangle = GetComponent<Rigidbody2D>().velocity.y;
        if (speed >= 8f&&Yangle<-7f&&!ifdead)
        {
            ifdead = true;
           // if(isgrounded == true)
             //   GetComponent<PlayerControl>().enabled = false;
            //anim.SetTrigger("Die");
        }*/
        if(ifdead&&isgrounded)
        {
            transform.gameObject.layer = 8;
            pc.enabled = false;
            if (OnceAnim)
            {
                anim.SetTrigger("Die");
                OnceAnim = false;
            }
            mbm.enabled = false;
            //GetComponent<DeathControl>().enabled = false;
        }
    }
    void OnGUI()
    {
        if (ifdead && isgrounded)
        {
            GUIStyle bb = new GUIStyle();
            bb.normal.background = null;    //这是设置背景填充的
            bb.normal.textColor = new Color(1, 1, 1);   //设置字体颜色的
            bb.fontSize = 100;
            GUI.Label(new Rect(Screen.width * 0.35f, Screen.height * 0.35f, 250, 250), text,bb);
        }
    }
}
