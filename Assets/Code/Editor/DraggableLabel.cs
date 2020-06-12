using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class DraggableLabel : Label {
    public static string s_DragDataType = "DraggableLabel";

    private Vector2 m_MouseOffset;
    private Menu_Editor baseEditor;
    public PersonParametr parametr;
    private ObjectField focusDataBar;

    private VisualElement textBox;

    public DraggableLabel (Menu_Editor baseEditor) {
        RegisterCallback<MouseDownEvent> (OnMouseDownEvent);
        RegisterCallback<MouseUpEvent> (OnMouseUpEvent);
        this.baseEditor = baseEditor;

        focusDataBar = new ObjectField ();
        focusDataBar.objectType = typeof (PersonParametr);
        focusDataBar.RegisterValueChangedCallback (x => UpdataData ((PersonParametr) x.newValue));
        this.Add (focusDataBar);

        textBox = new VisualElement ();
        this.Add (textBox);
    }

    private void UpdataData (PersonParametr data) {
        textBox.Clear ();
        parametr = data;

        if (data != null) {
            var list = data.datas;
            for (int i = 0; i < list.Count; i++) {
                var lable = new Label (list[i].EntitiesNames[0]);
                textBox.Add (lable);
            }
        }
    }

    void OnMouseDownEvent (MouseDownEvent e) {
        if (e.target == this && e.button == 0) {
            baseEditor.dragElement = this;
            DragAndDrop.visualMode = DragAndDropVisualMode.Link;
            DragAndDrop.StartDrag ("0");
            style.opacity = 0.2f;
        }
    }
    void OnMouseUpEvent (MouseUpEvent e) {
        if (e.button == 0) {
            DragAndDrop.AcceptDrag ();
            style.opacity = 1f;
        }
    }

}