using System;
using UnityEngine;
using UnityEngine.UI;

public class ImageController : MonoBehaviour
{
    [SerializeField] private GameObject imageObject;
    [SerializeField] private GameObject imageContainer;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject cornerTopLeft;
    [SerializeField] private GameObject cornerBottomRight;
    public ImageManager manager;
    private Material _material;
    private GameObject _closeButton;
    private GameObject _resizeButton;
    private Texture2D texture;
    private Size _size;

    private void Awake()
    {
        MeshRenderer renderer = imageObject.GetComponent<MeshRenderer>();
        _material = renderer.material;
        _material = Instantiate(_material);
        renderer.material = _material;
    }

    private void OnButtonClose()
    {
        manager.DeleteImage(this);
    }

    private void OnButtonResize()
    {
        manager.HideImageButtons();
        SizesUI.Instance.ShowSizes((size =>
        {
            size.RotateSize(texture.width, texture.height);
            imageContainer.transform.localScale = size.ToVector3();
            MoveButtons();
            manager.ShowImageButtons();
            _size = size;
        }), _size.SizeUIValue);
    }

    public void ShowButtons()
    {
        _closeButton.SetActive(true);
        _resizeButton.SetActive(true);
    }

    public void HideButtons()
    {
        _closeButton.SetActive(false);
        _resizeButton.SetActive(false);
    }

    public void SetImage(string path, Size size)
    {
        var fileContent = System.IO.File.ReadAllBytes(path);
        texture = new Texture2D(2, 2);
        ImageConversion.LoadImage(texture, fileContent);
        _material.mainTexture = texture;
        size.RotateSize(texture.width, texture.height);
        imageContainer.transform.localScale = size.ToVector3();
        _size = size;
        AddButtons();
    }

    private void AddButtons()
    {
        _closeButton = Instantiate(buttonPrefab, transform);
        _closeButton.GetComponent<ButtonCallback>().buttonHitCallback = OnButtonClose;
        
        _resizeButton = Instantiate(buttonPrefab, transform);
        _resizeButton.GetComponent<ButtonCallback>().SetIcon(ButtonCallback.ButtonActions.Resize);
        _resizeButton.GetComponent<ButtonCallback>().buttonHitCallback = OnButtonResize;
        
        MoveButtons();
    }

    private void MoveButtons()
    {
        _closeButton.transform.SetPositionAndRotation(cornerTopLeft.transform.position, cornerTopLeft.transform.rotation);
        _resizeButton.transform.SetPositionAndRotation(cornerBottomRight.transform.position, cornerBottomRight.transform.rotation);
    }
}
