using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : Tree
{
    [SerializeField] float _exp = 0;
    [SerializeField] Transform target, _player;
    [SerializeField] float _speed, _range_detect, _range_atk;


    protected override void SetUpTree()
    {
        //_nodes = new List<Node>() {

        //    new BND_DroneSeePlayer(this.transform,_range_detect, new List<Node>(){
        //        new BN_MoveToPlayer(this.transform,_player, _speed, _range_atk)
        //    } ),

        //    new BND_DroneCanAtk(this.transform,_range_atk),

        //    new BNDronePatrol(this.transform, target.position, _speed)
        //};

        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, _range_detect);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, _range_atk);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            GameController.instant.player.GainEXP(this._exp);
            Destroy(this.gameObject);
        }
    }
}
