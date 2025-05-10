using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [Header("UI References")]
    public Image[] slotImages;           
    public GameObject[] slotHighlights; 

    [Header("Icons")]
    public Sprite emptyIcon;
    public Sprite dragonflyIcon;
    public Sprite beetleIcon;
    public Sprite grubIcon;
    public Sprite drinkIcon;
    public Sprite bellIcon;

    public void UpdateUI(GameObject[] inventory, int selectedSlot)
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            
            if (inventory[i] != null)
            {
                string itemName = inventory[i].name.ToLower();
                if (itemName.Contains("dragonfly")) slotImages[i].sprite = dragonflyIcon;
                else if (itemName.Contains("beetle")) slotImages[i].sprite = beetleIcon;
                else if (itemName.Contains("grub")) slotImages[i].sprite = grubIcon;
                else if (itemName.Contains("drink")) slotImages[i].sprite = drinkIcon;
                else if (itemName.Contains("bell")) slotImages[i].sprite = bellIcon;
                else slotImages[i].sprite = emptyIcon;
            }
            else
            {
                slotImages[i].sprite = emptyIcon;
            }

           
            if (slotHighlights != null && i < slotHighlights.Length)
            {
                slotHighlights[i].SetActive(i == selectedSlot);
            }
        }
    }
}
