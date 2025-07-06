#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class EasyAdManagerUltimate : MonoBehaviour
{
    [MenuItem("Tools/www.anysourcecode.com")]
    public static void ToolsMenu()
    {
        string url = "https://www.anysourcecode.com";
        Application.OpenURL(url);
    }
}
#endif
