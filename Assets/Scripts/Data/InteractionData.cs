using UnityEngine;

[System.Serializable]
public class InteractionData
{
    public string inputName;
    public string interactionName;
    public float seconds;
    public int countGrabInteraction;
    public int countReleaseInteraction;
    public int countTouchInteraction;

    public InteractionData() { }

    public InteractionData(string name)
    {
        interactionName = name;
    }
    
    public InteractionData(string inputName, string name, float seconds, int countGrabInteraction, int countReleaseInteraction, int countTouchInteraction)
    {
        interactionName = name;
        this.seconds = seconds;
        this.countGrabInteraction = countGrabInteraction;
        this.inputName = inputName;
    }
    
    public void SetInputName(string name)
    {
        inputName = name;
    }

    public string GetInputName()
    {
        return inputName;
    }

    public void SetName(string name)
    {
        interactionName = name;
    }

    public string GetName()
    {
        return interactionName;
    }

    public void SetSeconds(float seconds)
    {
        this.seconds = seconds;
    }

    public float GetSeconds()
    {
        return seconds;
    }

    public void SetCountGrabInteraction(int countGrabInteraction)
    {
        this.countGrabInteraction = countGrabInteraction;
    }

    public int GetCountGrabInteraction()
    {
        return countGrabInteraction;
    }

    public void SetCountReleaseInteraction(int countReleaseInteraction)
    {
        this.countReleaseInteraction = countReleaseInteraction;
    }

    public int GetCountReleaseInteraction()
    {
        return countReleaseInteraction;
    }
    
    public void SetCountTouchInteraction(int countTouchInteraction)
    {
        this.countTouchInteraction = countTouchInteraction;
    }

    public int GetCountTouchInteraction()
    {
        return countTouchInteraction;
    }

    public void PrintInteractionData()
    {
        Debug.Log("Name: " + interactionName + " Seconds: " + seconds + " Count Grab Interaction: " + countGrabInteraction);
    }
}
