public class ChessCode
{
    public readonly string Value;

    public ChessCode(string value)
    {
        Value = value;
    }

    public override string ToString() => Value;
}