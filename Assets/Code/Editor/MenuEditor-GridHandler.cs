using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class MenuEditor : EditorWindow
{

    public Vector2 offset;
    public Vector2 drag;

    public VisualElement root;

    public Node node;

    [MenuItem("Person editor/Editor")]
    public static void ShowWindow()
    {
        MenuEditor window = GetWindow<MenuEditor>();
        window.minSize = new Vector2(450, 450);
        window.titleContent = new GUIContent("Editor");

    }

    public void OnEnable()
    {
        root = rootVisualElement;
        root.styleSheets.Add(AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Code/Editor/dnd.uss"));

        var area = new ParametrMenu(this);
        area.AddToClassList("area");
        root.Add(area);
    }


    private void OnGUI()
    {

        DrawGrid(100, 0.5f, Color.grey);
        DrawGrid(20, 0.2f, Color.grey);

        ProcessEvents(Event.current);

        if (GUI.changed) Repaint();

    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        offset += drag * 0.5f;
        Vector3 newOffset = new Vector3(offset.x % gridSpacing, offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

}