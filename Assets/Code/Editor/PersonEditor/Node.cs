using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;


public abstract class Node : VisualElement
{
    public MenuEditor editor;

    public void UpdateRect(Vector3 offset)
    {
        transform.position += offset;
    }
}
