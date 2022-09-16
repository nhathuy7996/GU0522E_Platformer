using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tree : MonoBehaviour
{

    [SerializeField]
    protected List<Node> _nodes;

    // Start is called before the first frame update
    void Start()
    {
        SetUpTree();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(_nodes != null)
        {
            foreach (Node n in _nodes)
            {
                if (n == null)
                    continue;
                if (n.Evaluate() == nodeState.RUNNING)
                    return;
            }
        }
    }

    protected virtual void SetUpTree()
    {

    }


}
