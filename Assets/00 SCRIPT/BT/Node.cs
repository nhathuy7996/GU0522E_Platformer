using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum nodeState
{
    FAIL,
    RUNNING,
    SUCCESS
}

[System.Serializable]
public class Node : MonoBehaviour
{
    [SerializeField]
    protected Node _parent = null;

    [SerializeField]
    protected List<Node> _childrens = new List<Node>();

    public Node()
    {

    }

    public void Init()
    {

    }

    public void Attach(Node node)
    {
        _childrens.Add(node);

        node.setParent(this);
    }

    public void Dettach(Node node)
    {
        _childrens.Remove(node);

        node.setParent(null);
    }

    public void setParent(Node node)
    {
        this._parent = node;
    }

    public virtual nodeState Evaluate()
    {
        return nodeState.FAIL;
    }
}
