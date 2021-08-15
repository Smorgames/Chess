using UnityEngine;

public class Test : MonoBehaviour
{
    public void ChangeTurn()
    {
        GameManager.Instance.TriggerChangeTurn();
        Debug.Log(GameManager.Instance.WhoseTurn);
    }
}