using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private DataEntityCollection entityCollection;
    private VisualElement menuBox;

    public ParametrMenu (MenuEditor baseEditor) {
        entityCollection = AssetDatabase.LoadAssetAtPath<DataEntityCollection> (dataEntityPath + "Data.asset");

        var dataBaseField = new ObjectField ();
        dataBaseField.objectType = typeof (DataEntityCollection);
        dataBaseField.value = entityCollection;
        dataBaseField.RegisterValueChangedCallback (x => SetVisibaleMenu ((DataEntityCollection) x.newValue));
        this.Add (dataBaseField);

        menuBox = new VisualElement ();
        menuBox.visible = entityCollection != null;
        this.Add (menuBox);

        focusDataBar = new ObjectField ();
        focusDataBar.objectType = typeof (PersonParametr);
        focusDataBar.RegisterValueChangedCallback (x => UpdataData ((PersonParametr) x.newValue));
        menuBox.Add (focusDataBar);

        menuBox.Add (newParametrName = new TextField ("Name"));
        menuBox.Add (new Button (() => NewParametr ()) { text = "New" });
        menuBox.Add (new Button (() => AddItems ()) { text = "Add" });

        textBox = new ScrollView () { showVertical = true };
        textBox.style.opacity = 0f;
        textBox.style.right = 4f;
        menuBox.Add (textBox);
    }

    private void AddItems () {
        if (parametr == null) return;
        var list = Selection.gameObjects.ToList ();
        var listBeforeCount = list.Count;
        Debug.Log ($"Selected: {listBeforeCount}");

        list.Sort (delegate (GameObject x, GameObject y) {
            if (x.name == null && y.name == null) return 0;
            else if (x.name == null) return -1;
            else if (y.name == null) return 1;
            else return x.name.CompareTo (y.name);
        });

        foreach (var item in list) {
            var entityLocal = entityCollection.Collection.Find (entity => entity.Key == item.name);
            if (entityLocal != null && !parametr.datas.Contains (entityLocal))
                parametr.datas.Add (entityLocal);
        }

        list.FindAll (item => entityCollection.ConstainsName (item.name));

        list.RemoveAll (item => entityCollection.ConstainsName (item.name));
        Debug.Log ($"Removed copy: {listBeforeCount-list.Count}");
        listBeforeCount = list.Count;


        var entitylist = new List<DataEntity> ();
        foreach (var item in list) {
            var newdata = ScriptableObject.CreateInstance<DataEntity> ();
            newdata.Init (item.name);
            AssetDatabase.CreateAsset (newdata, dataEntityPath + newdata.Key + ".asset");
            entitylist.Add (newdata);

        }
        Debug.Log ($"Add new entity: {list.Count}");

        parametr.datas.AddRange (entitylist);
        entityCollection.Collection.AddRange (entitylist);
        UpdataData (parametr);
        AssetDatabase.Refresh ();
    }

    private void SetVisibaleMenu (DataEntityCollection entityCollection) {
        this.entityCollection = entityCollection;
        menuBox.visible = entityCollection != null;
    }

    private void NewParametr () {
        if (string.IsNullOrEmpty (newParametrName.value)) return;

        var entity = ScriptableObject.CreateInstance<PersonParametr> ();
        entity.name = newParametrName.value;
        if (string.IsNullOrEmpty (AssetDatabase.AssetPathToGUID (personParametrPath + entity.name + ".asset"))) {
            AssetDatabase.CreateAsset (entity, personParametrPath + entity.name + ".asset");
            AssetDatabase.Refresh ();
            focusDataBar.value = entity;
        } else {
            Debug.Log ("Already have this asset");
        }
        AssetDatabase.Refresh ();
    }

    private void UpdataData (PersonParametr data) {
        textBox.Clear ();
        parametr = data;

        if (data != null && data.datas != null) {
            var linkedParametr = new ObjectField ("Link");
            linkedParametr.objectType = typeof (PersonParametr);
            linkedParametr.value = parametr.linkedParametr;
            linkedParametr.RegisterValueChangedCallback (x => parametr.linkedParametr = (PersonParametr) x.newValue);
            textBox.Add (linkedParametr);

            var list = data.datas;
            for (int i = 0; i < list.Count; i++) {
                var id = i;
                var field = new ObjectField ();
                field.objectType = typeof (DataEntityGiver);
                field.style.left = 5f;
                field.value = list[i];

                field.RegisterValueChangedCallback (x => list[id] = (DataEntityGiver) x.newValue);
                textBox.Add (field);
            }

            var extraObject = new ObjectField ();
            extraObject.objectType = typeof (DataEntityGiver);
            extraObject.style.left = 5f;
            extraObject.tooltip = "Add new object";
            extraObject.style.backgroundColor = Color.green;
            extraObject.style.color = Color.green;
            extraObject.style.opacity = 0.4f;

            extraObject.RegisterValueChangedCallback (x => {
                list.Add ((DataEntityGiver) x.newValue);
                UpdataData (parametr);
            });
            textBox.Add (extraObject);

            textBox.style.opacity = (data.datas.Count == 0) ? 0f : 1f;
        }
    }
}