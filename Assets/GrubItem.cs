using UnityEngine;

public class GrubItem : MonoBehaviour, IInventoryItem
{
    public void Use()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        if (stats != null)
            stats.Heal(1);
    }

    public void OnDrop() { }
}