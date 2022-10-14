using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemManager : MonoBehaviour
{
    [SerializeField] Image itemIcon;
    [SerializeField] TMP_Text itemRank;
    
    public enum ItemRank
    {
        S=0, A=1, B=2
    }

    public enum ItemType
    {
        Weapon,
        Armor
    }

    public enum ItemPart
    {
        Weapon = 0,
        Helmet = 1,
        Strap = 2,
        Top = 3,
        Bottom = 4,
        Glove = 5,
        Boot = 6
    }

    [System.Serializable]
    public class Equipment
    {
        public ItemType itemType;
        public ItemRank itemRank;
        public ItemPart itempart;
        public int itemCode;
        public Sprite sprite;
    }
    public Equipment itemData;
    
    public void SetItem(ItemType itemType)
    {
        itemData.itemType = itemType;

        int ran = Random.Range(0, 10);

        // 아이템 등급 (S 10%, A 30%, B 60%)
        if(ran==0)
        {
            itemData.itemRank = ItemRank.S;
        }
        else if(ran<4)
        {
            itemData.itemRank = ItemRank.A;
        }
        else
        {
            itemData.itemRank = ItemRank.B;
        }

        // 아이템 타입,코드,스프라이트
        if (itemType==ItemType.Weapon)
        {
            itemData.itempart = ItemPart.Weapon;
            switch (itemData.itemRank)
            {
                case ItemRank.S:
                    itemData.itemCode = 0;
                    break;
                case ItemRank.A:
                    itemData.itemCode = 1;
                    break;
                case ItemRank.B:
                    itemData.itemCode = 2;
                    break;
            }
            itemData.sprite = Resources.Load<Sprite>($"Sprites/Weapon/{itemData.itemCode.ToString()}");
        }
        else
        {
            ran = Random.Range(1, 7);
            itemData.itemCode = ran-1;
            itemData.itempart = (ItemPart)ran;
            itemData.sprite = Resources.Load<Sprite>($"Sprites/Armor/{itemData.itemRank.ToString()}/{itemData.itemCode.ToString()}");
        }

        itemIcon.sprite = itemData.sprite;
        itemRank.text = itemData.itemRank.ToString();
    }

    public void Refresh()
    {
        itemIcon.sprite = itemData.sprite;
        itemRank.text = itemData.itemRank.ToString();
    }

    public void BtnDelete()
    {
        if (InventoryManager.instance.mode == InventoryManager.Mode.Delete)
        {
            InventoryManager.instance.inventory.Remove(this);
            Destroy(this.gameObject);
            InventoryManager.instance.RefreshCount();
        }
    }

}
