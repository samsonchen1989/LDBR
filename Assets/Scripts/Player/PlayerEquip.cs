﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEquip : PlayerBase
{
    LinkedList<WeaponGun> guns = new LinkedList<WeaponGun>();

    WeaponGun currentWeapon;

    public WeaponGun CurrentWeapon
    {
        get {
            return currentWeapon;
        }
    }

    // Override Awake() incase PlayerBase's Awake() called multiple times
    void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        guns.AddLast(new Pistol());
        // First pointer always point to current weapon.
        currentWeapon = guns.First.Value;
    }

    public void Shoot(Vector3 position, Vector3 fireDir)
    {
        if (currentWeapon == null) {
            return;
        }

        if (currentWeapon.CanFire) {
            // Shoot one bullet
            currentWeapon.Shoot(position, fireDir);
        }
    }

    public void Reload()
    {
        if (currentWeapon == null) {
            return;
        }

        if (currentWeapon.State != WeaponGunState.RELOADING) {
            currentWeapon.Reload();
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != null) {
            currentWeapon.Update(Time.deltaTime);
        }
    }
}
