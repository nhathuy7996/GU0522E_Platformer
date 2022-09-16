using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BNDronePatrol : Node
{
    [SerializeField]
    Transform target;
    Vector2 _start, _end, _target;
    [SerializeField]
    float _speed = 1;

    void Start()
    {
        _end = target.transform.position;
        _target = target.transform.position;
        _start = this.transform.position;
  
    }

    public override nodeState Evaluate()
    {
        Debug.DrawLine(this.transform.position, _end, Color.green);
        this.transform.position = Vector2.MoveTowards(this.transform.position, _end, _speed * Time.deltaTime );

        if (Vector2.Distance(this.transform.position, _end) <= 0.5f)
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
