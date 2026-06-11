using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class TakePhotos : MonoBehaviour
{
    public void TakePhoto()
    {
        StartCoroutine(TakeAPhoto());
    }
    
    IEnumerator TakeAPhoto()
    {
        yield return new WaitForEndOfFrame();
        Camera camera = Camera.main;
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        camera.targetTexture = rt;
        
        // The Render Texture in RenderTexture.active is the one
        // that will be read by ReadPixels.
        var currentRT = RenderTexture.active;
        RenderTexture.active = rt;

        // Render the camera's view.
        camera.Render();

        // Make a new texture and read the active Render Texture into it.
        Texture2D image = new Texture2D(Screen.width, Screen.height);
        image.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        image.Apply();

        camera.targetTexture = null;

        // Replace the original active Render Texture.
        RenderTexture.active = currentRT;

        byte[] bytes = image.EncodeToJPG();
        string fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".jpg";

        NativeGallery.SaveImageToGallery(bytes, "AR-Bilderprojektion", fileName);
        
        Destroy(rt);
        Destroy(image);
    }
}
