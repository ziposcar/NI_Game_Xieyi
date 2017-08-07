using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Volume : MonoBehaviour {

    public GameObject came;
    public AudioListener cameAL;
    public bool ifvolumeon;
    public Sprite VolumeOn;
    public Sprite VolumeOff;
    public Image img;
    void Start ()
    {
        img = GetComponent<Image>();
        cameAL = came.GetComponent<AudioListener>();
        ifvolumeon = true;
	}
    public void OnChangeVolumeSet()
    {
        
        if(ifvolumeon==true)
        {
            ifvolumeon = false;
            img.sprite = VolumeOff;
            cameAL.enabled = false;
        }
        else if(ifvolumeon == false)
        {
            ifvolumeon = true;
            img.sprite = VolumeOn;
            cameAL.enabled = true;
        }
    }
        

}
