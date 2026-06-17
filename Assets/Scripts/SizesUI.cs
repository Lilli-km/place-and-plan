using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Size
{
    public int Width, Height;

    public void RotateSize(int width, int height)
    {
        // Make Width the bigger size
        if (Width < Height)
        {
            (Width, Height) = (Height, Width);
        }
        
        if (width > height)
        {
            return;
        }

        (Width, Height) = (Height, Width);
    }

    public Vector3 ToVector3()
    {
        return new Vector3((float)Width / 100, (float)Height / 100, 1);
    }
}

public class SizesUI : MonoBehaviour
{
    private UIDocument _uiDocument;
    [SerializeField] private UIController mainUI;

    private VisualElement _container;

    private RadioButtonGroup _radioGroup;
    private Button _button;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (_uiDocument == null) Debug.LogError("UIDocument not found");

        _container = _uiDocument.rootVisualElement.Q<VisualElement>("Container");
        _radioGroup = _uiDocument.rootVisualElement.Q<RadioButtonGroup>("RadioButtons");
        _button = _uiDocument.rootVisualElement.Q<Button>("Button");
        _button.RegisterCallback<ClickEvent>(ConfirmSize);
        
        _container.AddToClassList("hidden");
    }

    public void ShowSizes()
    {
        _radioGroup.value = 0;
        _container.RemoveFromClassList("hidden");
    }

    private void ConfirmSize(ClickEvent evt)
    {
        string selected = _radioGroup.choices.ElementAt(_radioGroup.value);
        string[] selection = selected.Substring(0, selected.Length - 2).Split("x");
        mainUI.CreateImage(new Size
        {
            Width = int.Parse(selection[0]),
            Height = int.Parse(selection[1])
        });
        _container.AddToClassList("hidden");
    }
}
