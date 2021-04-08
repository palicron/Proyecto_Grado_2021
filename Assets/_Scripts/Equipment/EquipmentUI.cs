using UnityEngine;

public class EquipmentUI : MonoBehaviour
{
    public Transform itemsParent;

    public GameObject equipmentUI;

    EquipmentManager equipment;

    EquipmentSlot[] slots;

    // Start is called before the first frame update

    void Start()
    {
        equipment = EquipmentManager.instance;
        equipment.onItemEquipedCallBack += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<EquipmentSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Equipment"))
        {
            bool active = !equipmentUI.activeSelf;
            equipmentUI.SetActive(active);
            UI_Status.instance.SetOpen(active, MenuType.Equipment);
            UI_SFX.instance.PlayEquipment(active);
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            int actualIndex = (int)slots[i].slotType;
            if(equipment.currentEquipment[actualIndex] == null)
            {
                slots[i].ClearSlot();
                continue;
            }
            slots[i].EquipItem(equipment.currentEquipment[actualIndex]);
        }
    }
}
