using UnityEngine;

public class FloorChange : MonoBehaviour
{
    public Material triggeredMaterial;
    public string[] triggerTags = { "Player", "Screech_Radius", "Stick_Radius"};

    private Renderer rend;
    private Material originalMaterial;
    private bool hasChanged = false;

    void Start()
    {
        rend = GetComponent<Renderer>();

        if (rend != null)
        {
            originalMaterial = rend.material;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasChanged && IsValidTrigger(other))
        {
            if (triggeredMaterial != null && rend != null)
            {
                rend.material = triggeredMaterial;
                hasChanged = true;
            }
        }
    }

    private bool IsValidTrigger(Collider other)
    {
        foreach (string tag in triggerTags)
        {
            if (other.CompareTag(tag)) return true;
        }
        return false;
    }

    public void ResetFloor()
{
    if (rend != null && originalMaterial != null)
    {
        rend.material = originalMaterial;
        hasChanged = false;
    }
}

}
