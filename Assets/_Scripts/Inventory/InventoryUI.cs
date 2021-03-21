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
        inventory.onItemChangedCallBack += UpdateUI;
        inventory.onStorageCallBack += UpdateStorage;
        inventory.onPocketAddedCallback += UpdatePocketUI;
        InventorySlot[] principalSlots = itemsParent.GetComponentsInChildren<InventorySlot>();
        InventorySlot[] extraSlots1 = extraBag1.GetComponentsInChildren<InventorySlot>();
        InventorySlot[] extraSlots2 = extraBag2.GetComponentsInChildren<InventorySlot>();
        slots = new InventorySlot[(principalSlots.Length + extraSlots1.Length + extraSlots2.Length)];
        principalSlots.CopyTo(slots, 0);
        extraSlots1.CopyTo(slots, principalSlots.Length);
        extraSlots2.CopyTo(slots, extraSlots1.Length + principalSlots.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            bool active = !inventoryUI.activeSelf;
            inventoryUI.SetActive(active);
            UI_Status.instance.SetOpen(active, MenuType.Inventory);
        }
    }

    void UpdatePocketUI()
    {
        extraBag1.SetActive(inventory.isThereExtraBag(1));
        extraBag2.SetActive(inventory.isThereExtraBag(2));
    }

    void UpdateUI()
    {
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
