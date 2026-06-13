using System;
using UnityEngine;

public class PlaceImage : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private GameObject imageTemplate;
    [SerializeField] private LayerMask layerMask;

    private bool isImageHolding = false;
    private GameObject currentQuad;

    public void AddImage(string path, Size size)
    {
        if (isImageHolding) return;
        
        isImageHolding = true;
        currentQuad = Instantiate(imageTemplate);
        ImageController controller = currentQuad.GetComponent<ImageController>();
        controller.SetImage(path, size);
    }

    public void PlaceImageInSpace()
    {
        currentQuad = null;
        isImageHolding = false;
    }
    
    void FixedUpdate()
    {
        if (!isImageHolding) return;
        
        var transform = camera.transform;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                1, layerMask))
        {
            currentQuad.transform.position = hit.point;
            currentQuad.transform.rotation = Quaternion.FromToRotation(-Vector3.forward, hit.normal);
        }
    }
}

