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

    public void SetImage(string path)
    {
        var fileContent = System.IO.File.ReadAllBytes(path);
        var tex = new Texture2D(2, 2);
        ImageConversion.LoadImage(tex, fileContent);
        _material.mainTexture = tex;
    }
}
