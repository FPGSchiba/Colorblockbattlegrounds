using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof (FieldOfView))]
public class FliedOfViewEditor : Editor
{

    public float Offset1 = 60;
    public float Offset2 = -60;

    private void OnSceneGUI()
    {
        FieldOfView fow = (FieldOfView)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius + Vector3.up * Offset1);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius + Vector3.up * Offset1);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius + Vector3.up * Offset2);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius + Vector3.up * Offset2);
        Handles.DrawLine(fow.transform.position + viewAngleA * fow.viewRadius + Vector3.up * Offset1, fow.transform.position + viewAngleB * fow.viewRadius + Vector3.up * Offset1);
        Handles.DrawLine(fow.transform.position + viewAngleA * fow.viewRadius + Vector3.up * Offset1, fow.transform.position + viewAngleA * fow.viewRadius + Vector3.up * Offset2);
        Handles.DrawLine(fow.transform.position + viewAngleB * fow.viewRadius + Vector3.up * Offset1, fow.transform.position + viewAngleB * fow.viewRadius + Vector3.up * Offset2);
        Handles.DrawLine(fow.transform.position + viewAngleA * fow.viewRadius + Vector3.up * Offset2, fow.transform.position + viewAngleB * fow.viewRadius + Vector3.up * Offset2);
    }

}