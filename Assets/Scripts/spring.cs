using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class spring : MonoBehaviour {

    SpringJoint2D myJoint;
    GameObject hero;
    RaycastHit2D hit;
    public static bool isHanging;
    LineRenderer line;
    private float maxCatch, maxLenOfLine, minLenOfLine, speedOfTakeUp;
    private float frequency;

    private bool ADown, DDown;
    // Use this for initialization
    void Start () {
        hero = GameObject.Find("NewHero");
        myJoint = hero.AddComponent<SpringJoint2D>();
        myJoint.enabled = false;
        isHanging = false;
        line = GetComponent<LineRenderer>();
        maxCatch = 2.5f;
        maxLenOfLine = 2.0f;
        minLenOfLine = 0.5f;
        speedOfTakeUp = 0.025f;
        frequency = 14;
        ADown = DDown = false;
}

// Update is called once per frame
void Update () {
        if (!isHanging && Input.GetMouseButtonDown(1))
        {
            Vector2 aim = new Vector2();
            aim = Input.mousePosition;
            aim.x -= Camera.main.pixelWidth / 2f + hero.transform.position.x;
            aim.y -= Camera.main.pixelHeight / 2f + hero.transform.position.y;
            if (aim.y <= 0) return;
            hit = Physics2D.Linecast(hero.transform.position, aim, 1 << LayerMask.NameToLayer("Ground"));
            if (hit.distance <= maxCatch)
            {
                if (myJoint == null)
                {
                    myJoint = hero.AddComponent<SpringJoint2D>();
                }
                CreateSpring(hit);
            }
        }
        else if (isHanging && Input.GetMouseButtonDown(1))
        {
            if (myJoint == null)
            {
                isHanging = false;
            }
            else
            {
                springBreak();
            }
        }
        else if (isHanging && Input.GetKeyDown("w"))
        {
            springUp();
        }
        else if (isHanging && Input.GetKeyDown("s"))
        {
            springDown();
        }
        else if (isHanging && Input.GetKey("q"))
        {
            StartCoroutine(springTakeUp());
        }
        if (Input.GetKeyDown("a"))
        {
            ADown = true;
        }
        if (Input.GetKeyDown("d"))
        {
            DDown = true;
        }
        if (Input.GetKeyUp("a"))
        {
            ADown = false;
        }
        if (Input.GetKeyUp("d"))
        {
            DDown = false;
        }

        if (isHanging)
        {
            line.SetPosition(0, hero.transform.position);
            line.SetPosition(1, hit.point);

            hero.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -10f * (float)Math.Abs(Math.Cos((hit.point.y - hero.transform.position.y)/hit.distance))));
        }

	}

    private void CreateSpring(RaycastHit2D hit)
    {
        Vector2 aim = hit.point;
        myJoint.connectedBody = null;
        myJoint.connectedAnchor = aim;
        myJoint.frequency = frequency;
        myJoint.enableCollision = true;
        myJoint.enabled = true;
        myJoint.distance = (float)Math.Min(maxLenOfLine, Math.Sqrt(Math.Pow(aim.y - hero.transform.position.y, 2) + Math.Pow(aim.y - hero.transform.position.y, 2)));
        isHanging = true;

        line.enabled = true;
    }
    private void springUp()
    {
        if (myJoint != null && myJoint.distance > minLenOfLine)
        {
            myJoint.distance -= 0.08f;
        }
    }
    private void springDown()
    {
        if (myJoint != null && myJoint.distance < 10f)
        {
            myJoint.distance += 0.08f;
        }
    }
    private void springBreak()
    { 
        myJoint.enabled = false;
        isHanging = false;

        line.enabled = false;
    }

    IEnumerator springTakeUp()
    {
        while (myJoint.distance >= minLenOfLine)
        {
            myJoint.distance -= speedOfTakeUp;
            yield return 0;
        }
        springBreak();
        yield return 0;
    }

}

public class example : MonoBehaviour
{
    void OnJointBreak(float breakForce)
    {
        spring.isHanging = false;
    }
}