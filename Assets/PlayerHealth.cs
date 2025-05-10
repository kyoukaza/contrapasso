using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHearts = 4;
    public int currentHearts;

    private HeartUI heartUI;

    void Start()
    {
        currentHearts = maxHearts;
        heartUI = FindObjectOfType<HeartUI>();
        heartUI.UpdateHearts(currentHearts, maxHearts);
    }

    public void TakeDamage(int amount)
    {
        currentHearts -= amount;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
        heartUI.UpdateHearts(currentHearts, maxHearts);

        AudioManager.Instance.PlayPlayerDamaged();
        
        if (currentHearts <= 1)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void Heal(int amount)
    {
        currentHearts += amount;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
        heartUI.UpdateHearts(currentHearts, maxHearts);
    }

    public void HealToFull()
{
    currentHearts = maxHearts;
    heartUI.UpdateHearts(currentHearts, maxHearts);
}


}
