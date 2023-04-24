using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static GameController _instant;
    public static GameController instant => _instant;

    [SerializeField] List<PlayerController> PlayersList = new List<PlayerController>();
    [SerializeField] GameObject _selectPlayerCanvas;

    [SerializeField] PlayerController _player;
    public PlayerController player => _player;

    private void Awake()
    {
        _instant = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //int currentPlayerIndex = PlayerPrefs.GetInt("CURRENT_PLAYER_INDEX", -1);
        //if (currentPlayerIndex != -1)
        //{
        //    _player = Instantiate<PlayerController>(PlayersList[currentPlayerIndex], this.transform.position, Quaternion.identity);
        //    Camera.main.transform.SetParent(_player.transform);
        //    _selectPlayerCanvas.SetActive(false);
        //    _player._playerData.PLAYER_INDEX = currentPlayerIndex;
        //    return;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AdManager.instant.onInterRewared += () =>
            {
                Debug.LogError("Add 100 gold to player!");
            };
            AdManager.instant.ShowInter();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AdManager.instant.onVideoRewarded += () =>
            {
                Debug.LogError("Add 100 gold to player!");
            };
            AdManager.instant.ShowRewared();
        }
    }

    public PlayerController choosePlayer(int index)
    {
        _player = Instantiate<PlayerController>(PlayersList[index], this.transform.position, Quaternion.identity);
        _player._playerData.PLAYER_INDEX = index;
        Camera.main.transform.SetParent(_player.transform);
        PlayerPrefs.SetInt("CURRENT_PLAYER_INDEX", index);
        _selectPlayerCanvas.SetActive(false);

        return _player;
    }

    
}
