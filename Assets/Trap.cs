using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public GameObject untriggeredModel;
    public GameObject triggeredModel;
    public float destroyDelay = 2f;

    private bool hasTriggered = false;

    void Start()
    {
        if (untriggeredModel != null) untriggeredModel.SetActive(true);
        if (triggeredModel != null) triggeredModel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            hasTriggered = true;

            if (untriggeredModel != null) untriggeredModel.SetActive(false);
            if (triggeredModel != null) triggeredModel.SetActive(true);

            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
                health.TakeDamage(1);
            AudioManager.Instance.PlayTrapTrigger();
            Destroy(gameObject, destroyDelay);
        }
    }
}
