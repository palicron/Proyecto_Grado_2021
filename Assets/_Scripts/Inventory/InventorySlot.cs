using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;

    public ItemDescriptionUI itemDescriptionUI;

    public GameObject descUI;

    ListItem lItem;

    public Button removeButton;

    public Text itemQuantity;


    void Start()
    {
        descUI.SetActive(false);
    }

    public void AddItem(ListItem newItem)
    {
        lItem = newItem;
        icon.sprite = lItem.item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
        if(lItem.quantity>1)
        {
            itemQuantity.text = "" + lItem.quantity;
        }
    }

    public void ClearSlot ()
    {
        lItem = null;
        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
        itemQuantity.text = "";
    }

    public void OnRemoveButton()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 playerPos = player.transform.position;
        Vector3 playerDirection = player.transform.forward;
        Quaternion playerRotation = player.transform.rotation;
        Vector3 spawnPos = playerPos + playerDirection * 1.4f;
        GameObject droppedItem = Instantiate(lItem.item.pf, spawnPos, Quaternion.identity);
        Rigidbody droppedItemRB = droppedItem.GetComponent<Rigidbody>();
        droppedItemRB.AddForce(player.transform.forward * 8f, ForceMode.Impulse);
        if(lItem.quantity>1)
        {
            --lItem.quantity;
            if(lItem.quantity>1)
            {
                itemQuantity.text = "" + lItem.quantity;
            }
            else
            {
                itemQuantity.text = "";
            }
        }
        else
        {
            Inventory.instance.Remove(lItem);
        }
    }

    public void UseItem()
    {
        if(lItem != null)
        {
            if(lItem.item.isMaterial)
            {
                itemDescriptionUI.setItem(lItem.item);
            }
            else
            {
                lItem.item.Use();
            }
        }
    }
}
