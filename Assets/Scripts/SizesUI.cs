using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Size
{
    public int Width, Height, SizeUIValue;

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
    public static SizesUI Instance;
    private UIDocument _uiDocument;

    private VisualElement _container;

    private RadioButtonGroup _radioGroup;
    private Button _button;
    
    public delegate void SizesUICallback(Size size);

    private SizesUICallback _callback;


    private void Awake()
    {
        Instance = this;
        _uiDocument = GetComponent<UIDocument>();
        if (_uiDocument == null) Debug.LogError("UIDocument not found");

        _container = _uiDocument.rootVisualElement.Q<VisualElement>("Container");
        _radioGroup = _uiDocument.rootVisualElement.Q<RadioButtonGroup>("RadioButtons");
        _button = _uiDocument.rootVisualElement.Q<Button>("Button");
        _button.RegisterCallback<ClickEvent>(ConfirmSize);
        
        _container.AddToClassList("hidden");
    }

    public void ShowSizes(SizesUICallback callback, int defaultValue = 0)
    {
        _callback = callback;
        _radioGroup.value = defaultValue;
        _container.RemoveFromClassList("hidden");
    }

    private void ConfirmSize(ClickEvent evt)
    {
        string selected = _radioGroup.choices.ElementAt(_radioGroup.value);
        string[] selection = selected.Substring(0, selected.Length - 2).Split("x");
        _callback(new Size
        {
            Width = int.Parse(selection[0]),
            Height = int.Parse(selection[1]),
            SizeUIValue = _radioGroup.value
        });
        _container.AddToClassList("hidden");
    }
}
