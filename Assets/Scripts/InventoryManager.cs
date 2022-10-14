using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    static public InventoryManager instance;
    private void Awake() => instance = this;

    [SerializeField] GameObject prefab;
    [SerializeField] Transform parent;
    [SerializeField] TMP_Text inventoryCountText;
    [SerializeField] Button[] buttons;
    [SerializeField] Button deleteButton;
    [SerializeField] ItemManager itemManager;

    public List<ItemManager> inventory = new List<ItemManager>();
    [SerializeField]
    public List<ItemManager> sortList = new List<ItemManager>();

    int inventoryCount = 0;
    int inventoryMax = 10;

    public enum ShowMode
    {
        All,
        Weapon,
        Armor
    }

    public enum Mode
    {
        None,
        Delete
    }

    public ShowMode showMode;

    public Mode mode;

    private void Start()
    {
        RefreshCount();
        buttons[0].interactable = false;
    }

    public void BtnCreatWeaponItem()
    {
        if (inventoryCount < inventoryMax)
        {
            itemManager.SetItem(ItemManager.ItemType.Weapon);
            itemManager = Instantiate(itemManager.gameObject, parent).GetComponent<ItemManager>();
            inventory.Add(itemManager);
            inventoryCount++;
            RefreshCount();
        }

        if (showMode == ShowMode.Armor)
            itemManager.gameObject.SetActive(false);
    }

    public void BtnCreatArmorItem()
    {
        if (inventoryCount < inventoryMax)
        {
            itemManager.SetItem(ItemManager.ItemType.Armor);
            itemManager = Instantiate(itemManager.gameObject, parent).GetComponent<ItemManager>();
            inventoryCount++;
            RefreshCount();
        }

        if (showMode == ShowMode.Weapon)
            itemManager.gameObject.SetActive(false);
    }

    public void BtnInventoryMax()
    {
        inventoryMax += 10;
        RefreshCount();
    }

    public void BtnAllShow()
    {
        showMode = ShowMode.All;
        buttons[0].interactable = false;
        buttons[1].interactable = true;
        buttons[2].interactable = true;
        foreach (var item in inventory)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void BtnWeaponShow()
    {
        showMode = ShowMode.Weapon;
        buttons[0].interactable = true;
        buttons[1].interactable = false;
        buttons[2].interactable = true;
        foreach (var item in inventory)
        {
            if (item.itemData.itemType == ItemManager.ItemType.Armor)
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    public void BtnArmorShow()
    {
        showMode = ShowMode.Armor;
        buttons[0].interactable = true;
        buttons[1].interactable = true;
        buttons[2].interactable = false;
        foreach (var item in inventory)
        {
            if (item.itemData.itemType == ItemManager.ItemType.Weapon)
            {
                item.gameObject.SetActive(false);
            }
            else
            {
                item.gameObject.SetActive(true);
            }
        }
    }

    public void RefreshCount()
    {
        inventoryCountText.text = $"{inventoryCount}/{inventoryMax}";
    }


    public void BtnSort()
    {
        sortList = inventory.OrderBy(x => x.itemData.itemRank).ThenBy(x => x.itemData.itempart).ToList();
        for (int i = 0; i < sortList.Count; i++)
        {
            sortList[i].gameObject.transform.SetAsLastSibling();
        }
        sortList.Clear();
    }

    public void BtnDelete()
    {
        if (mode == Mode.None)
        {
            mode = Mode.Delete;
            deleteButton.image.color = Color.red;
        }
        else
        {
            mode = Mode.None;
            deleteButton.image.color = Color.white;
        }
    }




}
