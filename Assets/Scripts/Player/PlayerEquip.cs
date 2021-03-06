﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerEquip : PlayerBase
{
    List<WeaponGun> guns = new List<WeaponGun>();

    WeaponGun currentWeapon;
    int currentWeaponSlot;

    public WeaponGun CurrentWeapon
    {
        get {
            return currentWeapon;
        }
    }

    public int CurrentweaponSlot
    {
        get {
            return currentWeaponSlot;
        }
    }

    public int EquipCount
    {
        get {
            return guns.Count;
        }
    }

    public WeaponGun GetWeapon(int index)
    {
        if (index < 0) {
            Debug.Log("index number illegal");
        }

        if (index >= guns.Count) {
            Debug.Log("index out of range");
            return null;
        }

        return guns[index];
    }

    // Override Awake() incase PlayerBase's Awake() called multiple times
    void Awake()
    {
        ReplaceFisrtWeapon(new Pistol());
    }

    // Use this for initialization
    void Start()
    {
        currentWeaponSlot = 1;
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

    public void ReplaceFisrtWeapon(WeaponGun gun)
    {
        if (guns.Count == 0) {
            guns.Add(gun);
        } else {
            guns[0] = gun;
        }

        currentWeapon = guns[0];
    }

    public void ReplaceSecondWeapon(WeaponGun gun)
    {
        if (guns.Count < 2) {
            guns.Add(gun);
        } else {
            guns[1] = gun;
        }

        currentWeapon = guns[1];
    }
    
    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != null) {
            currentWeapon.Update(Time.deltaTime);
        }

        if (Input.GetKeyUp(KeyCode.Alpha1)) {
            if (guns.Count < 1) {
                return;
            }
            
            if (guns[0] == null) {
                return;
            }

            currentWeaponSlot = 1;
            currentWeapon = guns[0];
        } else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (guns.Count < 2) {
                return;
            }

            if (guns[1] == null) {
                return;
            }

            currentWeaponSlot = 2;
            currentWeapon = guns[1];
        }
    }
}
