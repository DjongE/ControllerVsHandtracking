using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;

public class Television : MonoBehaviour
{
    [Header("Channel Switcher")]
    public List<GameObject> channels;
    public GameObject actualChannelName;
    private List<string> channelNames;
    private int actualChannelNumber;
    
    [Header("Channel 1")]
    public string channel1Name;
    public GameObject channel1Template;
    public GameObject channel1GridObjectContainer;
    //public ScrollingObjectCollection channel1Soc;
    //private GridObjectCollection _channel1GridObjectCollection;
    private List<string> _channel1ColostomaActionDescription;
    private List<GameObject> _channel1Actions = new List<GameObject>();
    List<GameObject> actionsToRemove = new List<GameObject>();

    [Header("Channel 2")]
    public string channel2Name;
    //public Procedure proc;
    public GameObject channel2Template;
    public GameObject channel2GridObjectContainer;
   // public ScrollingObjectCollection channel2Soc;
    //private GridObjectCollection _channel2GridObjectCollection;

    [Header("Channel 3")]
    public string channel3Name;
    //public Timer timer;
    public GameObject txtTime;
    
    private void Start()
    {
        //ChannelSwitcher
        channelNames = new List<string>();
        channelNames.Add(channel1Name);
        channelNames.Add(channel2Name);
        channelNames.Add(channel3Name);
        
        actualChannelNumber = 0;
        SwitchToChannelNumber(actualChannelNumber);

        //Channel 1
        //_channel1GridObjectCollection = channel1GridObjectContainer.GetComponent<GridObjectCollection>();
        _channel1ColostomaActionDescription = new List<string>();
        
        //Channel 2
        //_channel2GridObjectCollection = channel2GridObjectContainer.GetComponent<GridObjectCollection>();
    }

    private void Update()
    {
        UpdateTimerStatistik();
    }
    
    //Television Channel Switch
    #region ChannelSwitch

    public void NextChannel()
    {
        print("ChannelCounts: " + (channels.Count));
        actualChannelNumber = (actualChannelNumber + 1) % (channels.Count);
        print("Actual Channel Number: " + actualChannelNumber);
        SwitchToChannelNumber(actualChannelNumber);
    }

    public void PreviousChannel()
    {
        print("ChannelCounts: " + (channels.Count));
        //actualChannelNumber = (actualChannelNumber - 1) % (channels.Count + 1);
        actualChannelNumber--;
        actualChannelNumber = (actualChannelNumber % channels.Count + channels.Count) % channels.Count;
        print("Actual Channel Number: " + actualChannelNumber);
        SwitchToChannelNumber(actualChannelNumber);
    }

    public void SwitchToChannelNumber(int channelNumber)
    {
        foreach (GameObject channel in channels)
        {
            if (channels.IndexOf(channel) == channelNumber)
            {
                channel.SetActive(true);
                actualChannelName.GetComponent<TextMeshPro>().SetText(channelNames[actualChannelNumber]);
            }
            else
            {
                channel.SetActive(false);
            }
        }
    }

    #endregion

    //Sender 1 (Durchgeführte Maßnahmen in Reihenfolge)

    #region Channel 1

    public void AddChannel1ColostomaAction(string colostomaActionDescription)
    {
        _channel1ColostomaActionDescription.Add(colostomaActionDescription);
        CreateChannel1TextMeshPro(colostomaActionDescription);
    }

    private void CreateChannel1TextMeshPro(string colostomaActionDescription)
    {
        GameObject inst = Instantiate(channel1Template, channel1GridObjectContainer.transform);
        _channel1Actions.Add(inst);

        inst.GetComponent<TextMeshPro>().SetText(colostomaActionDescription);


        StartCoroutine(UpdateGridLayout());
        //channel1Soc.UpdateContent();
        //_channel1GridObjectCollection.UpdateCollection();

    }

    // remove text mesh with text for next step
    public void RemoveTextForNextStep()
    {
        _channel1ColostomaActionDescription.RemoveAll(s => s.Contains("nächster Schritt"));

        //foreach (TextMeshPro tmp in _channel1GridObjectCollection.GetComponentsInChildren<TextMeshPro>())
        foreach (GameObject action in _channel1Actions)
        {
            if (action.GetComponent<TextMeshPro>() && 
                (action.GetComponent<TextMeshPro>().text.Contains("nächster Schritt") 
                || action.GetComponent<TextMeshPro>().text.Contains("     ")))
            {
                actionsToRemove.Add(action);
            }
        }

        foreach(GameObject actionToRemove in actionsToRemove)
        {
            _channel1Actions.Remove(actionToRemove);
            Destroy(actionToRemove);
        }

        actionsToRemove.Clear();

        

        StartCoroutine(UpdateGridLayout());
        //channel1Soc.UpdateContent();
        //_channel1GridObjectCollection.UpdateCollection();
    }

    private void RemoveTextMeshPro()
    {
        
    }

    private IEnumerator UpdateGridLayout()
    {
        
        yield return new WaitForEndOfFrame();
        /*
        if(_channel1GridObjectCollection != null)
            _channel1GridObjectCollection.UpdateCollection();

        yield return new WaitForEndOfFrame();
        if (channel1Soc != null)
        {
            //channel1Soc.UpdateContent();
            //channel1Soc.MoveByTiers(2, true);
            //channel1Soc.MoveToIndex(_channel1Actions.Count, true);
        }*/
    }
    
    #endregion
    
    //Sender 2 (Korrekte Reihenfolge der Maßnahmen)
    #region Channel 2

    public void AddChannel2ColostomaActionList()
    {/*
        print(proc.ActionList.Count);
        if (proc.ActionList != null)
        {
            foreach (ColostomaAction colostomaAction in proc.ActionList)
            {
                CreateChannel2TextMeshPro(colostomaAction.GetActionDescription());
            }
        }*/
    }

    private void CreateChannel2TextMeshPro(string colostomaActionDescription)
    {
        GameObject inst = Instantiate(channel2Template, channel2GridObjectContainer.transform);
        
        inst.GetComponent<TextMeshPro>().SetText(colostomaActionDescription);
        //channel2Soc.UpdateContent();
        //_channel2GridObjectCollection.UpdateCollection();
    }
    
    #endregion
    
    //Sender 3 (Statistik)
    #region Channel 3

    private void UpdateTimerStatistik()
    {
        //txtTime.GetComponent<TextMeshPro>().SetText(timer.GetTimeStampInMinutesAndSeconds());
    }
    
    #endregion
}
