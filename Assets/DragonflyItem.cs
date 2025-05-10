using UnityEngine;

public class DragonflyItem : MonoBehaviour, IInventoryItem
{
    public float duration = 5f;
    public float speedMultiplier = 1.5f;

    public void Use()
    {
        PlayerStats stats = FindObjectOfType<PlayerStats>();
        if (stats != null)
            stats.StartCoroutine(stats.TemporarySpeedBoost(speedMultiplier, duration));
    }

    public void OnDrop() { }
}
