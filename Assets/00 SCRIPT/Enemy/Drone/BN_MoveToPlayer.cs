using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BN_MoveToPlayer : Node
{
    Vector2 _start;

    Transform _droneTransform, _target;
    float _speed = 1;

    public BN_MoveToPlayer(Transform droneTransform, Transform target, float speed)
    {
  
        _target = target;
        _start = droneTransform.position;

        _droneTransform = droneTransform;
        _speed = speed;
    }

    public override nodeState Evaluate()
    {

        _droneTransform.position = Vector2.MoveTowards(_droneTransform.position, _target.position, _speed * Time.deltaTime);

        if (Vector2.Distance(_droneTransform.position, _target.position) <= 0.5f)
        {
            return nodeState.SUCCESS;
        }

        return nodeState.RUNNING;
    }
}
