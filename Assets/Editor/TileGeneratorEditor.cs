using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MapManager))]
public class TileGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Generate Board"))
        {
            GenerateBoard();
        }
    }

    void GenerateBoard()
    {
        MapManager.Instance.GenerateMap();
    }
}
