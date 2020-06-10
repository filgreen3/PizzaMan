using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu (fileName = "Data", menuName = "ScriptableObjects/EasySystem", order = 1)]
public class PersonData : ScriptableObject, IBinding {
    [SerializeField] public Transform PersonTransform;

    [Space (6)] public PersonElement[] Elements;

    public void PreUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void Release()
    {
        throw new System.NotImplementedException();
    }

    public void Update()
    {
        throw new System.NotImplementedException();
    }

    [ContextMenu ("Create System")]
    private void GenerateElements () {
        Elements = new PersonElement[PersonTransform.childCount];
        for (int i = 0; i < PersonTransform.childCount; i++) {
            var item = PersonTransform.GetChild (i);
            Elements[i] = new PersonElement (item.name, false);
        }
    }
}