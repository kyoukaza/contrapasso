using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ladder : MonoBehaviour
{
    [Header("Fade")]
    public Image fadeOverlay;
    public float fadeDuration = 1f;

     [Header("Floor Update")]
    public Material newFloorMaterial;

    [Header("Wall Generator")]
    public WallGenerator wallGenerator;

    private bool triggered = false;
    public void Initialize(WallGenerator generator, Image fadeOverlay)
{
    this.wallGenerator = generator;
    this.fadeOverlay = fadeOverlay;
}

    void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(HandleExitTransition());
        }
    }

    IEnumerator HandleExitTransition()
    {
        
        playermovement playerMovement = FindObjectOfType<playermovement>();
        if (playerMovement != null)
            playerMovement.movementLocked = true;

            AudioManager.Instance.PlayLadderUse();

        yield return StartCoroutine(FadeToBlack());

        UpdateFloorMaterials();

        ClearTaggedObjects("Wall");
        ClearTaggedObjects("Item");
        ClearTaggedObjects("Trap");

        if (wallGenerator != null)
            wallGenerator.Generate();

        yield return StartCoroutine(FadeFromBlack());

        if (playerMovement != null)
            playerMovement.movementLocked = false;

        Destroy(gameObject);

    }

    IEnumerator FadeToBlack()
    {
        float t = 0f;
        Color color = fadeOverlay.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, t / fadeDuration);
            fadeOverlay.color = color;
            yield return null;
        }
    }

    IEnumerator FadeFromBlack()
    {
        float t = 0f;
        Color color = fadeOverlay.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeOverlay.color = color;
            yield return null;
        }
    }


    void ClearTaggedObjects(string tag)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject obj in targets)
        {
            Destroy(obj);
        }
    }

      void UpdateFloorMaterials()
{
    GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
    foreach (GameObject floor in floors)
    {
        FloorChange changer = floor.GetComponent<FloorChange>();
        if (changer != null)
        {
            changer.ResetFloor();
        }
        else
        {
            Renderer renderer = floor.GetComponent<Renderer>();
            if (renderer != null && renderer.sharedMaterial != newFloorMaterial)
            {
                renderer.sharedMaterial = newFloorMaterial;
            }
        }
    }
}

}
