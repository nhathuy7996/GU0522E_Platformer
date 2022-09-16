using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{

    private static PoolingManager _instant;
    public static PoolingManager instant => _instant;

    Dictionary<GameObject, List<GameObject>> objectsPool = new Dictionary<GameObject, List<GameObject>>();


    private void Awake()
    {
        _instant = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public T getComponent<T>(GameObject keyObject) where T : MonoBehaviour
    {
        return GetObject(keyObject).GetComponent<T>();
    }

    public GameObject GetObject(GameObject keyObject)
    {
        List<GameObject> objects;
        bool getSuccess = objectsPool.TryGetValue(keyObject, out objects);
        if (getSuccess)
        {
            foreach (GameObject g in objects)
            {
                if (g.activeSelf)
                    continue;

                return g;
            }
        }

        GameObject g2 = Instantiate(keyObject, this.transform.position, Quaternion.identity);

        if (getSuccess)
            objectsPool[keyObject].Add(g2);
        else
        {
            objects = new List<GameObject>();
            objects.Add(g2);
            objectsPool[keyObject] = objects;
        }

        return g2;
    }
}
