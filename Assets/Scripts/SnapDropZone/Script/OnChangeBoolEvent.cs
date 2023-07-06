using UnityEngine;

public class OnChangeBoolEvent : MonoBehaviour
{
    private bool _myBool = false;

    public event OnVariableChangeDelegate OnVariableChange;

    public delegate void OnVariableChangeDelegate(bool newVal);

    public bool MyBool
    {
        get
        {
            return _myBool;
        }
        set
        {
            if (_myBool == value) return;
            
            _myBool = value;
            if (OnVariableChange != null)
                OnVariableChange(_myBool);
        }
    }
}
