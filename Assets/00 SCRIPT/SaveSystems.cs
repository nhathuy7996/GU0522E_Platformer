using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class SaveSystems : MonoBehaviour
{

    private static SaveSystems _instant;
    public static SaveSystems instant => _instant;

    [SerializeField] Transform _canvas;
    [SerializeField] Button _button;

    // Start is called before the first frame update
    void Awake()
    {
        if(_instant == null)
            _instant = this;
        else
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }


    private void Start()
    {
        LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveData()
    {
        string dataPlayer = JsonUtility.ToJson(GameController.instant.player._playerData);

        string dataAllPlayers =  PlayerPrefs.GetString("SAVE_FILE","[]");

        if (dataAllPlayers.EndsWith("]"))
        {
            dataAllPlayers = dataAllPlayers.Substring(0, dataAllPlayers.Length -1);
        }

        dataAllPlayers += "," + dataPlayer + "]";


        PlayerPrefs.SetString("SAVE_FILE", dataAllPlayers);
    }

    public void LoadData() {
        string dataAllPlayers = PlayerPrefs.GetString("SAVE_FILE", "[]");

        var dataParsed = JSON.Parse(dataAllPlayers);
        int i = 0;
        foreach (var j in dataParsed)
        {
            //spaw button
            Button b = Instantiate<Button>(_button, _canvas.position, Quaternion.identity, _canvas);
            b.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
            b.onClick.AddListener(() =>
            {
                var dataPlayer = JSON.Parse(j.ToString());
                dataParsed = JSON.Parse(dataPlayer[0].ToString());
                PlayerController player = GameController.instant.choosePlayer(dataPlayer[0]["INDEX"]);
                _canvas.gameObject.SetActive(false);

                player._playerData.HP = dataParsed["HP"];
                player._playerData.GOLD = dataParsed["GOLD"];
                player._playerData.LEVEL = dataParsed["LEVEL"];
                player._playerData.EXP = dataParsed["EXP"];
            });
            i++;
        }


    }

}

[System.Serializable]
public struct PlayerData
{
    public float HP;
    public float EXP;
    public float GOLD;
    public int LEVEL;
    public int PLAYER_INDEX;

}
