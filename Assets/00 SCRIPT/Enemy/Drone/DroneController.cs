using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : Tree
{

    [SerializeField] Transform target, _player;
    [SerializeField] float _speed, _range;


    protected override void SetUpTree()
    {
        //_nodes = new List<Node>() {

        //    new BND_DroneSeePlayer(this.transform,_range, new List<Node>(){
        //        new BN_MoveToPlayer(this.transform,_player, _speed)
        //    } ),

        //    new BNDronePatrol(this.transform, target.position, _speed)
        //};
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _range);
    }
}
