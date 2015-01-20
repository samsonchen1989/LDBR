using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpgradeNodeUI : MonoBehaviour
{
    public Text propertyName;
    public Text level;
    public Text buttonText;
    public Button upgradeButtton;

    int index;
    UpgradeType type;
    UpgradaProperty property;
    WeaponUpgradeUI parentUI;

    public void InitNodeType(UpgradeType type, int index, WeaponUpgradeUI parent)
    {
        this.type = type;
        this.index = index;
        this.parentUI = parent;
    }

    // Use this for initialization
    void Start()
    {
        if (propertyName == null || level == null || upgradeButtton == null) {
            Debug.LogError("Please assign propertyNode's component first.");
            return;
        }

        EventTriggerListener.Get(upgradeButtton.gameObject).onClick = OnUpgradeButtonClick;

        RefreshNodeUI();
    }

    public void RefreshNodeUI()
    {
        property = PlayerBase.Instance.PlayerEquip.GetWeapon(index).GetProperty(type);
        if (property == null) {
            return;
        }

        propertyName.text = property.Name;
        level.text = string.Format("{0}, level{1}", property.CurrentValue, property.Level);

        if (property.IsLevelMax()) {
            upgradeButtton.interactable = false;
            buttonText.text = "Max";
        } else {
            buttonText.text = property.UpgradeCost + "G";
        }

        if (PlayerBase.Instance.PlayerState.Gold < property.UpgradeCost) {
            upgradeButtton.interactable = false;
        }
    }

    void OnUpgradeButtonClick(GameObject go)
    {
        if (upgradeButtton.interactable == true) {
            PlayerBase.Instance.PlayerState.CostGold(property.UpgradeCost);
            // Upgrade after the cost
            PlayerBase.Instance.PlayerEquip.GetWeapon(index).Upgrade(type);
            parentUI.RefreshChildUpgradeNode();
            /*
            if (PlayerBase.Instance.PlayerState.Gold < property.UpgradeCost) {
                upgradeButtton.interactable = false;
            }

            if (property.IsLevelMax()) {
                upgradeButtton.interactable = false;
                buttonText.text = "Max";
            }
            */
        }
    }
}
