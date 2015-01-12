using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPrefabsDefinition : MonoBehaviour
{

    #region Sigleton Code

    private static ItemPrefabsDefinition instance;

    public static ItemPrefabsDefinition Instance {
        get {
            if (instance == null) {
                Debug.LogError("No instance of ItemPrefabsDefinition in the scene.");
            }

            return instance;
        }
    }

    void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Debug.LogError("Just one ItemPrefabsDefinition allowed.");
        }

        ItemDefinitionInit();
    }

    #endregion
    
    private Dictionary<int, Item> itemDic = new Dictionary<int, Item>();

    public Dictionary<int, Item> ItemDictionary {
        get {
            return itemDic;
        }
    }

    public List<ItemRecipe> Recipes = new List<ItemRecipe>();
    private Dictionary<int, ItemRecipe> recDic;

    public Dictionary<int, ItemRecipe> RecipeDictionary {
        get {
            //Definition are never changed in game, so just copy references over once
            if (recDic == null) {
                recDic = new Dictionary<int, ItemRecipe>();
                foreach (ItemRecipe recipe in Recipes) {
                    recDic [recipe.craftID] = recipe;
                }
            }

            return recDic;
        }
    }

    public static Item ItemClone(int id)
    {
        return instance.ItemDictionary[id].ShallowClone();
    }

    public static ItemStack StackClone(int itemID, int size)
    {
        return new ItemStack() { num = size, item = ItemClone(itemID) };
    }

    public static string GetItemName(int id)
    {
        if (instance.ItemDictionary[id] == null) {
            return "";
        }

        return instance.ItemDictionary[id].itemName;
    }

    void ItemDefinitionInit()
    {
        itemDic.Add(0, new Item("ItemKey", 0, "Key to police office.", Item.ItemType.Consumable, 1));
    }

}
