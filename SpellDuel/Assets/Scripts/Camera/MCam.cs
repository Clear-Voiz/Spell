using UnityEngine;
using Cinemachine;

public class MCam : MonoBehaviour
{
    private void Awake()
    {
        Pawn.OnFirstObjectSpawned += OnFirstObjectNotified;
    }

    private void OnDestroy()
    {
        Pawn.OnFirstObjectSpawned -= OnFirstObjectNotified;
    }

    private void OnFirstObjectNotified(Transform obj)
    {
        CinemachineVirtualCamera vc = GetComponent<CinemachineVirtualCamera>();
        Pawn pn = obj.GetComponent<Pawn>();
        vc.Follow = pn.followTargetPosition.transform;
        vc.LookAt = pn.LookAtPosition.transform;
    }
}
