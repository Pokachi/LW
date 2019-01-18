using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BoardCreator))]
public class BoardCreatorInspector : Editor {
    public BoardCreator current {
        get {
            return (BoardCreator)target;
        }
    }

    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("Reset"))
            current.Reset(9);
        //if (GUILayout.Button("Draw"))
       //     current.Draw();
        //if (GUILayout.Button("Draw Random Area"))
        //    current.DrawRandomArea();
        //if (GUILayout.Button("Draw Specific Area"))
        //    current.DrawSpecificArea();
        //if (GUILayout.Button("Save"))
        //    current.Save();
        //if (GUILayout.Button("Load"))
        //    current.Load(current.levelData);
        //if (GUI.changed)
        //    current.UpdateMarker();
    }
}
