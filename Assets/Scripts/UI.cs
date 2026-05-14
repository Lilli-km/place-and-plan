using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private UIDocument _uiDocument;

    private VisualElement _editModeVe;
    private VisualElement _mainModeVe;
    private bool editMode = false;

    private Button _editButton;
    private Button _exitEditButton;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (_uiDocument == null) Debug.LogError("UIDocument not found");

        _mainModeVe = _uiDocument.rootVisualElement.Q<VisualElement>("MainMode");
        _editModeVe = _uiDocument.rootVisualElement.Q<VisualElement>("EditMode");
        _editModeVe.AddToClassList("hidden");

        _editButton = _uiDocument.rootVisualElement.Q<Button>("EditButton");
        _editButton.RegisterCallback<ClickEvent>(StartEdit);
        _exitEditButton = _uiDocument.rootVisualElement.Q<Button>("ExitEditButton");
        _exitEditButton.RegisterCallback<ClickEvent>(ExitEdit);
    }

    private void StartEdit(ClickEvent evt)
    {
        editMode = true;
        _mainModeVe.AddToClassList("hidden");
        _editModeVe.RemoveFromClassList("hidden");
    }

    private void ExitEdit(ClickEvent evt)
    {
        editMode = false;
        _editModeVe.AddToClassList("hidden");
        _mainModeVe.RemoveFromClassList("hidden");
    }
}