using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;

    ItemDescriptionUI itemDescriptionUI;

    public GameObject equipUI;

    ListItem lItem;

    public Button removeButton;

    public Text itemQuantity;

    private bool inStorage;

    Storage storage;

    void Awake()
    {
        itemDescriptionUI = gameObject.GetComponentInParent<ItemDescriptionUI>();
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
        Vector3 spawnPos = new Vector3(playerPos.x,playerPos.y+1,playerPos.z) + playerDirection * 1.4f;
        GameObject droppedItem = Instantiate(lItem.item.pf, spawnPos, Quaternion.identity);
        Rigidbody droppedItemRB = droppedItem.GetComponent<Rigidbody>();
        droppedItemRB.AddForce(player.transform.forward * 8f, ForceMode.Impulse);
        removeItem();
    }

    public void removeItem()
    {
        if (lItem.quantity >= 2)
        {
            --lItem.quantity;
            if (lItem.quantity >= 2)
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
            if(inStorage)
            {
                if (storage.Add(lItem.item))
                {
                    removeItem();
                }
            }
            else 
            {
                if (lItem.item.type == ItemType.Equipment && equipUI.activeSelf)
                {
                    if (lItem.item.Use())
                    {
                        removeItem();
                    }
                }
                else
                {
                    itemDescriptionUI.setItem(lItem.item);
                }
            }
        }
    }

    public void SetStorage(Storage pStorage)
    {
        storage = pStorage;
        if(storage!=null)
        {
            inStorage = true;
        }
        else
        {
            inStorage = false;
        }
    }
}
