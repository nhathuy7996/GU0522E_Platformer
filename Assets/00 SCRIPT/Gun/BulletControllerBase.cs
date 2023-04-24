using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerBase : MonoBehaviour
{
    [SerializeField] float _speed, _damage, _lifeTime;

    Coroutine waitDeactice_C;
    // Start is called before the first frame update
    void OnEnable()
    {
        waitDeactice_C = StartCoroutine(waitDeactive());
    }

    private void OnDisable()
    {
        StopCoroutine(waitDeactice_C);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += this.transform.right * _speed * Time.deltaTime;
        //this.GetComponent<Rigidbody2D>().MovePosition(this.transform.position + );
    }

    IEnumerator waitDeactive()
    {
        yield return new WaitForSeconds(_lifeTime);
        this.gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        this.gameObject.SetActive(false);
    }
}
