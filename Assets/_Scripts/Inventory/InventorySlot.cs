using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;

    ListItem lItem;

    public Button removeButton;

    public Text itemQuantity;

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
        Vector3 spawnPos = playerPos + playerDirection * 1.8f;
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
            lItem.item.Use();
        }
    }
}
