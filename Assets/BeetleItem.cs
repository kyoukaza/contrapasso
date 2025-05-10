using UnityEngine;

public class BeetleItem : MonoBehaviour, IInventoryItem
{
    public void Use()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        if (stats != null)
            stats.RestoreStamina();
    }

    public void OnDrop() { }
}
