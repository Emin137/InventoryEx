using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;
    private void Awake() => instance = this;

    [System.Serializable]
    public class ItemData
    {
        public enum ItemType
        {
            Weapon,
            Armor,
            Null
        }

        public enum ItemRank
        {
            S = 0,
            A = 1,
            B = 2,
            Null = 3
        }

        public enum ItemPart
        {
            Weapon = -1,
            Helmet = 0,
            Strap = 1,
            Top = 2,
            Bottom = 3,
            Glove = 4,
            Shoes = 5,
            Null = 6
                
        }

        public ItemType itemType;
        public ItemRank itemRank;
        public ItemPart itemPart;
        public int itemCode;
    }

    public enum Categori
    {
        All,
        Weapon,
        Armor
    }

    private Categori categori;

    [SerializeField]
    List<ItemData> weaponitemDatas;
    [SerializeField]
    List<ItemData> armoritemDatas;

    public List<ItemSlotManager> itemSlots = new List<ItemSlotManager>();

    [SerializeField] TMP_Text inventoryCountText;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform parent;
    [SerializeField] Button[] buttonsCategori;
    [SerializeField] Button buttonDelete;
    [SerializeField] TMP_Text moneyText;

    private int inventoryCount = 0;
    private int inventoryMax = 0;
    private int money;
    public bool isChoose;
    public bool isSort;

    private void Start()
    {
        money = 12000;
        OnAddInventoryMax();
        OnCategoriAll();
        buttonDelete.interactable = false;
        RefreshMoney();
    }

    public void OnCreatWeapon()
    {
        if (inventoryCount < inventoryMax && money>=1000)
        {
            ItemData item = new ItemData();
            int rand = Random.Range(0, 10);
            if (rand == 0)
            {
                item = weaponitemDatas[0];
            }
            else if (rand < 4)
            {
                item = weaponitemDatas[1];
            }
            else
            {
                item = weaponitemDatas[2];
            }
            if (categori != Categori.Armor)
                itemSlots[inventoryCount].gameObject.SetActive(true);
            itemSlots[inventoryCount].SetSlot(item);
            inventoryCount++;
            RefreshInventoryCount();
            money -= 1000;
            RefreshMoney();
            isSort = false;
        }
    }

    public void OnCreatArmor()
    {
        if (inventoryCount < inventoryMax && money>=500)
        {
            ItemData item = new ItemData();
            int rand = Random.Range(0, 10);
            int rand2 = Random.Range(0, 6);
            if (rand == 0)
            {
                item = armoritemDatas[rand2];
            }
            else if (rand < 4)
            {
                item = armoritemDatas[rand2+6];
            }
            else
            {
                item = armoritemDatas[rand2 + 12];
            }
            if (categori != Categori.Weapon)
                itemSlots[inventoryCount].gameObject.SetActive(true);
            itemSlots[inventoryCount].SetSlot(item);
            inventoryCount++;
            RefreshInventoryCount();
            money -= 500;
            RefreshMoney();
            isSort = false;
        }
    }

    public void RefreshInventoryCount()
    {
        inventoryCountText.text = $"{inventoryCount}/{inventoryMax}";
    }

    public void RefreshMoney()
    {
        if (money == 0)
            moneyText.text = 0.ToString();
        else
            moneyText.text = string.Format("{0:#,###}", money);
    }

    public void OnAddInventoryMax()
    {
        if (money >= 2000)
        {
            inventoryMax += 10;
            for (int i = 0; i < 10; i++)
            {
                itemSlots.Add(Instantiate(slotPrefab, parent).GetComponent<ItemSlotManager>());
            }
            RefreshInventoryCount();
            money -= 2000;
            RefreshMoney();
        }
    }

    public void OnAddMoney()
    {
        money += 10000;
        RefreshMoney();
    }

    public void OnCategoriAll()
    {
        categori = Categori.All;
        foreach (var item in itemSlots)
        {
            if (item.itemData.itemType != ItemData.ItemType.Null)
                item.gameObject.SetActive(true);
        }
    }

    public void OnCategoriWeapon()
    {
        categori = Categori.Weapon;
        foreach (var item in itemSlots)
        {
            if(item.itemData.itemType == ItemData.ItemType.Weapon)
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
        }
    }

    public void OnCategoriArmor()
    {
        categori = Categori.Armor;
        foreach (var item in itemSlots)
        {
            if (item.itemData.itemType == ItemData.ItemType.Armor)
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);
        }
    }

    public void OnSell()
    {
        int deleteCount = 0;
        for (int i = itemSlots.Count-1; i >= 0; i--)
        {
            if (itemSlots[i].isChoose)
            {
                Destroy(itemSlots[i].gameObject);
                itemSlots.RemoveAt(i);
                itemSlots.Add(Instantiate(slotPrefab, parent).GetComponent<ItemSlotManager>());
                deleteCount++;
            }
        }
        inventoryCount -= deleteCount;
        RefreshInventoryCount();
        buttonDelete.interactable = false;
        isSort = false;
    }

    public void OnSort()
    {
        if (!isSort)
        {
            itemSlots = itemSlots.OrderBy(x => x.itemData.itemRank).ThenBy(x => x.itemData.itemPart).ToList();
            foreach (var item in itemSlots)
            {
                item.transform.SetAsLastSibling();
            }
            isSort = true;
        }
        else
        {
            itemSlots = itemSlots.OrderBy(x => x.itemData.itemRank).ThenBy(x => x.itemData.itemPart).ToList();
            foreach (var item in itemSlots)
            {
                item.transform.SetAsFirstSibling();
            }
            isSort = false;
        }
    }

    public void CheckSlotChoose()
    {
        isChoose = false;
        foreach (var item in itemSlots)
        {
            if (item.isChoose)
                isChoose = true;
        }

        if (isChoose)
            buttonDelete.interactable = true;
        else
            buttonDelete.interactable = false;
    }
    
}
