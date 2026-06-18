using System;
using System.Collections.Generic;
using UnityEngine;

public class ImageManager : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject imageTemplate;
    [SerializeField] private LayerMask layerMask;

    private bool isImageHolding;
    private GameObject currentImage;

    private List<ImageController> images = new List<ImageController>();

    public void AddImage(string path, Size size)
    {
        if (isImageHolding) return;
        
        isImageHolding = true;
        currentImage = Instantiate(imageTemplate);
        ImageController controller = currentImage.GetComponent<ImageController>();
        controller.SetImage(path, size);
        controller.manager = this;
        images.Add(controller);
    }

    public void HoldExistingImage(ImageController image)
    {
        isImageHolding = true;
        currentImage = image.gameObject;
        HideImageButtons();
    }

    public void PlaceImageInSpace()
    {
        currentImage = null;
        isImageHolding = false;
        ShowImageButtons();
    }

    public void DeleteImage(ImageController image)
    {
        Destroy(image.gameObject);
        images.Remove(image);
    }
    
    void FixedUpdate()
    {
        if (!isImageHolding) return;
        
        var transform = camera.transform;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                1, layerMask))
        {
            currentImage.transform.position = hit.point;
            currentImage.transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
        }
    }

    public void ShowImageButtons()
    {
        foreach (ImageController image in images)
        {
            image.ShowButtons();
        }
    }

    public void HideImageButtons()
    {
        foreach (ImageController image in images)
        {
            image.HideButtons();
        }
    }
}

