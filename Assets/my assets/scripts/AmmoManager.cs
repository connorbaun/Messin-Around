using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour {

    public int currentClipAmmo = 30;
    public int maxClipAmmo = 30;

    public int currentPocketAmmo = 100;
    public int maxPocketAmmo = 100;


	void Start ()
    {
        currentClipAmmo = maxClipAmmo;
        currentPocketAmmo = maxPocketAmmo;
	}

    public void AmmoRefill()
    {
        currentClipAmmo = maxClipAmmo;
        currentPocketAmmo = maxPocketAmmo;
    }

}
