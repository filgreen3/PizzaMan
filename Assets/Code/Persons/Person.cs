using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Person : MonoBehaviour {

    public PersonPreset Data;

    [SerializeField] private DragonBones.UnityArmatureComponent Armature;

    [HideInInspector] public float Speed;
    [HideInInspector] public Vector3 Dir;

    private SpriteRenderer spriteRenderer;
    private Transform transf;

    private Vector3 startPosition;

    public void Init (PersonPreset data) {
        Data = PersonPreset.CopyPersonData (data);
        startPosition = transf.position;
        startPosition.x = Mathf.Abs (startPosition.x);
        Armature._armature.flipX = (Dir.x > 0);
        var transfBody = transf.GetChild (0);

        for (int i = 0; i < transfBody.childCount; i++) {
            transfBody.GetChild (i).gameObject.SetActive (false);
        }
        foreach (var item in Data.Parametrs) {
            foreach (var id in item.CurrentData.NamesItem) {
                transf.GetChild (0).Find (id).gameObject.SetActive (true);
            }
        }
    }

    private void Awake () {
        transf = transform;
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    private void FixedUpdate () {
        transf.Translate (Dir * Speed);
        if (Mathf.Abs (transf.position.x) > startPosition.x) {
            Destroy (gameObject);
        }
    }

    void OnMouseDown () {
        PersonMatchManager.MatchPerson (this);
    }

}