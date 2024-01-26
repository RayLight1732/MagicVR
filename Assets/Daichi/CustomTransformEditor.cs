using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(Transform))]       // こうすることによりTransformコンポーネントのインスペクタに、カスタム項目が追加できる
public class WorldPosCS : Editor {
    Transform _t = null;

    private void OnEnable() {
        _t = target as Transform;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        EditorGUILayout.Vector3Field("World Position", _t.position);       // ワールド座標を表示する
        EditorGUILayout.Vector3Field("World Rotation", _t.rotation.eulerAngles);       // ワールド座標を表示する
    }
}
#endif