using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public partial class MenuEditor : EditorWindow
{
    public EditorState State;

    public List<Node> Nodes = new List<Node>();
    public List<Link> Links = new List<Link>();


    public void AddNode(Node node)
    {
        rootVisualElement.Add(node);
        Nodes.Add(node);
        node.SendToBack();
        node.editor = this;

        node.RegisterCallback<MouseOverEvent>(OnElementOver);
        node.RegisterCallback<MouseOutEvent>(OnElementOut);
    }

    public void ClearWindow()
    {
        Nodes.ForEach(node => node.RemoveFromHierarchy());
        Nodes.Clear();
        Links.ForEach(link => link.Unlink());
        Links.Clear();
    }

    public void AddLink(Link link)
    {

        Links.Add(link);
    }
}
public enum EditorState
{
    Mouse,
    Link,
}