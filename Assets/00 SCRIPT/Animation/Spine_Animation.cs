using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using state = PlayerController.playerState;

public class Spine_Animation : BaseAnimation
{
    [SerializeField] SkeletonAnimation _animationController;
    [SerializeField][SpineAnimation] string idle,run,jump,fall,shoot;

    bool isShoot = false;

    public override void ChangeAnim(state currentSTATE)
    {
        if (playerController.currentSTATE.Equals(currentSTATE))
            return;

       
        switch (currentSTATE)
        {
            case state.IDLE:
                _animationController.state.SetAnimation(0, idle, true);
                break;
            case state.MOVING:
                _animationController.state.SetAnimation(0, run, true);
                break;
            case state.JUMP:
                _animationController.state.SetAnimation(0, jump, false);
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.C) && isShoot == false )
        {
            _animationController.state.SetAnimation(1, shoot, true);
            isShoot = true;
        }
        else if (!Input.GetKey(KeyCode.C) && isShoot == true)
        {
            _animationController.state.SetEmptyAnimation(1,0);
            isShoot = false;
        }

    }
}
