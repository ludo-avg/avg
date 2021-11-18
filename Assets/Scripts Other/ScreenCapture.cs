using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
public class ScreenCapture : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(0,0,20,20), ""))
        {
            CaptureScreenshot2();
        }
    }

    [Button("Cpature Screen")]
    Texture2D CaptureScreenshot2()
    {
        Rect rect = new Rect(Screen.width * 0f, Screen.height * 0f, Screen.width * 1f, Screen.height * 1f);
        // 先创建一个的空纹理，大小可根据实现需要来设置  
        Texture2D screenShot = new Texture2D((int)rect.width, (int)rect.height, TextureFormat.RGB24, false);

        // 读取屏幕像素信息并存储为纹理数据，  
        screenShot.ReadPixels(rect, 0, 0);
        screenShot.Apply();

        // 然后将这些纹理数据，成一个png图片文件  
        byte[] bytes = screenShot.EncodeToPNG();
        string filename = Application.dataPath + "/Screenshot.png";
        System.IO.File.WriteAllBytes(filename, bytes);
        Debug.Log(string.Format("截屏了一张图片: {0}", filename));

        // 最后，我返回这个Texture2d对象，这样我们直接，所这个截图图示在游戏中，当然这个根据自己的需求的。  
        return screenShot;
    }
}
