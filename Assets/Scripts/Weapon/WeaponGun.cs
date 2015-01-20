using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

    protected Dictionary<UpgradeType, UpgradaProperty> upgradeData = new Dictionary<UpgradeType, UpgradaProperty>();

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

    public Dictionary<UpgradeType, UpgradaProperty> UpgradeData
    {
        get {
            return upgradeData;
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

    public UpgradaProperty GetProperty(UpgradeType type)
    {
        if (upgradeData.ContainsKey(type)){
            return upgradeData[type];
        }

        return null;
    }

    public int GetUpgradeCost(UpgradeType type)
    {
        if (upgradeData.ContainsKey(type)){
            return upgradeData[type].UpgradeCost;
        }
        
        return -1;
    }
    
    public void Upgrade(UpgradeType type)
    {
        if(!upgradeData.ContainsKey(type)) {
            Debug.Log("No such property");
            return;
        }
        
        if (!upgradeData[type].Upgrade()) {
            Debug.Log("Max level maybe");
            return;
        }
        
        switch(type)
        {
        case UpgradeType.CLIP_SIZE:
            clipSize = (int)(upgradeData[type].CurrentValue);
            break;
        case UpgradeType.BULLET_DAMAGE:
            bulletDamage = upgradeData[type].CurrentValue;
            break;
        case UpgradeType.RELOAD_TIME:
            reloadInterval = upgradeData[type].CurrentValue;
            break;
        default:
            break;
        }

        //Messenger<UpgradeType>.Invoke(MyEventType.WEAPON_UPGRADED, type);
    }
}

public class Pistol : WeaponGun
{
    public Pistol()
    {
        // Todo, read from xml/json config file
        name = "Pistol";

        fireInterval = 0.8f;
        reloadInterval = 2f;

        bulletDamage = 10f;
        bulletSpeed = 8f;

        clipSize = 10;
        clipLeft = 5;
        ammoLeft = 10;
        canFire = true;

        // Upgrade data init
        List<LevelData> levels = new List<LevelData>();
        levels.Add(new LevelData(0, 10, 3));
        levels.Add(new LevelData(1, 12, 5));
        levels.Add(new LevelData(2, 15, 7));
        levels.Add(new LevelData(3, 20, -1));
        UpgradaProperty update = new UpgradaProperty(levels, "Clip Size", 0);
        upgradeData.Add(UpgradeType.CLIP_SIZE, update);

        levels = new List<LevelData>();
        levels.Add(new LevelData(0, 2f, 5));
        levels.Add(new LevelData(1, 1.8f, 6));
        levels.Add(new LevelData(2, 1.6f, 10));
        levels.Add(new LevelData(3, 1.4f, -1));
        update = new UpgradaProperty(levels, "Reload Time", 0);
        upgradeData.Add(UpgradeType.RELOAD_TIME, update);
    }
}

public class Rifle : WeaponGun
{
    // Todo, read from xml/json config file
    public Rifle()
    {
        name = "Rifle";
        
        fireInterval = 0.2f;
        reloadInterval = 2f;
        
        bulletDamage = 15f;
        bulletSpeed = 12f;
        
        clipSize = 30;
        clipLeft = 30;
        ammoLeft = 60;
        canFire = true;

        // Upgrade data init
        List<LevelData> levels = new List<LevelData>();
        levels.Add(new LevelData(0, 30, 3));
        levels.Add(new LevelData(1, 35, 5));
        levels.Add(new LevelData(2, 40, 7));
        levels.Add(new LevelData(3, 50, -1));
        UpgradaProperty update = new UpgradaProperty(levels, "Clip Size", 0);
        upgradeData.Add(UpgradeType.CLIP_SIZE, update);

        levels = new List<LevelData>();
        levels.Add(new LevelData(0, 15f, 5));
        levels.Add(new LevelData(1, 18f, 6));
        levels.Add(new LevelData(2, 22f, 10));
        levels.Add(new LevelData(3, 25f, -1));
        update = new UpgradaProperty(levels, "Bullet Damage", 0);
        upgradeData.Add(UpgradeType.BULLET_DAMAGE, update);
    }
}
