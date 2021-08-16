using System.Collections.Generic;

public class PiecePossibleTurns
{
    public Piece Piece { get => _piece; set => _piece = value; }
    private Piece _piece;
    public List<Square> MoveTurns { get => _moveTurns; set => _moveTurns = value; }
    private List<Square> _moveTurns = new List<Square>();
    public List<Square> AttackTurns { get => _attackTurns; set => _attackTurns = value; }
    private List<Square> _attackTurns = new List<Square>();

    private List<Square> _possibleTurns;

    public List<Square> GetAllPossibleTurns()
    {
        List<Square> turns = new List<Square>();

        turns.AddRange(_moveTurns);
        turns.AddRange(_attackTurns);

        return turns;
    }
}