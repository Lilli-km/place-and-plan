using System;
using UnityEngine;

public class ButtonCallback : MonoBehaviour
{
    public enum ButtonActions
    {
        Close,
        Resize,
        Move
    }
    
    [SerializeField] private GameObject buttonObject;
    [SerializeField] private Texture2D resizeTexture;
    [SerializeField] private Texture2D moveTexture;
    private static Material resizeMaterial;
    private static Material moveMaterial;
    
    public delegate void ButtonHitCallback();

    public ButtonHitCallback buttonHitCallback;

    public static void ButtonHit(RaycastHit hit)
    {
        GameObject parent = hit.collider.gameObject.transform.parent.gameObject;
        ButtonCallback callback = parent.GetComponent<ButtonCallback>();
        if (callback == null) return;
        callback.buttonHitCallback();
    }

    public void SetIcon(ButtonActions action)
    {
        var mesh = buttonObject.GetComponent<MeshRenderer>();
        switch (action)
        {
            case ButtonActions.Close:
                return; // default material
            case ButtonActions.Resize:
                if (resizeMaterial != null)
                {
                    mesh.material = resizeMaterial;
                }
                else
                {
                    var material = Instantiate(mesh.material);
                    material.mainTexture = resizeTexture;
                    mesh.material = material;
                    resizeMaterial = material;
                }
                break;
            case ButtonActions.Move:
                if (resizeMaterial != null)
                {
                    mesh.material = resizeMaterial;
                }
                else
                {
                    var material = Instantiate(mesh.material);
                    material.mainTexture = moveTexture;
                    mesh.material = material;
                    moveMaterial = material;
                }
                break;
        }
    }
}
