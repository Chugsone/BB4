using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class addtocamera : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public Vector2[] spawnpoints;

    public void addToGroup(PlayerInput input)
    {
        targetGroup.Targets.Add(new() { Weight = 1, Object = input.transform });
    }

    public void teleportToSpawn(PlayerInput input)
    {
        input.transform.position = spawnpoints[input.playerIndex];
    }
}