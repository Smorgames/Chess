using UnityEngine;

[System.Serializable]
public class ColorData
{
    public bool IsBlack { get { return _isBlack; } }
    [SerializeField] private bool _isBlack;

    public int Multiplier 
    { 
        get 
        {
            if (_isBlack)
                return -1;
            else
                return 1;
        } 
    }
    private int _colorMultiplier;
}