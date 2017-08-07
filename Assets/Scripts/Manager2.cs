using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum DisplayState
{
    Play,
    Pause,
}

public class Manager2 : MonoBehaviour
{

    public DisplayState state;//展示状态,暂停或运行
    GUIStyle guiRectStyle;
    GUIStyle guiTextRectStyle1;
    GUIStyle guiTextRectStyle2;
    //按钮
    public Texture Pause_btn;
    public Texture Continue_btn;
    public Texture Replay_btn;
    public Texture Exit_btn;

    //道具
    public Texture FireCreaters_img;
    public Texture Ability1_img;
    public Texture Ability2_img;
    public Texture RedPocket_img;
    public Texture Dumplings_img;
    public Texture fu_img;

    public float screenX;
    public float screenY;
    public float scaleX;
    public float scaleY;

    ResPutUp ResPart;
    Vector2 pointStart; // 物品列表顶点
    // Use this for initialization
    void Start()
    {
        screenX = Screen.width;
        screenY = Screen.height;
        scaleX = screenX / 1280;
        scaleY = screenY / 720;
        guiRectStyle = new GUIStyle();
        guiRectStyle.border = new RectOffset(0, 0, 0, 0);
        guiRectStyle.alignment = TextAnchor.MiddleCenter;
        //textstyle1
        guiTextRectStyle1 = new GUIStyle();
        guiTextRectStyle1.fontSize = 36;
        guiTextRectStyle1.fontStyle = FontStyle.Bold;
        guiTextRectStyle1.normal.textColor = Color.white;

        //textstyle2
        guiTextRectStyle2 = new GUIStyle();
        guiTextRectStyle2.fontSize = 30;
        guiTextRectStyle2.fontStyle = FontStyle.Bold;
        guiTextRectStyle2.normal.textColor = Color.white;
        guiTextRectStyle2.alignment = TextAnchor.MiddleRight;

        ResPart = GameObject.Find("NewHero").GetComponent<ResPutUp>();
        pointStart = new Vector2(1000, 20);

        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnGUI()
    {
        if (state == DisplayState.Play)
        {

            if (GUI.Button(new Rect(20 * scaleX, 20 * scaleY,
                Pause_btn.width * scaleX, Pause_btn.height * scaleY),
                Pause_btn,
                guiRectStyle))
            {
                state = DisplayState.Pause;
                Time.timeScale = 0;
            }
        }
        if (state == DisplayState.Pause)
        {
            if (GUI.Button(new Rect(screenX * 0.5f - Exit_btn.width * 0.5f * scaleX,
                screenY * 0.4f + Replay_btn.height * 0.5f * scaleY + 10f * scaleY,
                Exit_btn.width * scaleX, Exit_btn.height * scaleY),
                Exit_btn,
                guiRectStyle))
                Application.Quit();


            if (GUI.Button(new Rect(screenX * 0.5f - Replay_btn.width * 0.5f * scaleX,
                screenY * 0.4f - Replay_btn.height * 0.5f * scaleY,
                Replay_btn.width * scaleX, Replay_btn.height * scaleY),
                Replay_btn,
                guiRectStyle))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("NI_GameScene");
            }

            if (GUI.Button(new Rect(screenX * 0.5f - Continue_btn.width * 0.5f * scaleX,
                screenY * 0.4f - Replay_btn.height * 0.5f * scaleY - Continue_btn.height * scaleY - 10f * scaleY,
                Continue_btn.width * scaleX, Continue_btn.height * scaleY),
                Continue_btn,
                guiRectStyle))
            {
                Time.timeScale = 1;
                state = DisplayState.Play;
            }
        }

        int order = 0;
       
        // Debug.Log(ResPart.ToString());
        
        if (ResPart != null && !ResPart.Reses.FireCreaters.empty)
        {
            listing(FireCreaters_img, ref order, "X " + ResPart.Reses.FireCreaters.num.ToString(),"爆竹");
        }
        if (ResPart != null && !ResPart.Reses.Ability1.empty)
        {
            listing(Ability1_img, ref order, "", "二段跳能力石");
        }
        if (ResPart != null && !ResPart.Reses.Ability2.empty)
        {
            listing(Ability2_img, ref order, "", "护盾能力石");
        }
        if (ResPart != null && !ResPart.Reses.RedPocket.empty)
        {
            listing(RedPocket_img, ref order, "","红包");
        }
        if (ResPart != null && !ResPart.Reses.Dumplings.empty)
        {
            listing(Dumplings_img, ref order, "","饺子");
        }
        if (ResPart != null && !ResPart.Reses.fu.empty)
        {
            listing(fu_img, ref order, "", "福字");
        }
        // Debug.Log(order.ToString());
    }
    void listing(Texture t, ref int order, string num,string description)
    {
        GUI.Button(new Rect((pointStart.x - 70) * scaleX, (pointStart.y + 5 + order) * scaleY,
            60 * scaleX, 60 * scaleY),
            description,
            guiTextRectStyle2);
        GUI.Button(new Rect(pointStart.x * scaleX, (pointStart.y + order) * scaleY,
            60 * scaleX, 60 * scaleY),
            t,
            guiRectStyle);
        GUI.Button(new Rect((pointStart.x + 70) * scaleX, (pointStart.y +15+ order) * scaleY,
            60 * scaleX, 60 * scaleY),
            num,
            guiTextRectStyle1);
        order += 80;
    }
}