using System;
using UnityEngine;

public class ImageController : MonoBehaviour
{
    [SerializeField] private GameObject imageObject;
    private Material _material;

    private void Awake()
    {
        MeshRenderer renderer = imageObject.GetComponent<MeshRenderer>();
        _material = renderer.material;
        _material = Instantiate(_material);
        renderer.material = _material;
    }

    public void SetImage(string path, Size size)
    {
        var fileContent = System.IO.File.ReadAllBytes(path);
        var tex = new Texture2D(2, 2);
        ImageConversion.LoadImage(tex, fileContent);
        _material.mainTexture = tex;
        imageObject.transform.localScale = new Vector3(size.Width, size.Height, 1);
        Debug.Log(imageObject.transform.localScale);
    }
}
