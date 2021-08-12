using UnityEngine;

[System.Serializable]
public class ColorData
{
    public PieceColor Color { get { return _color; } }
    [SerializeField] private PieceColor _color;

    public int Multiplier 
    { 
        get 
        {
            if (_color == PieceColor.Black)
                return -1;
            else if (_color == PieceColor.White)
                return 1;
            return 0;
        } 
    }
    private int _colorMultiplier;
}

[System.Serializable]
public enum PieceColor
{
    Black,
    White,
    Green,
    Blue,
    Red,
    Yellow,
    Purple,
    Orange
}