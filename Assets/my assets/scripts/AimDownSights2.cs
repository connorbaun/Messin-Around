using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSights2 : MonoBehaviour {
    public Animator anim;
    private bool isScoped = false;
    public PlayerController2 controller;
    public Camera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("2Fire4"))
        {
            anim.SetBool("Scoped", true);
            cam.fieldOfView = 45;
        } else
        {
            anim.SetBool("Scoped", false);
            cam.fieldOfView = 60;

        }

    }
    
}
