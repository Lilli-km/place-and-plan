using System;
using System.Resources;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARController : MonoBehaviour
{
    private ARPlaneManager _planeManager;

    private void Awake()
    {
        _planeManager = GetComponent<ARPlaneManager>();
    }

    public void ShowGrid()
    {
        _planeManager.planePrefab.SetActive(true);
        _planeManager.SetTrackablesActive(true);
    }

    public void HideGrid()
    {
        _planeManager.planePrefab.SetActive(false);
        _planeManager.SetTrackablesActive(false);
    }
}
