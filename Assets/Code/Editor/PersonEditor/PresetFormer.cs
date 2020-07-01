using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class PresetFormer : VisualElement
{
    public PersonPreset preset;
    public ObjectField objectfield;
    private int count;

    private static MenuEditor savedWindow;

    public PresetFormer()
    {
        objectfield = new ObjectField();
        objectfield.objectType = typeof(PersonPreset);
        objectfield.style.width = 235;
        objectfield.RegisterValueChangedCallback(x =>
        {
            preset = (PersonPreset)x.newValue;
            if (preset) LoadPreset();
        });

        var saveButton = new Button();
        saveButton.text = "Save";
        saveButton.style.position = Position.Absolute;
        saveButton.style.left = 240;
        saveButton.style.width = 60;

        var loadButton = new Button();
        loadButton.text = "Load";
        loadButton.style.position = Position.Absolute;
        loadButton.style.left = 305;
        loadButton.style.width = 60;

        this.Add(objectfield);
        this.Add(saveButton);
        this.Add(loadButton);
    }

    private void LoadPreset()
    {
        MenuEditor.CurrentWindow.ClearWindow();

        count = 0;
        var yOffset = 0;

        for (int i = 0; i < preset.Parametrs.Length; i++)
        {
            var parametr = preset.Parametrs[i] as PersonParametr;
            if (parametr)
            {
                CreateNode(parametr, i, Vector2.up * 100 * (i + yOffset));
                yOffset += parametr.datas.Count;

            }
        }
    }



    private ParamNode CreateNode(PersonParametr parametr, int id, Vector2 pos)
    {
        if (count > 100) return null;

        count++;
        var newNode = new ParamNode();
        newNode.style.width = 250;
        newNode.AddToClassList("nodearea");
        newNode.transform.position = pos;

        newNode.Paramfield.value = parametr;
        newNode.UpdateDatas(parametr);
        MenuEditor.CurrentWindow.AddNode(newNode);


        Debug.Log(newNode.transform.position);

        for (int i = 0; i < parametr.datas.Count; i++)
        {
            var subParametr = parametr.datas[i] as PersonParametr;
            if (subParametr)
            {
                var node = CreateNode(subParametr, i, 300 * Vector2.right + Vector2.up * 100 * i +
                newNode.transform.position.y * Vector2.up);
                newNode.InLinkPoints[i].LinkInToOut(node.OutLinkPoint);
            }
        }

        return newNode;
    }
}



