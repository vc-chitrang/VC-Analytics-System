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
            _e.name = d.Key;
            string message = $"{d.Key.ToUpper()}: <b>{d.Value}</b>";
            _e.SetText(message);
        }
    }
}
