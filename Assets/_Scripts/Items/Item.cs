using UnityEngine;

public enum ItemType
{
    Default,
    Consumable,
    Paper,
    Plastic,
    Glass,
    Metal,
    Equipment
}
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public enum Rarity
    {
        Com�n,
        Raro,
        UltraRaro,
        Legendario,
        �nico
    }
    public int id = -1;
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isMaterial = false;
    public int maxStack = 1;
    public GameObject pf = null;
    public string description = "This is a great item!";
    public string extraInfo = "Extra info";
    public ItemType type = ItemType.Default;
    public Rarity rarity = Rarity.Com�n;
    public virtual bool Use()
    {
        Debug.Log("Using " + name);
        return false;
    }
}
