using System.Collections;
using System.Collections.Generic;
using UnityEditor;

using UnityEngine;


namespace MyEditor {
    public class RequireInterfaceAttribute : PropertyAttribute {
        public System.Type requiredType { get; private set; }
        public RequireInterfaceAttribute (System.Type type) {
            this.requiredType = type;
        }
    }


}