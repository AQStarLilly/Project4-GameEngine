using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public GameObject currentInteractable = null;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && currentInteractable != null)
        {
            InteractableObject interactable = currentInteractable.GetComponent<InteractableObject>();

            if (interactable != null) // Prevent NullReferenceException
            {
                interactable.Interact();
            }
            else
            {
                Debug.LogWarning("The object doesn't have an InteractableObject component!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interact"))
        {
            currentInteractable = other.gameObject; // Store the interactable object
            Debug.Log($"Entered interactable: {currentInteractable.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interact"))
        {
            if (currentInteractable == other.gameObject)
            {
                currentInteractable = null; // Clear only if it's the same object
                Debug.Log("Exited interactable zone.");
            }
        }
    }
}
