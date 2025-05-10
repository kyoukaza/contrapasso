using UnityEngine;
using UnityEngine.UI;

public class HeartUI : MonoBehaviour
{
    public Sprite heartFull;
    public Sprite heartEmpty;

    private Image[] heartImages;

    void Awake()
    {
        heartImages = GetComponentsInChildren<Image>();
    }

    public void UpdateHearts(int current, int max)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < current)
                heartImages[i].sprite = heartFull;
            else
                heartImages[i].sprite = heartEmpty;

            heartImages[i].enabled = i < max;
        }
    }
}
