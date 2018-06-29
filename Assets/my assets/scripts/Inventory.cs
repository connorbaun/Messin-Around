using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public class Gun
    {
        public GameObject weaponModel;
        public int clipSize = 10;
        public int pocketSize = 250;
        public float _damage = 10f;

        public Gun(GameObject model , int clip, int pock, int _dam)
        {

            weaponModel = model;
            clipSize = clip;
            pocketSize = pock;
            _damage = _dam;
        }

        public Gun pistol = new Gun(GameObject.Find("Glock1.0"), 8, 200, 10);
        public Gun smg = new Gun(GameObject.Find("SubmachineGun"), 60, 500, 5);
        public Gun rifle = new Gun(GameObject.Find("Rifle"), 45, 900, 25);

    }

    

}
