using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class Menu_Editor : EditorWindow {

    public DraggableLabel dragElement;
    private Label info;

    [MenuItem ("UIElementsExamples/Menu")]
    public static void ShowExample () {
        Menu_Editor window = GetWindow<Menu_Editor> ();
        window.minSize = new Vector2 (450, 450);
        window.titleContent = new GUIContent ("Menu");

    }

    private VisualElement m_DropArea;

    public void OnEnable () {
        var root = rootVisualElement;
        root.styleSheets.Add (AssetDatabase.LoadAssetAtPath<StyleSheet> ("Assets/Code/Editor/dnd.uss"));

        m_DropArea = new VisualElement ();
        m_DropArea.AddToClassList ("droparea");
        m_DropArea.Add (new Label { text = "Drag and drop anything here" });
        m_DropArea.Add (info = new Label { text = "info" });

        root.Add (m_DropArea);

        m_DropArea.RegisterCallback<MouseDownEvent> (OnDownMouse);
        m_DropArea.RegisterCallback<DragUpdatedEvent> (OnDrag);
        m_DropArea.RegisterCallback<DragExitedEvent> (OnDragEnd);

        var area = new ParametrMenu (this);
        area.AddToClassList ("area");
        root.Add (area);
    }

    void OnDownMouse (MouseDownEvent downEvent) {
        if (downEvent.target == m_DropArea && downEvent.button == 1) {
            var newBox = new DraggableLabel (this);
            newBox.AddToClassList ("box");
            newBox.style.top = downEvent.localMousePosition.y - 25f;
            newBox.style.left = downEvent.localMousePosition.x - 64f;
            m_DropArea.Add (newBox);
        }
    }

    void OnDrag (DragUpdatedEvent e) {
        if (dragElement != null) {
            dragElement.style.top = e.localMousePosition.y - 25f;
            dragElement.style.left = e.localMousePosition.x - 64f;
        }

    }
    void OnDragEnd (DragExitedEvent e) {
        if (dragElement != null) {
            dragElement.style.top = e.localMousePosition.y - 25f;
            dragElement.style.left = e.localMousePosition.x - 64f;
            DragAndDrop.AcceptDrag ();
            dragElement.style.opacity = 1f;
        }

    }

}