using UnityEngine;

public class HighlightOnReveal : MonoBehaviour
{
    [Header("Highlight Settings")]
    public Material highlightMaterial;
    public Renderer[] partsToHighlight;

    private bool isHighlighted = false;
    private string[] triggerTags = { "Stick_Radius", "Screech_Radius" };

   void OnTriggerEnter(Collider other)
{
     if (!isHighlighted && IsValidTrigger(other))
    {
        ApplyHighlight();
        isHighlighted = true;
    }
}


    void ApplyHighlight()
    {
        foreach (Renderer part in partsToHighlight)
        {
            if (part != null)
                part.material = highlightMaterial;
        }
    }

    bool IsValidTrigger(Collider other)
{
    foreach (string tag in triggerTags)
    {
        if (other.CompareTag(tag))
            return true;
    }

    return false;
}

}
