using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerState = PlayerController.playerState;

public class UnityAnimation : BaseAnimation
{

    [SerializeField] Animator _animControl;

   

    public override void ChangeAnim(playerState currentSTATE)
    {
        for (int i = 0; i <= (int)playerState.CLIMB; i++)
        {
            playerState tmp = (playerState)i;
            if (tmp != currentSTATE)
                _animControl.SetBool(tmp.ToString(), false);
            else
                _animControl.SetBool(tmp.ToString(), true);
        }

    }

    // Start is called before the first frame update
    void Awake()
    {
        base.Awake();
        _animControl = this.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C))
        {
            _animControl.SetFloat("SHOOT_ID", 1);
        }
        else
        {
            _animControl.SetFloat("SHOOT_ID", 0);
        }


        if (playerController.currentSTATE == playerState.CLIMB)
        {
            if (Input.GetAxisRaw("Vertical") == 0)
            {
                _animControl.speed = 0;
            }
            else
            {
                _animControl.speed = 1;
            }
        }else
            _animControl.speed = 1;

        //int isShooting = Input.GetKey(KeyCode.C) ? 1 : 0;
        //_animControl.SetFloat("SHOOT_ID", isShooting);
    }
}
