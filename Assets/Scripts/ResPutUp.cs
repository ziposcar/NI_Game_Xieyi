using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResPutUp : MonoBehaviour {
    public bool if_E_Pressed = false;
    public string resName;
    public bool showtext;
    public bool ifShieldOn = false;
    public GameObject shield;
    public GameObject cannon;
    public CannonShot cs;
    public bool ifCoroutine = false;
    public RaycastHit2D hit;
    public float curAlpha = 0;
    //private GameObject hero;
    private resList reses;
    public resList Reses
    {
        get
        {
            return reses;
        }
    }
    public void Button_E_Down()
    {
        if_E_Pressed = true;
    }
    public void Button_E_Up()
    {
        if_E_Pressed = false;
    }
    // Use this for initialization
    void Start () {
        reses = new resList();
        if(cannon!=null)
        cs = cannon.GetComponent<CannonShot>();
    }
    void showGetRes(GameObject obj)
    {
        switch (obj.name)
        {
            case "FireCraker":resName = "爆竹"; break;
            case "RedPocket":resName = "红包";break;
            case "NPC1": resName = "饺子";break;
            case "fu":resName = "缺失的福字";break;
            case "Ability1":resName = "二段跳能力";break;
            case "Ability2":resName = "护盾能力";break;
        }
        showtext = true;
        StartCoroutine(fadetext());

    }
    IEnumerator fadetext()
    {
        yield return new WaitForSeconds(2f);
        showtext = false;
    }
    void OnGUI()
    {
        string text = "你获得了 " + resName;
        if (showtext)
        {
            GUIStyle bb = new GUIStyle();
            bb.fontStyle = FontStyle.Bold;
            bb.alignment = TextAnchor.MiddleCenter;
            bb.normal.background = null;    //这是设置背景填充的
            bb.normal.textColor = new Color(1, 1, 1);   //设置字体颜色的
            bb.fontSize = 40;
            GUI.Label(new Rect(Screen.width * 0.4f, Screen.height * 0.2f, 250, 250), text, bb);
        }
    }
    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKey("e"))
        //{
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 0.05f), GetComponent<PlayerControl>().facingRight ? Vector2.right : Vector2.left, 0.3f, 1 << LayerMask.NameToLayer("res"));
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("res"))
            {
                if (hit.collider.gameObject.name == "FireCraker" && !reses.FireCreaters.full)
                {
                   
                    if (Input.GetKey("e")|| if_E_Pressed)
                    {
                        showGetRes(hit.collider.gameObject);
                        reses.FireCreaters.getRes(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);
                    }
                }
                else if (hit.collider.gameObject.name == "Ability1" && !reses.Ability1.full)
                {
                    if (!ifCoroutine)
                    {
#if UNITY_ANDROID
                        hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "靠近按“■”键试试";
#endif
                        StartCoroutine(Emerge(hit.collider.gameObject));
                        ifCoroutine = true;
                    }
                    if (Input.GetKey("e") || if_E_Pressed)
                    {
                        showGetRes(hit.collider.gameObject);
                        reses.Ability1.getRes(hit.collider.gameObject);
                        curAlpha = 0;
                        GameObject.Find("GotDoubleJumpEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                        StartCoroutine(Emerge(GameObject.Find("Text_1 (1)")));
                        Destroy(hit.collider.gameObject);
                    }
                    //Debug.Log(hit.collider.gameObject.name);
                    //Destroy(hit.collider.gameObject);
                }
                else if(hit.collider.gameObject.name == "Ability2" && !reses.Ability2.full)
                {
                    if (!ifCoroutine)
                    {
#if UNITY_ANDROID
                        hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "靠近按“■”键试试";
#endif
                        StartCoroutine(Emerge(hit.collider.gameObject));
                        ifCoroutine = true;
                    }
                    if (Input.GetKey("e") || if_E_Pressed)
                    {
                        showGetRes(hit.collider.gameObject);
                        reses.Ability2.getRes(hit.collider.gameObject);
                        curAlpha = 0;
                        Destroy(hit.collider.gameObject);
                    }
                    //Debug.Log(hit.collider.gameObject.name);
                    //Destroy(hit.collider.gameObject);
                }
                else if (hit.collider.gameObject.name == "RedPocket"&&!reses.RedPocket.full)
                {

                    if (Input.GetKey("e") || if_E_Pressed)
                    {
                        showGetRes(hit.collider.gameObject);
                        reses.RedPocket.getRes(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);
                    }
                }
                else if(hit.collider.gameObject.name=="fu"&& !reses.fu.full)
                {
                    if (Input.GetKey("e") || if_E_Pressed)
                    {
                        showGetRes(hit.collider.gameObject);
                        GameObject.Find("PutFuEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                        GameObject.Find("BeforePutFuEventPoint").GetComponent<BoxCollider2D>().enabled = false;
                        reses.fu.getRes(hit.collider.gameObject);
                        Destroy(hit.collider.gameObject);
                    }
                }
            }
            else if (hit.collider.gameObject.CompareTag("onlyTrigger"))
            {
                if (hit.collider.gameObject.name == "Cannon"/* && reses.FireCreaters.full*/)
                {
                    if (Input.GetKeyDown("e") || if_E_Pressed)
                    {
                        if (reses.FireCreaters.numOfRes > 0)
                        {
                            cs.FireCrackersAdded++;
                            reses.FireCreaters.numOfRes--;
                        }

                        if (cs.FireCrackersAdded==3)
                        {
                            cannon.GetComponentInChildren<TextMesh>().text = "准备发射";
                            //cannon.GetComponentInChildren<TextMesh>().color = new Color(1f,1f,1f,0f);
                            if (Input.GetKeyDown("e") || if_E_Pressed)
                            {
                                StartCoroutine(cs.RollToShot());
                                reses.FireCreaters.clear();
                                hit.collider.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                            }
                        }
                        else
                        {
                            if (Input.GetKeyDown("e") || if_E_Pressed)
                            {
                                StartCoroutine(Emerge(hit.collider.gameObject));
                            }
                        }
                    }
                }
            }
            else if(hit.collider.gameObject.CompareTag("NPC"))
            {
                if (hit.collider.gameObject.name == "NPC1")
                {
                    if (!ifCoroutine)
                    {
                        ifCoroutine = true;
                        StartCoroutine(Emerge(hit.collider.gameObject));
                        
                    }
                    if (Input.GetKeyDown("e") || if_E_Pressed)
                    {
                        if (reses.RedPocket.full)
                        {
                            hit.collider.gameObject.GetComponentInChildren<TextMesh>().text = "太谢谢你了,听说\n年兽喜欢吃饺子！";
                            StartCoroutine(Emerge(hit.collider.gameObject));
                            reses.RedPocket.clear();
                            reses.Dumplings.getRes();
                            showGetRes(hit.collider.gameObject);
                            GameObject.Find("GotDumplingsEventPoint").GetComponent<BoxCollider2D>().enabled = true;
                            GameObject.Find("ThrowDumplingsEventPoint").GetComponent<BoxCollider2D>().enabled = true;

                        }
                    }
                }
                else
                {
                    if (!ifCoroutine)
                    {
                        StartCoroutine(Emerge(hit.collider.gameObject));
                        ifCoroutine = true;
                    }
                }
            }
            
        }
        else if((Input.GetKeyDown("e") || if_E_Pressed )&& hit.collider == null && reses.Ability2.full)
        {
            bool ifcouroutine = true;
            if (!ifShieldOn)
            {
                shield.GetComponent<MeshRenderer>().enabled = true;
                shield.GetComponent<DynamicLight>().enabled = true;
                ifShieldOn = true;
                if (ifcouroutine)
                {
                    StartCoroutine(ShieldFade());
                }
            }
        }
    }
    IEnumerator ShieldFade()
    {
        yield return new WaitForSeconds(5f);
        shield.GetComponent<MeshRenderer>().enabled = false;
        shield.GetComponent<DynamicLight>().enabled = false;
        ifShieldOn = false;
    }

    IEnumerator Emerge(GameObject obj)
    {
        
        if (obj != null)
        {
            TextMesh tm = obj.GetComponentInChildren<TextMesh>();
            for (; curAlpha < 1; curAlpha += Time.deltaTime * 0.7f)
            {
                if (obj!=null&&obj.CompareTag("res"))
                    tm.color = new Color(1f, 1f, 1f, curAlpha);
                else if (obj != null && obj.CompareTag("onlyTrigger"))
                {
                    tm.color = new Color(1f, 1f, 1f, curAlpha);
                }
                else if (obj != null && obj.CompareTag("NPC"))
                {
                    tm.color = new Color(0f, 0f, 0f, curAlpha);
                    obj.transform.FindChild("NPCduihuakuang").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, curAlpha);
                }
                else if (obj != null && obj.CompareTag("EventText"))
                {
                    tm.color = new Color(1f, 1f, 1f, curAlpha);
                }
                yield return 0;
            }
           
            yield return new WaitForSeconds(1.0f);
            yield return StartCoroutine(Fade(obj));
                
        }
    }
    IEnumerator Fade(GameObject obj)
    {
            
        if (obj != null)
        {
            TextMesh tm = obj.GetComponentInChildren<TextMesh>();
            
                for (; curAlpha > 0; curAlpha -= Time.deltaTime * 0.7f)
                {
                    if (obj != null && obj.CompareTag("res"))
                        tm.color = new Color(1f, 1f, 1f, curAlpha);
                    else if (obj != null && obj.CompareTag("onlyTrigger"))
                    {
                        tm.color = new Color(1f, 1f, 1f, curAlpha);
                    }
                    else if (obj != null && obj.CompareTag("NPC"))
                    {
                        tm.color = new Color(0f, 0f, 0f, curAlpha);
                        obj.transform.FindChild("NPCduihuakuang").GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, curAlpha);
                    }
                    yield return 0;
                }
                ifCoroutine = false;
                curAlpha = 0;

        }
    }

}

