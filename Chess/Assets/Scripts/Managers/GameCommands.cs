using UnityEngine;
using UnityEngine.SceneManagement;

public class GameCommands : MonoBehaviour
{
    public void RestartGame() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}