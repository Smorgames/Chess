using UnityEngine;

public class Test : MonoBehaviour
{
    public void ChangeTurn()
    {
        GameManager.Instance.ChangeTurn();
        Debug.Log(GameManager.Instance.WhoseTurn);
    }
}