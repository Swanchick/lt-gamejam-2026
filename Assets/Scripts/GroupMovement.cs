using UnityEngine;
using System.Collections.Generic;

public class GroupMovement : MonoBehaviour
{
    [SerializeField]
    private List<Transform> moveToPoints = new();

    [SerializeField]
    private float monsterSpeed = 5f;

    [SerializeField]
    private float pointDistance = 0.5f;

    [SerializeField]
    private Transform groupBody;

    [SerializeField]
    private float groupRotationSpeed = 1f;

    private CharacterController groupController;

    private int currentMoveToPointIndex = 0;
    private Transform currentMoveToPoint;
    private Vector3 groupVelocity;

    private void Start()
    {
        groupController = GetComponent<CharacterController>();
        currentMoveToPoint = moveToPoints[currentMoveToPointIndex];
    }

    private void Update()
    {
        if (GameManager.IsPaused)
        {
            return;
        }

        Debug.Log(currentMoveToPointIndex);

        /// GIRLS DON'T REMOVE THIS SWITCH, THE AI WON'T WORK WITHOUT IT
        PointMovement();
        groupController.Move(groupVelocity * Time.deltaTime);
        Debug.DrawRay(transform.position, groupVelocity, Color.red);

        RotateGroup();
    }

    private Vector3 GetDirectcion()
    {
        Vector3 direction = currentMoveToPoint.position - transform.position;
        direction.Normalize();
        direction.y = 0;

        return direction;
    }

    private float DistanceToMoveToPoint()
    {
        return Vector3.Distance(transform.position, currentMoveToPoint.position);
    }

    private void ChangePoint()
    {
        currentMoveToPointIndex++;

        if (currentMoveToPointIndex >= moveToPoints.Count)
        {
            currentMoveToPointIndex = 0;
        }

        currentMoveToPoint = moveToPoints[currentMoveToPointIndex];
    }

    private void PointMovement()
    {
        Vector3 wishDir = GetDirectcion();
        groupVelocity = Vector3.Lerp(groupVelocity, wishDir * monsterSpeed, Time.deltaTime);
        float dinstance = DistanceToMoveToPoint();

        if (dinstance < pointDistance)
        {
            ChangePoint();
        }
    }

    private void RotateGroup()
    {
        transform.Rotate(transform.up * groupRotationSpeed * Time.deltaTime);
    }
}