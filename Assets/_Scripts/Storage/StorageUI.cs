using UnityEngine;
using UnityEngine.UI;

public class StorageUI : MonoBehaviour
{ 

    public GameObject itemsParent;

    Storage storage;

    StorageSlot[] slots;

    Color32 blue = new Color32(83, 101, 255, 240);

    Color32 red = new Color32(212, 115, 173, 240);
    
    Color32 green = new Color32(119, 212, 173, 240);

    Color32 yellow = new Color32(193, 212, 119, 240);

    // Start is called before the first frame update
    void Start()
    {
        storage = Storage.instance;
        if(storage!=null)
        {
            storage.onStorageChangedCallBack += UpdateUI;
            slots = itemsParent.GetComponentsInChildren<StorageSlot>();
        }
    }

    // Update is called once per frame
    void Update()
    {  
        //if player opens chest
    }

    void UpdateUI()
    {
        switch ((int)storage.storageType)
        {
            case 1:
                itemsParent.GetComponent<Image>().color = blue;
                break;
            case 2:
                itemsParent.GetComponent<Image>().color = yellow;
                break;
            case 3:
                itemsParent.GetComponent<Image>().color = green;
                break;
            case 4:
                itemsParent.GetComponent<Image>().color = red;
                break;
        }
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
