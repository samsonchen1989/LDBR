using UnityEngine;
using System.Collections;

public enum WeaponGunState
{
    IDLE,
    RELOADING
}

public class WeaponGun
{
    protected string name;

    protected float fireInterval;
    private float fireTimer = 0;
    bool startFireTimer = false;

    protected float reloadInterval;
    private float reloadTimer = 0;
    bool startReloadTimer = false;

    protected float bulletDamage;
    protected float bulletSpeed;

    protected int clipSize;
    // Ammo in current clip
    protected int clipLeft;
    // Ammo on body
    protected int ammoLeft;

    protected bool canFire;

    protected WeaponGunState state = WeaponGunState.IDLE;

    public string Name
    {
        get {
            return name;
        }
    }

    public float FireInterval
    {
        get {
            return fireInterval;
        }
    }

    public float ReloadInterval
    {
        get {
            return reloadInterval;
        }
    }

    public float BulletDamage
    {
        get {
            return bulletDamage;
        }
    }

    public int ClipSize
    {
        get {
            return clipSize;
        }
    }

    public int ClipLeft
    {
        get {
            return clipLeft;
        }
    }

    public int AmmoLeft {
        get {
            return ammoLeft;
        }
    }

    public WeaponGunState State {
        get {
            return state;
        }
    }

    public bool CanFire {
        get {
            return canFire;
        }

        set {
            canFire = value;
        }
    }

    /* Auto Reload or not?
    public void Shoot(Vector3 position, Vector3 dir)
    {
        if (clipLeft > 0) {
            ObjectFactory.SpawnBullet(position, dir, bulletSpeed, bulletDamage);
            clipLeft--;
            if (clipLeft == 0) {
                Reload();
            } else {
                canFire = false;
                startFireTimer = true;
            }
        }
    }
    */

    public void Shoot(Vector3 position, Vector3 dir)
    {
        if (clipLeft < 0) {
            Debug.LogError("ClipLeft is less than zero.");
            return;
        }

        if (clipLeft == 0) {
            Reload();
        } else {
            ObjectFactory.SpawnBullet(position, dir, bulletSpeed, bulletDamage);
            clipLeft--;
            canFire = false;
            startFireTimer = true;
        }
    }

    public void Reload()
    {
        if (clipLeft == clipSize) {
            return;
        }

        if (ammoLeft == 0) {
            //Debug.Log("Out of ammo.");
            return;
        }

        canFire = false;
        startReloadTimer = true;

        state = WeaponGunState.RELOADING;

        int ammoNeeded = clipSize - clipLeft;
        if (ammoLeft <= ammoNeeded) {
            clipLeft += ammoLeft;
            ammoLeft = 0;
        } else {
            clipLeft = clipSize;
            ammoLeft -= ammoNeeded;
        }
    }

    public void Update(float timeDelta)
    {
        if (startFireTimer) {
            fireTimer += timeDelta;
            if (fireTimer > fireInterval) {
                canFire = true;
                startFireTimer = false;
                fireTimer = 0f;
            }
        }

        if (startReloadTimer) {
            reloadTimer += timeDelta;
            if (reloadTimer > reloadInterval) {
                state = WeaponGunState.IDLE;

                canFire = true;
                startReloadTimer = false;
                reloadTimer = 0f;
            }
        }
    }
}

public class Pistol : WeaponGun
{
    public Pistol()
    {
        // Todo, read from json config file
        name = "Pistol";

        fireInterval = 0.8f;
        reloadInterval = 2f;

        bulletDamage = 10f;
        bulletSpeed = 8f;

        clipSize = 10;
        clipLeft = 5;
        ammoLeft = 10;
        canFire = true;
    }
}
