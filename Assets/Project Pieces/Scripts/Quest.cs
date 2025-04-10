[System.Serializable]
public class Quest
{
    public string questName;
    public bool isComplete;
    public string requiredItem;   // Item needed
    public int requiredAmount = 1; 
    public string rewardItem;     // Item given back
}
