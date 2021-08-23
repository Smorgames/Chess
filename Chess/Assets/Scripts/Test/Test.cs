﻿using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public static void ShowPossibleTurns(Square square)
    {
        List<Square> turns = square.PieceOnThis.GetPossibleAttackTurns(square);

        foreach (var turn in turns)
            turn.Activate();
    }

    public static void DebugTestInfo(string data)
    {
        Debug.Log(data + Time.realtimeSinceStartup);
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