using UnityEngine;

[System.Serializable]
public class InteractionData
{
    public string _inputName;
    public string _interactionName;
    public float _seconds;
    public int _countGrabInteraction;
    public int _countReleaseInteraction;
    public int _countTouchInteraction;

    public InteractionData() { }

    public InteractionData(string name)
    {
        _interactionName = name;
    }
    
    public InteractionData(string inputName, string name, float seconds, int countGrabInteraction, int countReleaseInteraction, int countTouchInteraction)
    {
        _interactionName = name;
        _seconds = seconds;
        _countGrabInteraction = countGrabInteraction;
        _inputName = inputName;
    }
    
    public void SetInputName(string name)
    {
        _inputName = name;
    }

    public string GetInputName()
    {
        return _inputName;
    }

    public void SetName(string name)
    {
        _interactionName = name;
    }

    public string GetName()
    {
        return _interactionName;
    }

    public void SetSeconds(float seconds)
    {
        _seconds = seconds;
    }

    public float GetSeconds()
    {
        return _seconds;
    }

    public void SetCountGrabInteraction(int countGrabInteraction)
    {
        _countGrabInteraction = countGrabInteraction;
    }

    public int GetCountGrabInteraction()
    {
        return _countGrabInteraction;
    }

    public void SetCountReleaseInteraction(int countReleaseInteraction)
    {
        _countReleaseInteraction = countReleaseInteraction;
    }

    public int GetCountReleaseInteraction()
    {
        return _countReleaseInteraction;
    }
    
    public void SetCountTouchInteraction(int countTouchInteraction)
    {
        _countTouchInteraction = countTouchInteraction;
    }

    public int GetCountTouchInteraction()
    {
        return _countTouchInteraction;
    }

    public void PrintInteractionData()
    {
        Debug.Log("Name: " + _interactionName + " Seconds: " + _seconds + " Count Grab Interaction: " + _countGrabInteraction);
    }
}
