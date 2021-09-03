using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGameStateParser
{
    public string Token { get; set; }

    private void Parse()
    {
        Token = Token.Trim(' ');
        
        for (int i = 0; i < Token.Length; i++)
        {
            if (i % 4 == 0)
            {
                string xString = Token[i].ToString();
                int x;
                bool xStringIsInt = Int32.TryParse(xString, out x);

                if (!xStringIsInt)
                    throw new SystemException($"The assumed x coordinate is not an int");
            }

            if (i % 4 == 1)
            {
                string yString = Token[i].ToString();
                int y;
                bool yStringIsInt = Int32.TryParse(yString, out y);

                if (!yStringIsInt)
                    throw new SystemException($"The assumed y coordinate is not an int");
            }

            if (i % 4 == 2)
            {
                string colorString = Token[i].ToString();

                if (colorString == "w")
                {
                    // Set piece color = White
                }
                else
                {
                    // Raise error: Incorrect color
                }

                if (colorString == "b")
                {
                    // Set piece color = Black
                }
                else
                {
                    // Raise error: Incorrect color
                }
            }

            if (i % 4 == 3)
            {
                char letter = Token[i];
                string pieceString = letter.ToString();

                switch (pieceString)
                {
                    case "p":
                        // pawn
                        break;
                    case "r":
                        // rook
                        break;
                    case "k":
                        // knight
                        break;
                    case "b":
                        // bishop
                        break;
                    case "Q":
                        // queen
                        break;
                    case "K":
                        // king
                        break;
                    default:
                        // Raise error
                        break;
                }
            }
        }
    }
}