using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAuthority : MonoBehaviour
{
    public int p1CurrentClip = 30;
    public int p1CurrentPocket = 30;

    public int p2CurrentClip = 30;
    public int p2CurrentPocket = 30;

    public int maxClipAmmo = 30;
    public int maxPocketAmmo = 30;

    public PlayerShoot[] guns;
    private WeaponSwap weapon1;
    private WeaponSwap2 weapon2;
    private AmmoManager ammo1;
    private AmmoManager2 ammo2;




	// Use this for initialization
	void Start ()
    {
        guns = new PlayerShoot[3];
        


        weapon1 = FindObjectOfType<WeaponSwap>();
        weapon2 = FindObjectOfType<WeaponSwap2>();

        ammo1 = FindObjectOfType<AmmoManager>();
        ammo2 = FindObjectOfType<AmmoManager2>();

        p1CurrentClip = ammo1.currentClipAmmo;
        p1CurrentPocket = ammo1.currentPocketAmmo;

        p2CurrentClip = ammo2.currentClipAmmo;
        p2CurrentPocket = ammo2.currentPocketAmmo;
    }

    public void Update()
    {
        //Debug.Log(guns[0].currentClipAmmo);
    }
    public void AmmoRefill(int playerNumber)
    {
        if (playerNumber == 1)
        {
            p1CurrentClip = maxClipAmmo;
            p1CurrentPocket = maxPocketAmmo;
        }

        if (playerNumber == 2)
        {
            p2CurrentClip = maxClipAmmo;
            p2CurrentPocket = maxPocketAmmo;
        }
    }

  
}
