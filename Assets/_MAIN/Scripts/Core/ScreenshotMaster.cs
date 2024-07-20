using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotMaster : MonoBehaviour
{
    public static Texture2D CaptureScreenshot(int width,  int height, float supersize = 1, string filePath = "") => CaptureScreenshot(Camera.main, width, height, supersize, filePath); 
    public static Texture2D CaptureScreenshot(Camera cam,int width,  int height, float supersize = 1, string filePath = "")
    {
        /*if(supersize != 1)
        {
            width = Mathf.RoundToInt(width * supersize);
            height = Mathf.RoundToInt(height * supersize);
        }

        RenderTexture rt = RenderTexture.GetTemporary(width, height, 32);
        cam.targetTexture = rt;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.ARGB32, false);

        cam.Render();

        RenderTexture.active = rt;

        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        cam.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        if (filePath != "")
            SaveScreenshotToFile(screenshot, filePath);

        return screenshot;*/
        RenderTexture rt = RenderTexture.GetTemporary(width, height, 32);
        cam.targetTexture = rt;

        Texture2D screenshot = new Texture2D(width, height, TextureFormat.ARGB32, false);

        cam.Render();

        RenderTexture.active = rt;

        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);

        cam.targetTexture = null;
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        // Scale down the screenshot if supersize is not 1
        if (supersize != 1)
        {
            int newWidth = Mathf.RoundToInt(width * supersize);
            int newHeight = Mathf.RoundToInt(height * supersize);
            screenshot = ScaleTexture(screenshot, newWidth, newHeight);
        }

        if (filePath != "")
        {
            SaveScreenshotToFile(screenshot, filePath);
        }

        return screenshot;
    }

    public enum ImageType { PNG, JPG }

    public static void SaveScreenshotToFile(Texture2D screenshot, string filePath, ImageType fileType = ImageType.PNG)
    {
        byte[] bytes = new byte[0];
        string extension = "";

        switch(fileType)
        {
            case ImageType.PNG:
                bytes = screenshot.EncodeToPNG();
                extension = ".png";
                break;
            case ImageType.JPG:
                bytes = screenshot.EncodeToJPG();
                extension = ".jpg";
                break;
        }

        if(!filePath.Contains('.'))
        {
            filePath = filePath + extension;
        }

        FileManager.TryCreateDirectoryFromPath(filePath);

        System.IO.File.WriteAllBytes(filePath, bytes);
    }

    private static Texture2D ScaleTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        Texture2D result = new Texture2D(targetWidth, targetHeight, source.format, false);
        Color[] pixels = result.GetPixels(0);
        float incX = 1.0f / targetWidth;
        float incY = 1.0f / targetHeight;

        for (int px = 0; px < pixels.Length; px++)
        {
            pixels[px] = source.GetPixelBilinear(incX * (px % targetWidth), incY * (px / targetWidth));
        }

        result.SetPixels(pixels, 0);
        result.Apply();
        return result;
    }

}
