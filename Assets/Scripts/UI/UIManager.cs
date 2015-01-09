using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text weaponText;

    PlayerBase playerBase;

    // Use this for initialization
    void Start()
    {
        if (healthText == null || weaponText == null || weaponText == null) {
            Debug.LogError("Please assign text gameObject first.");
            return;
        }

        playerBase = PlayerBase.Instance;
    }
    
    // Update is called once per frame
    void Update()
    {
        healthText.text = string.Format("HP: {0}/{1}", playerBase.PlayerState.Health,
                                        playerBase.PlayerState.MaxHelath);
        if (playerBase.PlayerEquip.CurrentWeapon.State == WeaponGunState.RELOADING) {
            weaponText.text = string.Format("{0}: Reloading", playerBase.PlayerEquip.CurrentWeapon.Name);
        } else {
            weaponText.text = string.Format("{0}: {1}/{2}", playerBase.PlayerEquip.CurrentWeapon.Name,
                                            playerBase.PlayerEquip.CurrentWeapon.ClipLeft,
                                            playerBase.PlayerEquip.CurrentWeapon.AmmoLeft);
        }
    }
}
