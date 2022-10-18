using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemSlotManager : MonoBehaviour
{
    [SerializeField] Image imageSlot;
    [SerializeField] TMP_Text textRank;
    [SerializeField] GameObject gameObjectToggle;
    [SerializeField] GameObject gameObjectEquip;

    public InventoryManager.ItemData itemData;
    public bool isChoose = false;
    public bool isRight = false;
    public bool isEquip = false;

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            isRight = true;
        }
        else
            isRight = false;
    }

    public void SetSlot(InventoryManager.ItemData _itemData)
    {
        itemData = _itemData;
        imageSlot.sprite = Resources.Load<Sprite>($"Sprites/{itemData.itemType}/{itemData.itemCode}");
        textRank.text = itemData.itemRank.ToString();
       
    }

    public void OnSlot()
    {
        if (isChoose)
        {
            isChoose = !isChoose;
            gameObjectToggle.SetActive(false);
        }
        else
        {
            if (!isEquip)
            {
                isChoose = !isChoose;
                gameObjectToggle.SetActive(true);
            }
        }
        InventoryManager.instance.CheckSlotChoose();
    }

    public void Equip()
    {
        if (isRight)
        {
            if (isEquip)
            {
                isEquip = false;
                gameObjectEquip.SetActive(false);
            }
            else
            {
                foreach (var item in InventoryManager.instance.itemSlots)
                {
                    if (item.itemData.itemPart == itemData.itemPart)
                    {
                        item.isEquip = false;
                        item.gameObjectEquip.SetActive(false);
                    }
                }
                isEquip = true;
                gameObjectEquip.SetActive(true);
            }
            
        }
    }
    
}
