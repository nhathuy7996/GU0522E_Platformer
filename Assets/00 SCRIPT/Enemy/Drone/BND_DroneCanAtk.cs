using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BND_DroneCanAtk : Node
{
    [SerializeField]
    float _rangeAtk;

    public override nodeState Evaluate()
    {
        if (GameController.instant.player == null)
        {
            Debug.LogError("Player not assign in manager!");
            return nodeState.FAIL;
        }

        if (!GameController.instant.player.gameObject.activeSelf)
            return nodeState.FAIL;

        Vector3 playerPos = GameController.instant.player.transform.position;
        if (Vector2.Distance(this.transform.position, playerPos) > _rangeAtk)
            return nodeState.FAIL;

        Debug.LogError("Atking!!!");
        return nodeState.RUNNING;
    }
}
