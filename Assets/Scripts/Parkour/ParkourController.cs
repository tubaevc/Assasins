using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParkourController : MonoBehaviour
{
    [SerializeField] private EnvChecker _envChecker;
    private bool playerInAction;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerController _playerController;

    [SerializeField] private List<NewParkourAction> newParkourActions;

    private void Update()
    {
        if (Input.GetButton("Jump") && !playerInAction)
        {
            var hitData = _envChecker.CheckObstacle();
            if (hitData.hitFound)
            {
                foreach (var action in newParkourActions)
                {
                    if (action.CheckIfAvailable(hitData, transform))
                    {
                        StartCoroutine(PerformParkourAction(action));
                        break;
                    }
                }
            }
        }
    }

    IEnumerator PerformParkourAction(NewParkourAction action)
    {
        playerInAction = true;
        _playerController.SetControl(false);


        _animator.CrossFade(action.AnimationName, 0.2f);
        yield return null;
        var animationState = _animator.GetNextAnimatorStateInfo(0);
        if (!animationState.IsName(action.AnimationName))
        {
            Debug.Log("animation name incorrect");
        }

        yield return new WaitForSeconds(animationState.length);

        float timerCounter = 0f;
        while (timerCounter <= animationState.length)
        {
            timerCounter += Time.deltaTime;
            //player look towards to obstacle
            if (action.LookAtObstacle)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, action.RequiredRotation,
                    _playerController.rotSpeed * Time.deltaTime);
            }

            if (action.AllowTargetMatching)
            {
                CompareTarget(action);
            }

            if (_animator.IsInTransition(0)&& timerCounter>0.5f)
            {
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(action.ParkourActionDelay);

        _playerController.SetControl(true);
        playerInAction = false;
    }

    private void CompareTarget(NewParkourAction action)
    {
        _animator.MatchTarget(action.ComparePosition, transform.rotation, action.CompareBodyPart,
            new MatchTargetWeightMask(action.ComparePositionWeight, 0), action.CompareStartTime, action.CompareEndTime);
    }
}