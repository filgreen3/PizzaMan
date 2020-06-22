using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class MenuEditor : EditorWindow
{
    private void ProcessEvents(Event e)
    {
        drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {

                    //ClearConnectionSelection();
                }

                if (e.button == 1)
                {


                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }

    private void OnDrag(Vector2 delta)
    {
        drag = delta;

        if (Nodes != null)
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].UpdateRect(drag);
            }
        }
        GUI.changed = true;
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add node"), false, () =>
        {
            node = new BoxNode();

            var size = new Vector2(100, 75);
            node.style.width = size.x;
            node.style.height = size.y;
            node.style.opacity = 1f;
            node.style.backgroundColor = Color.grey;


            node.transform.position = mousePosition - size / 2f;
            node.AddToClassList("area");
            AddNode(node);
        });
        genericMenu.ShowAsContext();
    }
}