// 物品列表类，（不敢妄言是仓库）

 
public class resList
{
    public Res FireCreaters;
    public Res Ability1;
    public Res Ability2;
    public Res RedPocket;
    public Res Dumplings;
    public Res fu;
    // private Vector2 headPoint;
    public resList()
    {
        FireCreaters = new Res(0, 3);
        Ability1 = new Res(0, 1);
        Ability2 = new Res(0, 1);
        RedPocket = new Res(0, 1);
        Dumplings = new Res(0, 1);
        fu = new Res(0, 1);

    }
}
// 物品组信息的基类
public class Res
{
    public int numOfRes;
    private int maxOfRes;
    public Res(int numHad, int numMax)
    {
        numOfRes = numHad;
        maxOfRes = numMax;
    }
    public bool ifmorethan1
    {
        get
        {
            return maxOfRes >= 1;
        }
    }

    public int num
    {
        get
        {
            return numOfRes;
        }
    }
    public bool empty
    {
        get
        {
            return numOfRes == 0;
        }
    }
    public int fullNum
    {
        set
        {
            maxOfRes = value;
        }
    }
    
    public bool full
    {
        get
        {
            return numOfRes == maxOfRes;
        }
    }
    public void getRes(GameObject o)
    {
        if (this.empty)
        {
            // obj = (GameObject)GameObject.Instantiate(o);
        }
        ++numOfRes;
        numOfRes = System.Math.Min(numOfRes, maxOfRes);
    }
    public void getRes()
    {
        ++numOfRes;
        numOfRes = System.Math.Min(numOfRes, maxOfRes);
    }
    public void clear()
    {
        numOfRes = 0;
    }
    // public GameObject obj;
}


