using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    public string npcName;

    [Header("Before Quest Complete Dialogue")]
    [TextArea] public string[] defaultDialogue;

    [Header("After Quest Complete Dialogue")]
    [TextArea] public string[] completedDialogue;

    public Quest quest;

    public void Interact()
    {
        var inventory = FindObjectOfType<SimpleInventory>();

        if (!quest.isComplete)
        {
            // Check if player has enough of required item
            if (inventory.GetItemCount(quest.requiredItem) >= quest.requiredAmount)
            {
                // Remove required amount
                inventory.RemoveItem(quest.requiredItem, quest.requiredAmount);
                inventory.AddItem(quest.rewardItem);

                quest.isComplete = true;

                Singleton.Instance.dialogueManager.StartDialogue(new string[] {
                "Thanks for the " + quest.requiredItem + "!",
                "Here, take this " + quest.rewardItem + "."
            });
            }
            else
            {
                Singleton.Instance.dialogueManager.StartDialogue(defaultDialogue);
            }
        }
        else
        {
            Singleton.Instance.dialogueManager.StartDialogue(completedDialogue);
        }
    }
}