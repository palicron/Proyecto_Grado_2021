using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public int requiredPaper;

    public int requiredPlastic;

    public int requiredGlass;

    public int requiresMetal;

    public Item craftedItem;

    public void CraftObject()
    {
        Inventory.instance.Add(craftedItem);
    }

}
