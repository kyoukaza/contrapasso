using UnityEngine;
using UnityEngine.UI;

public class StaminaBarUI : MonoBehaviour
{
    public Image fillImage;

    public void UpdateStaminaBar(float normalizedValue)
    {
        fillImage.fillAmount = Mathf.Clamp01(normalizedValue);
    }
}
