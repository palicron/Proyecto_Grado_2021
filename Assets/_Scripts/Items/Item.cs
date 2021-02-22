using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public int maxStack = 1;
    public GameObject pf = null;
    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }
}
