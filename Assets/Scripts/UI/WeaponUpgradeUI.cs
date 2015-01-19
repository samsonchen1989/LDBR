using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponUpgradeUI : MonoBehaviour
{
    public GameObject upgradeNode;
    public GameObject parent;

    List<GameObject> dataList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        if (upgradeNode == null || parent == null) {
            Debug.LogError("Please assign upgrade node prefab or parent game object first.");
            return;
        }
    }

    void OnEnable()
    {
        CreateUpgradeList();
    }

    void CreateUpgradeList()
    {
        Dictionary<UpgradeType, UpgradaProperty> datas = PlayerBase.Instance.PlayerEquip.CurrentWeapon.UpgradeData;
        foreach (var data in datas) {
            GameObject go = GameObject.Instantiate(upgradeNode) as GameObject;
            if (go != null) {
                go.transform.SetParent(parent.transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                UpgradeNodeUI node = go.GetComponent<UpgradeNodeUI>();
                if (node != null) {
                    node.InitNodeType(data.Key);
                }

                dataList.Add(go);
            }
        }
    }

    public void CloseUpgradeUI()
    {
        foreach (var data in dataList) {
            GameObject.Destroy(data);
        }

        dataList.Clear();

        this.gameObject.SetActive(false);
    }
}
