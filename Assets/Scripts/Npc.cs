using System.Collections;
using UnityEngine;

public enum NpcStatus
{
    Dancing,
    GoingToRest,
    Resting,
    GoingBack,
}

public class Npc : PointMovement
{
    public NpcStatus status { get; private set; } = NpcStatus.Dancing;

    [SerializeField]
    private GroupMovement group;

    [SerializeField]
    private Transform groupPoint;

    [SerializeField]
    private float restingTime;

    private Transform restingPoint;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (GameManager.IsPaused)
        {
            return;
        }

        switch (status) {
            case NpcStatus.Dancing:
                MoveUpdate(characterController, groupPoint);
                break;
            case NpcStatus.Resting:
                break;
            case NpcStatus.GoingToRest:
                MoveUpdate(characterController, restingPoint);
                break;
            case NpcStatus.GoingBack:
                MoveUpdate(characterController, groupPoint);
                break;
            default:
                break;
        }
    }

    public void StartResting(Transform point)
    {
        restingPoint = point;
        status = NpcStatus.GoingToRest;
    }

    protected override void OnPointReached(Transform point)
    {
        switch (status)
        {
            case NpcStatus.GoingToRest:
                status = NpcStatus.Resting;
                StartCoroutine(WaitToRest());
                break;
            case NpcStatus.GoingBack:
                status = NpcStatus.Dancing;
                group.OnNpcComingBack();
                break;
            default:
                break;
        }
    }

    private IEnumerator WaitToRest()
    {
        yield return new WaitForSeconds(restingTime);
        status = NpcStatus.GoingBack;
    }

    public void Kill()
    {
        
    }
}