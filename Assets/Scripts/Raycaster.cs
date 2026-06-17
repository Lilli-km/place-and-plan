using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;

public class Raycaster : MonoBehaviour
{
    private void Update()
    {
        var pressed = false;
        Vector2 screenPosition = default;


        if (Touchscreen.current != null)
        {
            var primary = Touchscreen.current.primaryTouch;
            if (primary.press.wasPressedThisFrame)
            {
                pressed = true;
                screenPosition = primary.position.ReadValue();
            }
        }
        else if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            pressed = true;
            screenPosition = Mouse.current.position.ReadValue();
        }

        if (pressed)
        {
            DoRaycast(screenPosition);
        }
    }

    private void DoRaycast(Vector2 screenPosition)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(screenPosition), out hit))
        {
            ButtonCallback.ButtonHit(hit);
        }
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}
