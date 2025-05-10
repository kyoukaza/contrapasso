using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PickupItem : MonoBehaviour
{
    private bool canBePickedUp = true;
    private InventorySystem playerInventory;

    void Start()
    {
        GetComponent<Collider>().isTrigger = true;
        playerInventory = FindObjectOfType<InventorySystem>();
    }

    void OnTriggerStay(Collider other)
    {
        if (!canBePickedUp || other.tag != "Player") return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            bool added = playerInventory.TryAddToInventory(this.gameObject);
            if (added)
                Destroy(gameObject);
        }
    }

    public string GetItemType()
    {
        return gameObject.name.ToLower();
    }
}
