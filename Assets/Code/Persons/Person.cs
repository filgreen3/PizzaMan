using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Person : MonoBehaviour {

    public IPersonVisual Data;

    [SerializeField] private DragonBones.UnityArmatureComponent Armature;

    [HideInInspector] public float Speed;
    [HideInInspector] public Vector3 Dir;

    private SpriteRenderer spriteRenderer;
    private Transform transf;

    private Vector3 startPosition;

    public void Init (IPersonVisual data) {
        Data = data;
        startPosition = transf.position;
        startPosition.x = Mathf.Abs (startPosition.x);
        Armature._armature.flipX = (Dir.x > 0);

        data.SettingPerson (this);
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