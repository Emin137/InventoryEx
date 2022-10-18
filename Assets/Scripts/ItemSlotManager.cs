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

    public InventoryManager.ItemData itemData;
    public bool isChoose = false;

    public void SlotRefresh(InventoryManager.ItemData _itemData)
    {
        itemData = _itemData;
        imageSlot.sprite = Resources.Load<Sprite>($"Sprites/{itemData.itemType}/{itemData.itemCode}");
        textRank.text = itemData.itemRank.ToString();
       
    }

    public void OnSlot()
    {
        if(isChoose)
        {
            isChoose = !isChoose;
            gameObjectToggle.SetActive(false);
        }
        else
        {
            isChoose = !isChoose;
            gameObjectToggle.SetActive(true);
        }
        InventoryManager.instance.CheckSlotChoose();
    }
    
}
