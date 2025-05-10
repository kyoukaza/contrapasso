using UnityEngine;

public class TagFloors : MonoBehaviour
{
    public string tagToApply = "Floor";

    void Start()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>(true);
        int count = 0;

        foreach (GameObject obj in allObjects)
        {
            if (obj.name.ToLower().Contains("plane"))
            {
                obj.tag = tagToApply;
                count++;
            }
        }

        Debug.Log($"Tagged {count} objects as '{tagToApply}'");
    }
}
