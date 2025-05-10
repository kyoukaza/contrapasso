using UnityEngine;

public class SoundRingEffect : MonoBehaviour
{
    private float targetSize;
    private float expandTime;
    private float timer = 0f;
    private Vector3 initialScale;
    private bool expanding = false;

    public void StartExpand(float size, float duration)
    {
        targetSize = size;
        expandTime = duration;
        initialScale = transform.localScale;
        expanding = true;
    }

    void Update()
    {
        if (!expanding) return;

        timer += Time.deltaTime;
        float progress = Mathf.Clamp01(timer / expandTime);
        float scale = Mathf.Lerp(initialScale.x, targetSize, progress);

        transform.localScale = new Vector3(scale, transform.localScale.y, scale);

        if (progress >= 1f)
        {
            expanding = false;
            Destroy(gameObject); // Ring disappears after full expansion
        }
    }
}
