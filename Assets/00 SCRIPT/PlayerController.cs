using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    Collider2D _colli;
    [Header("Config")]
    [SerializeField] BaseAnimation _animControl;
    [SerializeField]
    float _speed, _jumpForce, _slopeForce;
    [SerializeField]
    bool isGround = false;
    [SerializeField] bool isOnSlope;
    [SerializeField] List<PhysicsMaterial2D> _frictions = new List<PhysicsMaterial2D>();

    [Header("Watching")]
    [SerializeField]
    float _anglePlatform;
    [SerializeField]
    Vector2 movement;

    [SerializeField]
    playerState PLAY_STATE = playerState.IDLE;

    public playerState currentSTATE => PLAY_STATE;

    public enum playerState
    {
        IDLE = 1,
        MOVING = 2,
        JUMP = 3,
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _colli = this.GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        checkGround();

        updateState();

        _animControl.ChangeAnim(PLAY_STATE);
    }

    void Moving()
    {

        float dir = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;

        Vector3 tmp = this.transform.localScale;
        if (dir > 0)
        {
            tmp.x = 1;
        }else if(dir < 0)
        {
            tmp.x = -1;
        }

        this.transform.localScale = tmp;

        movement.x = Mathf.Cos((_anglePlatform) * Mathf.Deg2Rad) * dir;
        if (isGround)
            movement.y = Mathf.Sin((_anglePlatform) * Mathf.Deg2Rad) * dir;
        else
            movement.y = _rb.velocity.y;
       
        _rb.velocity = movement;

        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            isGround = false;
            _rb.AddForce(new Vector2(0, _jumpForce));
        }

        isOnSlope = checkSlope();

        if (isOnSlope)
        {
            if (Input.GetAxisRaw("Horizontal") != 0)
                _rb.sharedMaterial = _frictions[0];
            else
                _rb.sharedMaterial = _frictions[1];

            return;
        }

        _rb.sharedMaterial = _frictions[0];
    }

    bool checkSlope()
    {

        if (!isGround)
        {
            _anglePlatform = 0;
            return false;
        }

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, Mathf.Infinity);
        
        if (hit == null || hit.collider == null)
        {
            _anglePlatform = 0;
            return false;
        }

        // extends slope angle
        if (hit.normal != Vector2.left && hit.normal != Vector2.right && hit.normal != Vector2.up)
        {
            _anglePlatform = Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg - 90;
            return true;
        }

        _anglePlatform = 0;
        return false;
    }

    void checkGround()
    {
        RaycastHit2D[] hits = new RaycastHit2D[20];

        _colli.Cast(Vector2.down , hits, 0.05f);
       foreach(RaycastHit2D hit in hits)
        { 
            if ( hit.collider != null)
            {
                
                isGround = true;
                this.transform.SetParent(hit.collider.transform);
                return;
            }
        }

        this.transform.SetParent(null);
        isGround = false;
    }

    void updateState() {
        if (isGround)
        {
            if (_rb.velocity.x != 0)
                PLAY_STATE = playerState.MOVING;
            else
                PLAY_STATE = playerState.IDLE;
        }
        else
        {
            if(_rb.velocity.y > 0)
                PLAY_STATE = playerState.JUMP;
            else if (_rb.velocity.y < 0)
                PLAY_STATE = playerState.JUMP;
        }
    }


}
