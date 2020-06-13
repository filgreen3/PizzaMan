using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Person : MonoBehaviour {

    [HideInInspector] public PersonPreset preset;
    public IPersonVisual Data;

    [SerializeField] private DragonBones.UnityArmatureComponent Armature;

    [HideInInspector] public float Speed;
    [HideInInspector] public Vector3 Dir;

    [HideInInspector] public float mainLine;
    [HideInInspector] public float delta;

    private SpriteRenderer spriteRenderer;
    private Transform transf;

    private Vector3 startPosition;

    public void Init () {

        Data = preset.GetDataPasport ();
        startPosition = transf.position;
        startPosition.x = Mathf.Abs (startPosition.x);

        Armature._armature.flipX = Dir.x > 0;
        Speed = (Random.value + 0.3f) * 0.05f;
        Armature.animation.timeScale = Speed * 25f;

        var y = ((Random.value - 0.5f) * delta + mainLine);
        transf.position = Vector3.right * transf.position.x + Vector3.up * y + Vector3.forward * y;

        Data.SettingPerson (this);
    }

    private void Awake () {
        transf = transform;
        spriteRenderer = GetComponent<SpriteRenderer> ();
    }

    private void FixedUpdate () {
        transf.Translate (Dir * Speed);
        if (Mathf.Abs (transf.position.x) > startPosition.x) {
            EndLine ();
        }
    }

    private void EndLine () {
        Dir.x = -Dir.x;
        Init ();
    }
}