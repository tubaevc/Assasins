using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class EnvChecker : MonoBehaviour
{
  [SerializeField] private Vector3 rayOffset = new Vector3(0, 0.2f, 0);  //raycast konum
  [SerializeField] private float rayLenght = 0.9f;
  [SerializeField] private float heightRayLenght = 6f;
  [SerializeField] private LayerMask obstacleLayer;

  public ObstacleInfo CheckObstacle()
  {
      var hitData = new ObstacleInfo();
      var rayOrigin = transform.position + rayOffset;
      hitData.hitFound = Physics.Raycast(rayOrigin, transform.forward, out hitData.hitInfo,rayLenght,obstacleLayer);
      Debug.DrawRay(rayOrigin,transform.forward * rayLenght,(hitData.hitFound)? Color.red : Color.green);
      if (hitData.hitFound)
      {
          var heightOrigin =  hitData.hitInfo.point + Vector3.up * heightRayLenght;
          hitData.heightHitFound = Physics.Raycast(heightOrigin, Vector3.down, out hitData.heightInfo, heightRayLenght,
              obstacleLayer);
          Debug.DrawRay(heightOrigin,Vector3.down * heightRayLenght,(hitData.heightHitFound)? Color.blue : Color.green);

      }
      return hitData;
  }
}
  public struct ObstacleInfo
  {
      public bool hitFound;
      public bool heightHitFound;
      public RaycastHit hitInfo;
      public RaycastHit heightInfo;
  }
