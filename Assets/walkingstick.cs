using UnityEngine;

public class walkingstick : MonoBehaviour
{
    public GameObject walkingStickObject;   
    public GameObject visibilityCone;   
    private bool isHeld = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            isHeld = !isHeld;
            walkingStickObject.SetActive(isHeld);
            visibilityCone.SetActive(isHeld);
        }
        
    }

}