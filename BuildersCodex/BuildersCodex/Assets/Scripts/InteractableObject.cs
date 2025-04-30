using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public void OnInteract()
    {
        Debug.Log("Interacted with: " + gameObject.name);
        // Add your custom interaction logic here:
        // - Open doors (animation)
        // - Play sounds
        // - Pick up items
    }
}