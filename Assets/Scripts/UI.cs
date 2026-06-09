using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private UIDocument _uiDocument;

    private VisualElement _editModeVe;
    private VisualElement _mainModeVe;
    private VisualElement _placeModeVe;
    private bool editMode = false;
    private bool placeMode = false;

    private Button _editButton;
    private Button _exitEditButton;
    private Button _addButton;
    private Button _placeButton;

    [SerializeField] private GameObject xrOrigin;
    private PlaceImage _placeImage;
    private ARController _arController;

    private void Awake()
    {
        _uiDocument = GetComponent<UIDocument>();
        if (_uiDocument == null) Debug.LogError("UIDocument not found");

        _mainModeVe = _uiDocument.rootVisualElement.Q<VisualElement>("MainMode");
        _editModeVe = _uiDocument.rootVisualElement.Q<VisualElement>("EditMode");
        _placeModeVe = _uiDocument.rootVisualElement.Q<VisualElement>("PlaceMode");
        _editModeVe.AddToClassList("hidden");
        _placeModeVe.AddToClassList("hidden");

        _editButton = _uiDocument.rootVisualElement.Q<Button>("EditButton");
        _editButton.RegisterCallback<ClickEvent>(StartEdit);
        _exitEditButton = _uiDocument.rootVisualElement.Q<Button>("ExitEditButton");
        _exitEditButton.RegisterCallback<ClickEvent>(ExitEdit);
        _addButton = _uiDocument.rootVisualElement.Q<Button>("AddButton");
        _addButton.RegisterCallback<ClickEvent>(AddImage);
        _placeButton = _uiDocument.rootVisualElement.Q<Button>("PlaceButton");
        _placeButton.RegisterCallback<ClickEvent>(PlaceImage);
        
        _placeImage = xrOrigin.GetComponent<PlaceImage>();
        _arController = xrOrigin.GetComponent<ARController>();
        _arController.HideGrid();
    }

    private void StartEdit(ClickEvent evt)
    {
        editMode = true;
        _mainModeVe.AddToClassList("hidden");
        _editModeVe.RemoveFromClassList("hidden");
        _arController.ShowGrid();
    }

    private void ExitEdit(ClickEvent evt)
    {
        editMode = false;
        _editModeVe.AddToClassList("hidden");
        _mainModeVe.RemoveFromClassList("hidden");
        _arController.HideGrid();
    }

    private void AddImage(ClickEvent evt)
    {
        if (evt.target != _addButton) return;

        NativeFilePicker.PickFile(FilePicked, "image/*");
    }

    private void FilePicked(string path)
    {
        _placeImage.AddImage(path);
        placeMode = true;
        _editModeVe.AddToClassList("hidden");
        _placeModeVe.RemoveFromClassList("hidden");
    }

    private void PlaceImage(ClickEvent evt)
    {
        if (evt.target != _placeButton) return;
        
        _placeImage.PlaceImageInSpace();
        
        placeMode = false;
        _placeModeVe.AddToClassList("hidden");
        _editModeVe.RemoveFromClassList("hidden");
    }
}