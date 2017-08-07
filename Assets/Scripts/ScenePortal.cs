using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePortal : MonoBehaviour {
    public bool ifshowed = false;
    public bool ifseeall = false;
    public Vector3 point;
    public string NextScene;
    public bool ifmoved = false;
    public bool IfDecideDirection = false;
    public GameObject player;
    //public Color originalColor;
    public bool ifopen = false;
    public AudioClip[] dooropenvoice;
    public GameObject came;
    public Camera Came;
    public CameraBlack cb;
    public CameraMoveWithPlayer cmwp;
    public float curColor;
    // Use this for initialization
    void Start()
    {
        Came = came.GetComponent<Camera>();
        point = new Vector3(11.81f, -3.19f, 0f);
        cb = came.GetComponent<CameraBlack>();
        cmwp = came.GetComponent<CameraMoveWithPlayer>();
        //originalColor = Came.backgroundColor;
        curColor = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (ifopen)
        {
            if (!ifseeall)
            {
                if (!ifshowed)
                {
                    cmwp.changeToSolidPoint(point,false);

                    if (Came.orthographicSize <=11.5f)
                    {
                        Came.orthographicSize +=Time.deltaTime*7;
                    }
                    came.transform.rotation = new Quaternion(0, 0, 0, 0);
                    if(Came.orthographicSize>=11.5f)
                        ifshowed = true;
                }

                if (cmwp.ifMoveWithPlayer)
                {
                    if (Came.orthographicSize >=1.5f)
                    {
                        Came.orthographicSize -= Time.deltaTime*7;
                    }
                    came.transform.rotation = new Quaternion(0, 0, 180, 0);
                    if(Came.orthographicSize <= 1.5f)
                        ifseeall = true;
                }
            }
            if (ifseeall)
            {
                GetComponent<Animator>().enabled = true;
                if (curColor >= -0.9f)
                {
                    curColor -= Time.deltaTime / 4;
                    cb.ma.SetFloat("_Float1", curColor);
                }
                Invoke("ChangeScene", 2f);
            }
        }
    }
    void ChangeScene()
    {
        if (curColor <= 0f)
        {
            curColor += Time.deltaTime / 4;
            cb.ma.SetFloat("_Float1", curColor);
        }
        if (!ifmoved)
        {
            SceneManager.LoadScene(NextScene);
            ifmoved = true;
            Physics2D.gravity = new Vector3(0, -9.81f,0);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ifopen = true;
        }
    }
}
