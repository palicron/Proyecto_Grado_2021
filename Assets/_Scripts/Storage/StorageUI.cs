using UnityEngine;

public class StorageUI : MonoBehaviour
{ 

    public Transform itemsParent;

    Storage storage;

    StorageSlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        storage = Storage.instance;
        storage.onStorageChangedCallBack += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<StorageSlot>();
    }

    // Update is called once per frame
    void Update()
    {  
        //if player opens chest
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < storage.items.Count)
            {
                slots[i].AddItem(storage.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
