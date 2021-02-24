using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum ItemType
    {
        Equipment,
        Consumable,
        Metal,
        Plastic,
        Paper,
        Default
    }
    public enum Rarity
    {
        Common,
        Rare,
        VeryRare,
        UltraRare
    }
    public InventoryUI ui;
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isMaterial = false;
    public int maxStack = 1;
    public GameObject pf = null;
    public string description = "This is a great item!";
    public ItemType type = ItemType.Default;
    public Rarity rarity = Rarity.Common;
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}
