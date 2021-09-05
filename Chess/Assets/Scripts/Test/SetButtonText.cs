using UnityEngine;
using TMPro;

public class SetButtonText : MonoBehaviour
{
    void Start()
    {
        TextMeshProUGUI TMP = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        string name = gameObject.name;

        TMP.fontSize = 15f;
        TMP.text = name;
    }
}