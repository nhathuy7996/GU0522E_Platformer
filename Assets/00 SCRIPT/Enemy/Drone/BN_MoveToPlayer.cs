using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BN_MoveToPlayer : Node
{
    Vector2 _start;

    [SerializeField]
    float _range_atk;
    [SerializeField]
    float _speed = 1;

    private void Start()
    {
       
        _speed *= 1.5f;
    }

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

        this.transform.position = Vector2.MoveTowards(this.transform.position, playerPos, _speed * Time.deltaTime);

        if (Vector2.Distance(this.transform.position, playerPos) <= _range_atk)
        {
            return nodeState.SUCCESS;
        }

        return nodeState.RUNNING;
    }
}
