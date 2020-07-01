using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class MenuEditor : EditorWindow
{

    public IEventHandler pointer;


    private void OnElementOver(MouseOverEvent e)
    {
        pointer = e.currentTarget;
    }

    private void OnElementOut(MouseOutEvent e)
    {
        pointer = null;
    }


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
        if (pointer == null)
        {
            drag = delta;

            if (Nodes != null)
            {
                for (int i = 0; i < Nodes.Count; i++)
                {
                    Nodes[i].UpdateRect(delta);
                }
            }
        }
        (pointer as Node)?.UpdateRect(delta);
        GUI.changed = true;
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add node"), false, () =>
        {
            node = new ParamNode();
            node.style.width = 250;
            node.transform.position = mousePosition - Vector2.right * 250 / 2f;
            node.AddToClassList("nodearea");
            AddNode(node);
        });
        genericMenu.ShowAsContext();
    }

    void OnDragPerformEvent(DragPerformEvent e)
    {
        DragAndDrop.AcceptDrag();

        object draggedObject = DragAndDrop.GetGenericData("BoxNode");

        var newBox = new ParamNode();
        var size = new Vector2(100, 75);
        newBox.style.width = size.x;
        newBox.style.height = size.y;

        newBox.transform.position = e.mousePosition - size / 2f;
        AddNode(newBox);

    }

}

