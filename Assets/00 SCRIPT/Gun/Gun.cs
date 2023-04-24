using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : WeaponBase
{
    [SerializeField] GameObject _bulletPrefab;

    [SerializeField] float _atkSpeed;


    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(AtkTask());
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer > 0)
            return;
        if (Input.GetKey(KeyCode.C))
        {
            this.Attack();
        }

        timer = _atkSpeed;

    }

    public override void Attack()
    {
        float angle = this.transform.parent.localScale.x == 1 ? 0 : 180;
        GameObject g = PoolingManager.instant.GetObject(_bulletPrefab);

        g.transform.rotation = Quaternion.Euler(0,angle,0);
        g.transform.position = this.transform.position;
        
        g.SetActive(true);

        //PoolingManager.instant.getComponent<BulletControllerBase>(_bulletPrefab);
    }

    IEnumerator AtkTask()
    {
        while (true)
        {
          
            if (Input.GetKey(KeyCode.C))
            {
                this.Attack();
                yield return new WaitForSeconds(_atkSpeed);
            }
            yield return new WaitForSeconds(0);
        }
    }
}
