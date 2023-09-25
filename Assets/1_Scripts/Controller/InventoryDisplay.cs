using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public bool initialized = false;
    public List<InventoryCard> listCard;
    public InventoryCard inventoryCard;
    public Transform rootCard;
    private void OnEnable()
    {
        if (!initialized)
        {
            foreach (var item in GCMng.inst.listSkin)
            {
                InventoryCard card = Instantiate(inventoryCard, rootCard);
                card.SetData(item.ID);

                listCard.Add(card);
            }

            initialized = true;
        }
        else
        {
            RefreshCards();
        }
    }
    public void SetData()
    {
        gameObject.SetActive(true);
    }
    public void RefreshCards()
    {
        foreach (var item in listCard)
        {
            item.Refresh();
        }
    }
}
