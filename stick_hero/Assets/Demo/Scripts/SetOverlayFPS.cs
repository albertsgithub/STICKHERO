using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SetOverlayFPS : MonoBehaviour {

    Texture tex;
    private float fps = 60;
    public Text text1;
    private int count = 60;
    GameObject CenterPointer;
    Camera LCamera;
    Camera RCamera;
    Camera UICamera;
    int textID = 0;
    void Start()
    {
        tex = Resources.Load("star") as Texture;
        LCamera = GameObject.Find("MojingMain/MojingVrHead/VR Camera Left").GetComponent<Camera>();
        RCamera = GameObject.Find("MojingMain/MojingVrHead/VR Camera Right").GetComponent<Camera>();
        CenterPointer = GameObject.Find("MojingMain/MojingVrHead/GazePointer");
        UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
        RenderTexture UIScreen = new RenderTexture(Screen.width, Screen.height, 24, RenderTextureFormat.Default);
        UIScreen.anisoLevel = 0;
        UIScreen.antiAliasing = Mathf.Max(QualitySettings.antiAliasing, 1);

        UICamera.GetComponent<Camera>().targetTexture = UIScreen;
        textID = UICamera.GetComponent<Camera>().targetTexture.GetNativeTextureID();
    }

    void Update()
    {
        count++;
        float interp = Time.deltaTime / (0.5f + Time.deltaTime);
        float currentFPS = 1.0f / Time.deltaTime;
        if (count >= 60)
        {
            fps = Mathf.Lerp(fps, currentFPS, interp);
            text1.text = "FPS:" + Mathf.RoundToInt(fps).ToString();
            count = 0;
        }
        if (ConfigItem.bUseTimeWarp || Mojing.SDK.NeedDistortion)
        {
#if !UNITY_EDITOR
                MojingSDK.Unity_SetOverlay3D(3, textID, 1, 1, CenterPointer.transform.position.magnitude);
#endif
        }
    }

    void OnGUI()
    {
        GUI.skin.label.fontSize = 45;
        if ((!ConfigItem.bUseTimeWarp) && (!Mojing.SDK.NeedDistortion) && Mojing.SDK.VRModeEnabled)
        {
            GUI.DrawTexture(new Rect(LCamera.WorldToScreenPoint(CenterPointer.transform.position).x - 25, LCamera.WorldToScreenPoint(CenterPointer.transform.position).y - 25, 50, 50), tex);
            GUI.DrawTexture(new Rect(RCamera.WorldToScreenPoint(CenterPointer.transform.position).x - 25, RCamera.WorldToScreenPoint(CenterPointer.transform.position).y - 25, 50, 50), tex);
            GUI.Label(new Rect(LCamera.WorldToScreenPoint(CenterPointer.transform.position).x - 300, LCamera.WorldToScreenPoint(CenterPointer.transform.position).y - 300, 300, 50), text1.text);
            GUI.Label(new Rect(RCamera.WorldToScreenPoint(CenterPointer.transform.position).x - 300, RCamera.WorldToScreenPoint(CenterPointer.transform.position).y - 300, 300, 50), text1.text);
        }
        else if (!Mojing.SDK.VRModeEnabled)
        {
            GUI.DrawTexture(new Rect(0.5f * Screen.width - 25, 0.5f * Screen.height - 25, 50, 50), tex);
            GUI.Label(new Rect(0.5f * Screen.width - 300, 0.5f * Screen.height - 300, 300, 50), text1.text);
        }
    }

    void OnDestroy()
    {
#if !UNITY_EDITOR
            MojingSDK.Unity_SetOverlay3D(3, 0, 1, 1, 1);
#endif
    }
}
