using UnityEngine;

[RequireComponent(typeof(Collider))]
public class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameObject PickupTrigger;

    public int slotCount = 4;
    private GameObject[] slots;
    private int selectedSlot = 0;

    private GameObject hoveredItem;

    [SerializeField] private InventoryUIController uiController;

    void Awake()
    {
        slots = new GameObject[slotCount];

        if (PickupTrigger != null){
            Collider col = PickupTrigger.GetComponent<Collider>();
            if (col != null) col.isTrigger = true;
    }


        if (uiController == null)
            uiController = FindObjectOfType<InventoryUIController>();

        UpdateUI();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickupItem>())
            hoveredItem = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if (hoveredItem == other.gameObject)
            hoveredItem = null;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && hoveredItem != null)
        {
            TryAddToInventory(hoveredItem);
            hoveredItem = null;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectSlot(0);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectSlot(1);
        if (Input.GetKeyDown(KeyCode.Alpha4)) SelectSlot(2);
        if (Input.GetKeyDown(KeyCode.Alpha5)) SelectSlot(3);

        if (Input.GetKeyDown(KeyCode.E)) UseSelectedItem();

        if (Input.GetKeyDown(KeyCode.Q)) DropSelectedItem();
    }

    public bool TryAddToInventory(GameObject item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                item.SetActive(false);
                AudioManager.Instance.PlayItemPickup();
                UpdateUI();
                return true;
            }
        }

        return false;
    }

    void SelectSlot(int index)
    {
        if (index >= 0 && index < slots.Length)
        {
            selectedSlot = index;
            UpdateUI();
        }
    }

    void UseSelectedItem()
    {
        GameObject itemObj = slots[selectedSlot];
        if (itemObj == null) return;

        var item = itemObj.GetComponent<IInventoryItem>();
        if (item != null)
        {
            item.Use();
            slots[selectedSlot] = null;
            AudioManager.Instance.PlayItemUse();
            UpdateUI();
        }
    }

    void DropSelectedItem()
    {
        GameObject itemObj = slots[selectedSlot];
        if (itemObj == null) return;

        slots[selectedSlot] = null;

        Vector3 dropPosition = new Vector3(transform.position.x + transform.forward.x, 0.2f, transform.position.z + transform.forward.z);

        itemObj.transform.position = dropPosition;

        itemObj.SetActive(true);

        var item = itemObj.GetComponent<IInventoryItem>();
        if (item != null)
            item.OnDrop();
        AudioManager.Instance.PlayItemDrop();
        UpdateUI();
    }

    void UpdateUI()
    {
        if (uiController != null)
        {
            uiController.UpdateUI(slots, selectedSlot);
        }
    }

    public GameObject[] GetSlots() => slots;
}