using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BNDronePatrol : Node
{
    PathCreator _path;
    Vector2  _end;
    int _currentIndexTarget = 0;

    List<Vector3> _listPoints = new List<Vector3>();

    [SerializeField]
    float _speed = 1;

    void Start()
    {
        _path = this.GetComponent<PathCreator>();
        _listPoints = _path.getList_Points;

        this.transform.position = _listPoints[0];
        _currentIndexTarget = 1;
        _end = _listPoints[_currentIndexTarget];
  
    }

    public override nodeState Evaluate()
    {
        Debug.DrawLine(this.transform.position, _end, Color.green);
        this.transform.position = Vector2.MoveTowards(this.transform.position, _end, _speed * Time.deltaTime );

        if (Vector2.Distance(this.transform.position, _end) <= 0.5f)
        {
            _currentIndexTarget++;
            if (_currentIndexTarget >= _listPoints.Count)
                _currentIndexTarget = 0;
            _end = _listPoints[_currentIndexTarget];
        }

        return nodeState.RUNNING;
    }
}
