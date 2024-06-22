using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmos(Waypoint waypoint, GizmoType gizmoType)
    {
        if ((gizmoType & GizmoType.Selected) != 0 )
        {
            Gizmos.color=Color.blue;
        }
        else
        {
            Gizmos.color = Color.blue * 0.5f;
        }
        Gizmos.DrawSphere(waypoint.transform.position,0.1f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoint.transform.position+ (waypoint.transform.right*waypoint.waypointWidth/2f),waypoint.transform.position- (waypoint.transform.right * waypoint.waypointWidth/2f));

        if (waypoint.prevWaypoint != null)
        {
            Gizmos.color = Color.red;
            //measure distance
            Vector3 offset = waypoint.transform.right * waypoint.waypointWidth / 2f;
            Vector3 offsetTo = waypoint.prevWaypoint.transform.right * waypoint.prevWaypoint.waypointWidth / 2f;
            
            Gizmos.DrawLine(waypoint.transform.position+offset,waypoint.prevWaypoint.transform.position+offsetTo);
        }
        if (waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.green;
            //measure distance
            Vector3 offset = waypoint.transform.right * -waypoint.waypointWidth / 2f;
            Vector3 offsetTo = waypoint.prevWaypoint.transform.right * -waypoint.prevWaypoint.waypointWidth / 2f;
            
            Gizmos.DrawLine(waypoint.transform.position+offset,waypoint.prevWaypoint.transform.position+offsetTo);
        }
    }
}