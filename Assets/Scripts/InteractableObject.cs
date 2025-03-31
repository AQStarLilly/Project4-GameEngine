using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractableObject : MonoBehaviour
{
    public enum InteractionType
    {
        Nothing,
        Pickup,
        Info,
        Dialogue
    }

    private DialogueManager dialogueManager;

    [Header("Type of Interactable")]
    public InteractionType interType;

    [Header("Simple info message")]
    public string infoMessage;
    public TMP_Text infoText;
    public float fadeDuration = 1f;

    [Header("Dialogue Text")]
    [TextArea] public string[] sentences;


    public void Awake()
    {
        dialogueManager = GetComponent<DialogueManager>();
    }

    public void Interact()
    {
        //Debug.Log("Interacting with object: " + gameObject.name + " Currently set to: " + interType.ToString());

        switch(interType)
        {
            case InteractionType.Nothing:
                Nothing();
                break;
            case InteractionType.Pickup:
                Pickup();
                break;
            case InteractionType.Info:
                Info();
                break;
            case InteractionType.Dialogue:
                Dialogue();
                break;
        }
    }

    public void Nothing()
    {
        Debug.Log("Interaction type not defined");
    }

    public void Pickup()
    {
        if (interType == InteractionType.Pickup)
        {
            Debug.Log("You picked up a " + gameObject.name);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("Tried to pick up an object but failed");
        }
    }

    public void Info()
    {
        if(infoText != null)
        {
            StopAllCoroutines();
            infoText.text = infoMessage;

            Color c = infoText.color;
            c.a = 1f;
            infoText.color = c;

            StartCoroutine(FadeOutInfo());
        }
        else
        {
            Debug.LogWarning("infoText missing");
        }
    }

    private IEnumerator FadeOutInfo()
    {
        yield return new WaitForSeconds(1f);
        float elapsed = 0f;
        Color originalColor = infoText.color;

        while(elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            infoText.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        infoText.text = "";
    }

    public void Dialogue()
    {
        if (Singleton.Instance.dialogueManager != null)
        {
            Singleton.Instance.dialogueManager.StartDialogue(sentences);
        }
        else
        {
            Debug.LogError("DialogueManager not assigned in the Singleton. Please check the GameManager.");
        }
    }
}
