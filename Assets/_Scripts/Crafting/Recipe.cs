    using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Crafting/Recipe")]
public class Recipe : ScriptableObject
{
    public int[] requirement = { 0, 0, 0, 0 };

    public Item item;

    public bool CraftObject()
    {
       return Inventory.instance.Add(item);
    }

    public int[] IsCraftable(int[] pScore)
    {
        int[] dummyArray;
        dummyArray = (int[]) pScore.Clone();
        for(int i = 0; i < dummyArray.Length; i++)
        {
            dummyArray[i] += -(requirement[i]);
            if(dummyArray[i]<0)
            {
                return new int[0];
            }
        }
        if(!CraftObject()) return new int[1];
        return dummyArray;
    }
}
