using UnityEngine;

public class Test : MonoBehaviour
{
    public void ChangeTurn()
    {
        GameManager.Instance.TriggerChangeTurn();
        Debug.Log(GameManager.Instance.WhoseTurn);
    }

    public void ShowAttackSquares()
    {
        GameManager.Instance.VerifyIfIsCheck();
        Debug.Log("Attack turns");
    }
}