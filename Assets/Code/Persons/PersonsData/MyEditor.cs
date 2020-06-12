﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MyEditor {
    public class RequireInterfaceAttribute : PropertyAttribute {
        public System.Type requiredType { get; private set; }
        public RequireInterfaceAttribute (System.Type type) {
            this.requiredType = type;
        }
    }

    [CustomPropertyDrawer (typeof (RequireInterfaceAttribute))]
    public class RequireInterfaceDrawer : PropertyDrawer {

        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
            if (property.propertyType == SerializedPropertyType.ObjectReference) {
                var requiredAttribute = this.attribute as RequireInterfaceAttribute;
                EditorGUI.BeginProperty (position, label, property);
                property.objectReferenceValue = EditorGUI.ObjectField (position, label, property.objectReferenceValue, requiredAttribute.requiredType, true);
                EditorGUI.EndProperty ();
            } else {
                var previousColor = GUI.color;
                GUI.color = Color.red;
                EditorGUI.LabelField (position, label, new GUIContent ("Property is not a reference type"));
                GUI.color = previousColor;
            }
        }
    }
}