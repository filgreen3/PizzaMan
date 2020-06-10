using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public class ParametrMenu : VisualElement {

    const string dataEntityPath = "Assets/Prefabs/Parametrs/DataParamers/";
    const string personParametrPath = "Assets/Prefabs/Parametrs/PersonParametrs/";

    private Button addMoreItems;
    private PersonParametr parametr;
    private VisualElement textBox;
    private TextField newParametrName;
    private ObjectField focusDataBar;

    public ParametrMenu (Menu_Editor baseEditor) {
        focusDataBar = new ObjectField ();
        focusDataBar.objectType = typeof (PersonParametr);
        focusDataBar.RegisterValueChangedCallback (x => UpdataData ((PersonParametr) x.newValue));
        this.Add (focusDataBar);

        this.Add (newParametrName = new TextField ("Name"));
        this.Add (new Button (() => NewParametr ()) { text = "New" });
        this.Add (new Button (() => AddItems ()) { text = "Add" });

        textBox = new ScrollView () { showVertical = true };
        this.Add (textBox);
    }

    private void AddItems () {
        if (parametr == null) return;

        var list = Selection.gameObjects;
        foreach (var item in list) {
            if (!parametr.ContainData (item.name)) {
                DataEntity entity;
                if (string.IsNullOrEmpty (AssetDatabase.AssetPathToGUID (dataEntityPath + item.name + ".asset"))) {
                    entity = ScriptableObject.CreateInstance<DataEntity> ();
                    entity.NamesItem = item.name;
                    AssetDatabase.CreateAsset (entity, dataEntityPath + entity.NamesItem + ".asset");
                } else {
                    entity = AssetDatabase.LoadAssetAtPath<DataEntity> (dataEntityPath + item.name + ".asset");
                }
                parametr.datas.Add (entity);
            }
        }
        AssetDatabase.Refresh ();
        UpdataData (parametr);
    }

    private void NewParametr () {

        var entity = ScriptableObject.CreateInstance<PersonParametr> ();
        entity.name = newParametrName.value;
        if (string.IsNullOrEmpty (AssetDatabase.AssetPathToGUID (personParametrPath + entity.name + ".asset"))) {
            AssetDatabase.CreateAsset (entity, personParametrPath + entity.name + ".asset");
            AssetDatabase.Refresh ();
            focusDataBar.value = entity;
        } else {
            Debug.Log ("Already have this asset");
        }
    }

    private void UpdataData (PersonParametr data) {
        textBox.Clear ();
        parametr = data;

        if (data != null && data.datas != null) {
            var list = data.datas;
            for (int i = 0; i < list.Count; i++) {
                var lable = new Label (list[i].NamesItem);
                textBox.Add (lable);
            }
        }
    }
}