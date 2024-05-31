using UnityEngine;

[CreateAssetMenu]
public class Drop : ScriptableObject
{
    public Color DropColor;
    public string DropName;
    public int DropChance;

    public Drop (Color dropColor, string dropName, int dropChance)
    {
        DropColor = dropColor;
        DropName = dropName;
        DropChance = dropChance;
    }
}
