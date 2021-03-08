using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEditor.UIElements;
using UnityEngine;
using Utils.CodeGen;

public class EventCreator : EditorWindow
{
    static string eventName = "";
    string eventDataName = "";
    GameEvent gameEvent;

    [Serializable]
    public class FieldDescription
    {
        public string Type;
        public string Name;
    }

    [MenuItem("Window/EventCreator")]
    private static void OpenWindow() {
        EventCreator eventCreator = EditorWindow.GetWindow<EventCreator>();
    }

    private void OnGUI() {
        GUILayout.Label("Specify the name of the new GameEvent");
        eventName = EditorGUILayout.TextField("New name:", eventName);

        if (GUILayout.Button("Create")) {
            gameEvent = (GameEvent)ScriptableObject.CreateInstance(typeof(GameEvent));
            if (!AssetDatabase.IsValidFolder("Assets/Events/Resources")) {
                if (!AssetDatabase.IsValidFolder("Assets/Events")) {
                    AssetDatabase.CreateFolder("Assets", "Events");
                }
                AssetDatabase.CreateFolder("Assets/Events", "Resources");
            }
            var eventPath = "Assets/Events/Resources/" + eventName + ".asset";
            AssetDatabase.CreateAsset(gameEvent, eventPath);

            eventDataName = eventName + "Data";
            var eventDataScript = CustomDataGenerator.Generate(eventDataName);
            string path = $"{Application.dataPath}/Events/CustomData";
            if (!Directory.Exists(path)) {
                Directory.CreateDirectory(path);
            }
            path += "/" + eventDataName + ".cs";
            File.WriteAllText(path, eventDataScript);

            var reletivePath = "Assets/Events/CustomData/" + eventDataName + ".cs";
            AssetDatabase.ImportAsset(reletivePath, ImportAssetOptions.ForceSynchronousImport);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
