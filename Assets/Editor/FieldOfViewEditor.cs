using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (FieldOfViewHelper))]
public class FieldOfViewEditor : Editor {

    private void OnSceneGUI()
    {
        FieldOfViewHelper fov = (FieldOfViewHelper)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov._viewRadious);

        Vector3 viewAngleA = fov.DirFromAngle(-fov._viewAngle / 2, false);
        Vector3 viewAngleB = fov.DirFromAngle(fov._viewAngle / 2, false);

        Handles.DrawLine(fov.transform.position, fov.transform.position +( viewAngleA * fov._viewRadious));
        Handles.DrawLine(fov.transform.position, fov.transform.position +( viewAngleB * fov._viewRadious));

        Handles.color = Color.red;
        foreach(FieldOfViewHelper.VisibleTargetsStruct visibleTarget in fov.visibleTargetStructList)
        {
            Handles.DrawLine(fov.transform.position, visibleTarget.transform.position);
        }

    }

}
