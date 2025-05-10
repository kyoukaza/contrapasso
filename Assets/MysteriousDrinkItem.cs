using UnityEngine;

public class MysteriousDrinkItem : MonoBehaviour, IInventoryItem
{
    public void Use()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        if (stats != null)
            stats.HealToFull();
    }

    public void OnDrop() { }
}