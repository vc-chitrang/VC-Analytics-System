using System.Collections.Generic;
using UnityEngine;

public class ReusableScrollPanel : MonoBehaviour
{
    [SerializeField] private TextElement textElement;
    [SerializeField] private RectTransform content;
    public void Display(Dictionary<string, object> data)
    {
        foreach (var d in data)
        {
            TextElement _e = Instantiate(textElement, content);
            string message = $"{d.Key.ToUpper()}: {d.Value}";
            _e.SetText(message);
            Debug.Log(message);
        }
    }
}
