using UnityEngine;
using System.Collections;
using System.Collections.Generic;


enum GroupStatus
{
    None,
    Waiting,
    Resting,
}

public class GroupMovement : PointMovement
{
    [SerializeField]
    private List<Npc> npcs = new();

    [SerializeField]
    private List<Transform> moveToPoints = new();

    [SerializeField]
    private Transform groupBody;

    [SerializeField]
    private float groupRotationSpeed = 1f;

    [SerializeField]
    private List<Transform> restingPoints = new();

    [SerializeField]
    private float restingTime = 10f;

    private CharacterController groupController;

    private int currentMoveToPointIndex = 0;
    private Transform currentMoveToPoint;

    private GroupStatus status = GroupStatus.None;

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

        MoveUpdate(groupController, currentMoveToPoint);
        RotateGroup();

        if (status == GroupStatus.None)
        {
            status = GroupStatus.Waiting;
            StartCoroutine(WaitToRest());
        }
    }

    private IEnumerator WaitToRest()
    {
        yield return new WaitForSeconds(restingTime);

        Transform point = GetRandom(restingPoints);
        Npc npc = GetRandom(npcs);
        npc.StartResting(point);

        status = GroupStatus.Resting;
    }

    public void OnNpcComingBack()
    {
        if (status != GroupStatus.Resting)
        {
            return;
        }

        status = GroupStatus.None;
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

    private T GetRandom<T>(List<T> list)
    {
        int index = Random.Range(0, list.Count);
        return list[index];
    }

    private void RotateGroup()
    {
        transform.Rotate(transform.up * groupRotationSpeed * Time.deltaTime);
    }

    protected override void OnPointReached(Transform point)
    {
        ChangePoint();
    }
}