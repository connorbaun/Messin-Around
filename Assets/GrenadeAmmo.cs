using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeAmmo : MonoBehaviour {
    public int p1Grenades;
    public int p2Grenades;
    
    public PlayerController controller1;
    public PlayerController2 controller2;

    public int maxGrenades = 4;
    
	// Use this for initialization
	void Start () {
        controller1 = FindObjectOfType<PlayerController>();
        controller2 = FindObjectOfType<PlayerController2>();

        p1Grenades = maxGrenades;
        p2Grenades = maxGrenades;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
