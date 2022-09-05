using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using playerState = PlayerController.playerState;

public abstract class BaseAnimation : MonoBehaviour
{

    [SerializeField] protected PlayerController playerController;
    // Start is called before the first frame update
    protected void Awake()
    {
        playerController = this.GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public abstract void ChangeAnim(playerState currentSTATE);
}
