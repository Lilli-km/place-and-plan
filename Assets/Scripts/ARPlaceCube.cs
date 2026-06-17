using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;

public class ARPlaceCube : MonoBehaviour
{
    [SerializeField] private ARRaycastManager raycastManager;
    [SerializeField] private ARPlaneManager planeManager;
    private bool isPlacing;

    // Update is called once per frame
    private void Update()
    {
        if (!raycastManager) return;
        if (isPlacing) return;

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
            isPlacing = true;
            PlaceObject(screenPosition);
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

    private void PlaceObject(Vector2 touchPosition)
    {
        var rayHits = new List<ARRaycastHit>();
        raycastManager.Raycast(touchPosition, rayHits);

        if (rayHits.Count > 0)
        {
            var hitPosePosition = rayHits[0].pose.position;
            var hitPoseRotation = rayHits[0].pose.rotation;
            Instantiate(raycastManager.raycastPrefab, hitPosePosition, hitPoseRotation);
        }

        StartCoroutine(SetIsPlacingToFalseWithDelay());
    }

    private IEnumerator SetIsPlacingToFalseWithDelay()
    {
        yield return new WaitForSeconds(0.25f);
        isPlacing = false;
    }

    public static void Meow()
    {
        Console.WriteLine("meow");
    }
}