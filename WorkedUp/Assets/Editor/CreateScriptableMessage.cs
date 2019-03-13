using UnityEngine;
using UnityEditor;

public class CreateScriptableMessage
{
    [MenuItem("Assets/Create/Message")]
    public static void CreateAsset()
    {
        ScriptableObjectUtility.CreateAsset<MessageScriptable>();
    }
}