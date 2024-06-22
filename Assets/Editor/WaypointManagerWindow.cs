using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class WaypointManagerWindow : EditorWindow
{
    [MenuItem("Waypoint/ Waypoint Editor Tools")]
    public static void ShowWindow()
    {
        GetWindow<WaypointManagerWindow>("Waypoint Editor Tools");
    }

    public Transform waypointOrigin;

    public void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty(("waypointOrigin")));
        if (waypointOrigin == null)
        {
            EditorGUILayout.HelpBox("Please assign a Waypoint origin transform", MessageType.Warning);
        }
        else
        {
            EditorGUILayout.BeginVertical("box");
            Createbuttons();
            EditorGUILayout.EndVertical();
        }

        obj.ApplyModifiedProperties();
    }

    public void Createbuttons()
    {
        if (GUILayout.Button("Create Waypoint"))
        {
            CreateWaypoint();
        }
    }

    public void CreateWaypoint()
    {
        GameObject waypointObject = new GameObject("Waypoint" + waypointOrigin.childCount, typeof(Waypoint));
        waypointObject.transform.SetParent(waypointOrigin, false);

        Waypoint waypoint = waypointObject.GetComponent<Waypoint>();
        if (waypointOrigin.childCount>1)
        {
            waypoint.prevWaypoint = waypointOrigin.GetChild(waypointOrigin.childCount - 2).GetComponent<Waypoint>();
            waypoint.prevWaypoint.nextWaypoint = waypoint;
            waypoint.transform.position = waypoint.prevWaypoint.transform.position;
            waypoint.transform.forward = waypoint.prevWaypoint.transform.forward;
            
        }
        Selection.activeGameObject= waypoint.gameObject; //auto select waypoint transform

    }
}