using System;
using UnityEngine;

public class ButtonCallback : MonoBehaviour
{
    public enum ButtonActions
    {
        Close,
        Resize
    }
    public delegate void ButtonHitCallback();

    public ButtonHitCallback buttonHitCallback;

    public static void ButtonHit(RaycastHit hit)
    {
        GameObject parent = hit.collider.gameObject.transform.parent.gameObject;
        ButtonCallback callback = parent.GetComponent<ButtonCallback>();
        if (callback == null) return;
        callback.buttonHitCallback();
    }
}
