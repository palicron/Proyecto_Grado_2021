using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;

    public GameObject inventoryUI;

    public GameObject extraBag1;

    public GameObject extraBag2;

    Inventory inventory;

    InventorySlot[] slots;

    bool isActiveBag1;

    bool isActiveBag2;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        isActiveBag1 = inventory.isThereExtraBag(1);
        isActiveBag2 = inventory.isThereExtraBag(2);
        inventory.onItemChangedCallBack += UpdateUI;
        inventory.onStorageCallBack += UpdateStorage;
        InventorySlot[] principalSlots = itemsParent.GetComponentsInChildren<InventorySlot>();
        InventorySlot[] extraSlots1 = extraBag1.GetComponentsInChildren<InventorySlot>();
        InventorySlot[] extraSlots2 = extraBag2.GetComponentsInChildren<InventorySlot>();
        slots = new InventorySlot[(principalSlots.Length + extraSlots1.Length + extraSlots2.Length)];
        principalSlots.CopyTo(slots, 0);
        extraSlots1.CopyTo(slots, principalSlots.Length);
        extraSlots2.CopyTo(slots, extraSlots1.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            bool active = !inventoryUI.activeSelf;  
            inventoryUI.SetActive(active);
            extraBag1.SetActive(active && isActiveBag1);
            extraBag2.SetActive(active && isActiveBag2);
        }
    }

    void UpdateUI()
    {
        isActiveBag1 = inventory.isThereExtraBag(1);
        isActiveBag2 = inventory.isThereExtraBag(2);
        for (int i=0;i<slots.Length;i++)
        {
            if(i<inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                if(slots[i]!=null)
                {
                    slots[i].ClearSlot();
                }
            }
        }
    }
    void UpdateStorage()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i]!=null)
            {
                slots[i].SetStorage(inventory.storage);
            }
        }
    }
}
