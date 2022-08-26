using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField]
    float _speed, _jumpForce, _slopeForce;
    [SerializeField]
    bool isGround = false;
    [SerializeField] bool isOnSlope;
    [SerializeField] List<PhysicsMaterial2D> _frictions = new List<PhysicsMaterial2D>();

    [SerializeField]
    float _anglePlatform;
    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
    }

    void Moving()
    {

        //if(isOnSlope)
        //{
        //    _rb.AddForce(new Vector2(0,-_slopeForce));
        //}

        float dir = Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime;

        Vector2 movement;

        movement.x = Mathf.Cos((_anglePlatform) * Mathf.Deg2Rad) * dir;
        if (isGround)
            movement.y = Mathf.Sin((_anglePlatform) * Mathf.Deg2Rad) * dir;
        else
            movement.y = _rb.velocity.y;

       // Debug.DrawRay(this.transform.position, movement, Color.red);

       
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
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
    
}
