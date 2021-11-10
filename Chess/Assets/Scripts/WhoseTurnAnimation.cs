using TMPro;
using UnityEngine;

public class WhoseTurnAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tmp;

    private void ChangeTMP(string whoseTurnCode)
    {
        var text = whoseTurnCode == "w" ? "White" : "Black";
        var color = whoseTurnCode == "w" ? Color.white : Color.black;

        _tmp.color = color;
        _tmp.text = $"{text}'s turn!";
    }
    
    private void OnTurnOrderChanged(object sender, TurnOrderEventArgs e)
    {
        var whoseTurn = Game.Instance.WhoseTurn;
        ChangeTMP(whoseTurn);
    }

    private void Start()
    {
        Game.OnTurnOrderChanged += OnTurnOrderChanged;
    }

    private void OnDestroy()
    {
        Game.OnTurnOrderChanged -= OnTurnOrderChanged;
    }
}