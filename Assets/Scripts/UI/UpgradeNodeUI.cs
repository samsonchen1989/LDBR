using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeNodeUI : MonoBehaviour
{
    public Text propertyName;
    public Text level;
    public Text buttonText;
    public Button upgradeButtton;

    UpgradeType type;
    UpgradaProperty property;

    public void InitNodeType(UpgradeType type)
    {
        this.type = type;
    }

    // Use this for initialization
    void Start()
    {
        if (propertyName == null || level == null || upgradeButtton == null) {
            Debug.LogError("Please assign propertyNode's component first.");
            return;
        }

        EventTriggerListener.Get(upgradeButtton.gameObject).onClick = OnUpgradeButtonClick;
        //upgradeButtton.onClick += 

        RefreshNodeUI();
    }

    void OnEnable()
    {
        Messenger<UpgradeType>.AddListener(MyEventType.WEAPON_UPGRADED, WeaponUpgradeHandler);
    }

    void OnDisable()
    {
        Messenger<UpgradeType>.RemoveListener(MyEventType.WEAPON_UPGRADED, WeaponUpgradeHandler);
    }

    void RefreshNodeUI()
    {
        property = PlayerBase.Instance.PlayerEquip.CurrentWeapon.GetProperty(type);
        if (property == null) {
            return;
        }

        propertyName.text = property.Name;
        level.text = string.Format("{0}, level{1}", property.CurrentValue, property.Level);
        buttonText.text = property.UpgradeCost + "G";

        if (PlayerBase.Instance.PlayerState.Gold < property.UpgradeCost) {
            upgradeButtton.interactable = false;
        }
    }

    void OnUpgradeButtonClick(GameObject go)
    {
        if (upgradeButtton.interactable == true) {
            PlayerBase.Instance.PlayerState.CostGold(property.UpgradeCost);
            // Upgrade after the cost
            PlayerBase.Instance.PlayerEquip.CurrentWeapon.Upgrade(type);
            if (property.IsLevelMax()) {
                upgradeButtton.interactable = false;
                buttonText.text = "Max";
            }
        }
    }

    void WeaponUpgradeHandler(UpgradeType type)
    {
        if (type == this.type) {
            RefreshNodeUI();
        }
    }
}
