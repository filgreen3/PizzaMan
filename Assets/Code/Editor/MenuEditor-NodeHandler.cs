using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class MenuEditor : EditorWindow
{

    public List<Node> Nodes = new List<Node>();

    public void AddNode(Node node)
    {
        root.Add(node);
        Nodes.Add(node);
        node.SendToBack();
    }
}