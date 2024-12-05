using TMPro;
using UnityEngine;

public class TextElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    public void SetText(string message){
        _text.text = message;
    }
}
