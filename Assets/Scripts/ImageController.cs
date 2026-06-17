using System;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    [SerializeField] private GameObject imageObject;
    [SerializeField] private GameObject imageContainer;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject cornerTopLeft;
    public ImageManager manager;
    private Material _material;
    private GameObject closeButton;

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

    public void ShowButtons()
    {
        closeButton.SetActive(true);
    }

    public void HideButtons()
    {
        closeButton.SetActive(false);
    }

    public void SetImage(string path, Size size)
    {
        var fileContent = System.IO.File.ReadAllBytes(path);
        var tex = new Texture2D(2, 2);
        ImageConversion.LoadImage(tex, fileContent);
        _material.mainTexture = tex;
        imageContainer.transform.localScale = new Vector3(size.Width, size.Height, 1);
        closeButton = Instantiate(buttonPrefab, transform);
        closeButton.transform.SetPositionAndRotation(cornerTopLeft.transform.position, cornerTopLeft.transform.rotation);
        closeButton.GetComponent<ButtonCallback>().buttonHitCallback = OnButtonClose;

    }
}
