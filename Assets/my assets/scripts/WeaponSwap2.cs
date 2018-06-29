using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap2 : MonoBehaviour {

    public int playerNumber = 2;
    public int currentWeapon = 0; //this number represents our currently selected gun
    public int weapon0;
    public int weapon1;
    public float _damage;
    private int index = 0;
    public float maxWeaponNumber = 1;
    public bool canSwitch = true;



	// Use this for initialization
	void Start ()
    {

        SelectWeapon(); //we want to activate our current weapon and deactivate out non-current weapons...
	}
	
	// Update is called once per frame
	void Update () {

        if (currentWeapon == 0)
        {
            _damage = 25;
        }
        if (currentWeapon == 1)
        {
            _damage = 20;
        }
        if (currentWeapon == 2)
        {
            _damage = 100;
        }

        int previousWeapon = currentWeapon; //new int previousWeapon. this is just our currentWeapon number.


        {
            if (Input.GetButtonDown("2Switch")) //if at any point we hit Triangle...
            {
                if (canSwitch == true)
                {
                    if (currentWeapon >= transform.childCount - 1) //if our currentWeapon number is over the total number of weapons in our hands...
                    {
                        currentWeapon = 0; //go back to the first weapon, index 0.
                    }
                    else //otherwise
                    {
                        currentWeapon++; //increase as normal. this way we don't go off the edge of our array.
                    }

                }

            }
        }




        if (currentWeapon != previousWeapon) //if our currentWeapon and previousWeapon are no longer the same, call the Select Weapon function to activate the proper gun and deactivate all others
        {
            SelectWeapon(); //call the select weapon script which activates/deactivates our weapons in hand
        }

    }

    public void ReceiveIndex(int _index) //this is how we pickup weapons.
    {
        index = _index;
        currentWeapon = index;
        SelectWeapon();
        //Debug.Log(currentWeapon);
    }

    public void SelectWeapon() //this function exists to run through all guns attached to our weapon holder, enable the current gun and disable all others.
    {
        int i = 0; //i starts at 0. i represents each weapon in the weapon holder.
        foreach (Transform weapon in transform) //for each of the weapons attached to the weapon holder...
        {
            
            if (i == currentWeapon) //if i is the same as our current weapon number...
            {
                weapon.gameObject.SetActive(true); //actiate that gun object
            }
            else weapon.gameObject.SetActive(false);//if the numbers don't match, deactivate that gun

            i++; //increment through each gun in our weapon holder
        }
    }

}
