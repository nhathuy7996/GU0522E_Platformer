using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField]
    float _speed, _jumpForce;
    bool isGround = false;
    [SerializeField] bool isOnSlope;
    [SerializeField] List<PhysicsMaterial2D> _frictions = new List<PhysicsMaterial2D>();
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
       
        _rb.velocity = new Vector2(
           Input.GetAxisRaw("Horizontal") * _speed * Time.deltaTime,
           _rb.velocity.y);

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
            return false;

        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.down, Mathf.Infinity);

        if (hit == null || hit.collider == null)
            return false;



        Debug.LogError(Mathf.Atan2(hit.normal.y, hit.normal.x) * Mathf.Rad2Deg - 90);

        if (hit.normal != Vector2.left && hit.normal != Vector2.right && hit.normal != Vector2.up)
            return true;

        return false;
    }
    
}
