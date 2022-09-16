using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BND_DroneSeePlayer : Sequence
{
    Transform _droneTransform;
    float _range;

    public BND_DroneSeePlayer(Transform transform, float range, List<Node> childrens)
    {
        _droneTransform = transform;
        _range = range;

        _childrens = childrens;
    }

    public override nodeState Evaluate()
    {

        Collider2D other = Physics2D.OverlapCircle(_droneTransform.position, _range);

        if (!other)
            return nodeState.FAIL;

        if (!other.gameObject.CompareTag("Player"))
            return nodeState.FAIL;


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
