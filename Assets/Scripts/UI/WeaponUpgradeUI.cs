using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class WeaponUpgradeUI : MonoBehaviour
{
    public GameObject upgradeNode;
    public GameObject parent;

    public Text weaponName;
    public Button preButton;
    public Button nextButton;

    List<GameObject> dataList = new List<GameObject>();
    int index = 0;

    // Use this for initialization
    void Start()
    {
        if (upgradeNode == null || parent == null) {
            Debug.LogError("Please assign upgrade node prefab or parent game object first.");
            return;
        }

        if (weaponName == null || preButton == null || nextButton == null) {
            Debug.LogError("Please assign weapon name text or pre/next button first.");
            return;
        }

        EventTriggerListener.Get(preButton.gameObject).onClick = PreButtonClickHandler;
        EventTriggerListener.Get(nextButton.gameObject).onClick = NextButtonClickHandler;
    }

    void OnEnable()
    {
        CreateUpgradeList(index);
    }

    void PreButtonClickHandler(GameObject go)
    {
        if (index <= 0) {
            Debug.Log("pre index none");
            return;
        }

        index -= 1;

        foreach (var data in dataList) {
            GameObject.Destroy(data);
        }
        
        dataList.Clear();
        // Refresh UI
        CreateUpgradeList(index);
    }

    void NextButtonClickHandler(GameObject go)
    {
        if (index >= PlayerBase.Instance.PlayerEquip.EquipCount - 1) {
            Debug.Log("next index none");
            return;
        }

        index += 1;

        foreach (var data in dataList) {
            GameObject.Destroy(data);
        }
        
        dataList.Clear();
        // Refresh UI
        CreateUpgradeList(index);
    }

    void CreateUpgradeList(int index)
    {
        WeaponGun gun = PlayerBase.Instance.PlayerEquip.GetWeapon(index);
        if (gun == null) {
            Debug.Log("No such gun with index:" + index);
            return;
        }

        // Set text
        weaponName.text = gun.Name;
        // Set pre/next button state
        preButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);

        if (index == 0) {
            preButton.gameObject.SetActive(false);
        }

        if (index == (PlayerBase.Instance.PlayerEquip.EquipCount - 1)) {
            nextButton.gameObject.SetActive(false);
        }

        Dictionary<UpgradeType, UpgradaProperty> datas = gun.UpgradeData;
        foreach (var data in datas) {
            GameObject go = GameObject.Instantiate(upgradeNode) as GameObject;
            if (go != null) {
                go.transform.SetParent(parent.transform);
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = Vector3.one;

                UpgradeNodeUI node = go.GetComponent<UpgradeNodeUI>();
                if (node != null) {
                    node.InitNodeType(data.Key, index, this);
                }

                dataList.Add(go);
            }
        }
    }

    public void RefreshChildUpgradeNode()
    {
        if (dataList == null) {
            return;
        }

        foreach (var data in dataList) {
            data.GetComponent<UpgradeNodeUI>().RefreshNodeUI();
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
