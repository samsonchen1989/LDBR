using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponUpgradeUI : MonoBehaviour
{
    public GameObject upgradeNode;

    // Use this for initialization
    void Start()
    {
        if (upgradeNode == null) {
            Debug.LogError("Please assign upgrade node prefab first.");
            return;
        }

        CreateUpgradeList();
    }

    void CreateUpgradeList()
    {
        Dictionary<UpgradeType, UpgradaProperty> datas = PlayerBase.Instance.PlayerEquip.CurrentWeapon.UpgradeData;
        foreach (var data in datas) {
            GameObject go = GameObject.Instantiate(upgradeNode) as GameObject;
            if (go != null) {
                go.transform.SetParent(this.transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                UpgradeNodeUI node = go.GetComponent<UpgradeNodeUI>();
                if (node != null) {
                    node.InitNodeType(data.Key);
                }
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
    
    }
}
