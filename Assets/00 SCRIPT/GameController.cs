using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController _instant;
    public static GameController instant => _instant;

    [SerializeField] PlayerController _player;
    public PlayerController player => _player;

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
}
