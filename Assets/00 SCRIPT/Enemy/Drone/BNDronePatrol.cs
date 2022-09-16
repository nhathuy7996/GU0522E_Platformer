using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BNDronePatrol : Node
{

    Vector2 _start, _end, _target;

    Transform _droneTransform;
    float _speed = 1;

    public BNDronePatrol(Transform droneTransform, Vector2 target, float speed)
    {
        _end = target;
        _target = target;
        _start = droneTransform.position;

        _droneTransform = droneTransform;
        _speed = speed;
    }

    public override nodeState Evaluate()
    {

        _droneTransform.position = Vector2.MoveTowards(_droneTransform.position, _end, _speed * Time.deltaTime );

        if (Vector2.Distance(_droneTransform.position, _end) <= 0.5f)
        {
            if (_end.Equals(_target))
            {
                _end = _start;
            }
            else
            {
                _end = _target;
            }
        }

        return nodeState.RUNNING;
    }
}
