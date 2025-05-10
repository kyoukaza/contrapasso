using UnityEngine;

public class SoundRing : MonoBehaviour
{
    public float expandSpeed = 10f;
    public float maxSize = 5f;
    public float duration = 0.5f;

    private float timeElapsed;

    void Update()
    {
        float scale = Mathf.Lerp(0, maxSize, timeElapsed / duration);
        transform.localScale = new Vector3(scale, 0.1f, scale);
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= duration)
            Destroy(gameObject);
    }
}
