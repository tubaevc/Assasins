using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Parkour Menu/ Create New Parkour Action")]
public class NewParkourAction : ScriptableObject
{
    [SerializeField] private string animationName;
    [SerializeField] private string barrierTag;
    [SerializeField] private float minHeight;
    [SerializeField] private float maxHeight;
    [SerializeField] public bool lookAtObstacle;
    [SerializeField] private float parkourActionDelay;
    public Quaternion RequiredRotation { get; set; }
    [SerializeField] private bool allowTargetMatching = true;
    [SerializeField] private AvatarTarget compareBodyPart;
    public Vector3 ComparePosition { get; set; }

    [SerializeField] private float compareStartTime;
    [SerializeField] private float compareEndTime;

    [SerializeField] private Vector3 comparePositionWeight = new Vector3(0, 1, 0);
    public bool CheckIfAvailable(ObstacleInfo hitData, Transform player)
    {
        if (!string.IsNullOrEmpty(barrierTag) && hitData.hitInfo.transform.tag != barrierTag)
        {
            return false;
        }
       
        float checkHeight = hitData.heightInfo.point.y - player.position.y;
        if (checkHeight < minHeight || checkHeight > maxHeight)
        {
            return false;
        }

        if (lookAtObstacle)
        {
            RequiredRotation = Quaternion.LookRotation(-hitData.hitInfo.normal);
        }

        if (allowTargetMatching)
        {
            ComparePosition = hitData.heightInfo.point;
        }

        return true;
    }

    public string AnimationName => animationName;
    public bool LookAtObstacle => lookAtObstacle;

    public float ParkourActionDelay => parkourActionDelay;
    
    public float CompareStartTime => compareStartTime;
    public float CompareEndTime => compareEndTime;
    public bool AllowTargetMatching => allowTargetMatching;
    public AvatarTarget  CompareBodyPart => compareBodyPart;
    [SerializeField] public Vector3 ComparePositionWeight => comparePositionWeight;
}