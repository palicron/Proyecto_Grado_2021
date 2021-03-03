using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{

    public Image icon;

    public Sprite defaultImage;

    ItemDescriptionUI itemDescriptionUI;

    GameObject equipUI;

    Equipment equipmentPiece;

    //public Button removeButton;

    private bool inStorage;

    public EquipSlotUI slotType;

    void Awake()
    {
        itemDescriptionUI = gameObject.GetComponentInParent<ItemDescriptionUI>();
        equipUI = GameObject.Find("Equipment");
    }

    public void EquipItem(Equipment newItem)
    {
        if(equipmentPiece!=null && newItem != equipmentPiece)
        {
            if(!Inventory.instance.Add(equipmentPiece))
            {
                return;
            }
        }
        equipmentPiece = newItem;
        icon.sprite = newItem.icon;
        icon.color = new Color32(255, 255, 255, 255);
        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        equipmentPiece = null;
        icon.sprite = defaultImage;
        icon.color = new Color32(255, 255, 255, 50);
        //removeButton.interactable = false;
    }

    //public void OnRemoveButton()
    //{
    //    GameObject player = GameObject.FindWithTag("Player");
    //    Vector3 playerPos = player.transform.position;
    //    Vector3 playerDirection = player.transform.forward;
    //    Quaternion playerRotation = player.transform.rotation;
    //    Vector3 spawnPos = new Vector3(playerPos.x, playerPos.y + 1, playerPos.z) + playerDirection * 1.4f;
    //    GameObject droppedItem = Instantiate(lItem.item.pf, spawnPos, Quaternion.identity);
    //    Rigidbody droppedItemRB = droppedItem.GetComponent<Rigidbody>();
    //    droppedItemRB.AddForce(player.transform.forward * 8f, ForceMode.Impulse);
    //    removeItem();
    //}

    public void removeItem()
    {
        if (equipmentPiece != null)
        {
            if (Inventory.instance.Add(equipmentPiece))
            {
                EquipmentManager.instance.Unequip((int)slotType);
            }
        }
    }

    public void UseItem()
    {
        removeItem();
    }

}

public enum EquipSlotUI { Head, Chest, Legs, Feet, Tool, Tool2 }