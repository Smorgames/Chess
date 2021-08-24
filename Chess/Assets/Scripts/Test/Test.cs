using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public static Test Instance;

    public LineRenderer Line;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }

        ResetLine();
    }

    public static void ShowPossibleTurns(Square square)
    {
        List<Square> turns = square.PieceOnThis.GetPossibleAttackTurns(square);

        foreach (var turn in turns)
            turn.Activate();
    }

    public void ConnectTwoPieceWithLine(Piece first, Piece second)
    {
        Line.SetPosition(0, first.transform.position);
        Line.SetPosition(1, second.transform.position);
    }

    public void ResetLine()
    {
        Line.SetPosition(0, Vector3.zero);
        Line.SetPosition(1, Vector3.zero);
    }

    public static void DebugTestInfo(string data)
    {
        Debug.Log(data + $" {Time.realtimeSinceStartup}");
    }

    public void ChangeTurn()
    {
        GameManager.Instance.TriggerChangeTurn();
        Debug.Log(GameManager.Instance.WhoseTurn);
    }

    public void ShowAttackSquares()
    {
        GameManager.Instance.VerifyIfIsCheck();
    }
}