using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    public playermovement movement;
    public PlayerHealth health;

    public void Heal(int amount)
    {
        if (health != null)
            health.Heal(amount);
    }

    public void HealToFull()
    {
        if (health != null)
            health.HealToFull();
    }

    public void RestoreStamina()
    {
        if (movement != null)
            movement.currentStamina = movement.maxStamina;
    }

    public IEnumerator TemporarySpeedBoost(float multiplier, float duration)
    {
        if (movement == null) yield break;

        float originalSpeed = movement.moveSpeed;
        float originalSprint = movement.sprintMultiplier;

        movement.moveSpeed *= multiplier;
        movement.sprintMultiplier *= multiplier;

        yield return new WaitForSeconds(duration);

        movement.moveSpeed = originalSpeed;
        movement.sprintMultiplier = originalSprint;
    }
}
