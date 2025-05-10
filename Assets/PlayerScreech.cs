using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerScreech : MonoBehaviour
{
    public GameObject screechProjectilePrefab;
    public Transform shootPoint;
    public float cooldownDuration = 10f;

    private float cooldownTimer = 0f;
    private bool isOnCooldown = false;

    [Header("UI")]
    public Image cooldownFill;
    public TextMeshProUGUI cooldownText;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isOnCooldown)
        {
            UseScreech();
        }

        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;

            cooldownFill.fillAmount = cooldownTimer / cooldownDuration;
            cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();

            if (cooldownTimer <= 0)
            {
                isOnCooldown = false;
                cooldownFill.fillAmount = 0f;
                cooldownText.text = "";
            }
        }
    }

    void UseScreech()
    {
        Instantiate(screechProjectilePrefab, shootPoint.position, shootPoint.rotation);

        AudioManager.Instance.PlayPlayerScreech();

        isOnCooldown = true;
        cooldownTimer = cooldownDuration;
        cooldownFill.fillAmount = 1f;
        cooldownText.text = cooldownDuration.ToString("F0");
    }
}
