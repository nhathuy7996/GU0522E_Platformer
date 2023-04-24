using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BND_DroneSeePlayer : Sequence
{
    [SerializeField]
    float _range;

    public BND_DroneSeePlayer(Transform transform, float range, List<Node> childrens)
    {
        _range = range;

        _childrens = childrens;
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
        if (Vector2.Distance(playerPos, this.transform.position) > _range)
        {
            return nodeState.FAIL;
        }


        Vector2 dir = playerPos - this.transform.position;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir);


        Debug.DrawLine(playerPos, this.transform.position, Color.green);

        if (hit.collider != null && !hit.collider.CompareTag("Player"))
        {
            Debug.DrawLine(playerPos, hit.point, Color.yellow);
            return nodeState.FAIL;
        }

        bool isAnyRunning = false;

        foreach (Node n in _childrens)
        {
            switch (n.Evaluate())
            {
                case nodeState.FAIL:
                    continue;
                    break;
                case nodeState.RUNNING:
                    isAnyRunning = true;
                    break;
                case nodeState.SUCCESS:
                    continue;
                    break;
            }
        }

        if (isAnyRunning)
            return nodeState.RUNNING;
        else
            return nodeState.SUCCESS;
    }
}
