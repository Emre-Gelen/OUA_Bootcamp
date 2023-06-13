using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<UIElement> _UIElements;

    public void UpdateUI()
    {
        foreach(UIElement element in _UIElements)
        {
            element.OnUIUpdate();
        }
    }
}