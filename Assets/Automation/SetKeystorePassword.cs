#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
public class SetKeystorePassword
{
    static SetKeystorePassword ()
    {
        PlayerSettings.Android.keystorePass = "realityVersus";
        PlayerSettings.Android.keyaliasName = "realityversus";
        PlayerSettings.Android.keyaliasPass = "realityVersus";
    }
}
#endif