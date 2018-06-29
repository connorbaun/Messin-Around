using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimDownSights : MonoBehaviour {
    public Animator anim;
    private bool isScoped = false;
    public PlayerController controller;
    public Camera cam;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Fire4"))
        {
            anim.SetBool("Scoped", true);
            cam.fieldOfView = 45f;
            
        } else
        {
            anim.SetBool("Scoped", false);
            cam.fieldOfView = 60f;
        }

    }
    
}
