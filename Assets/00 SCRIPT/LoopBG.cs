using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopBG : MonoBehaviour
{
    [SerializeField] Transform _player;
    float _inGameWidth;
    // Start is called before the first frame update
    void Start()
    {
        int width = this.GetComponent<SpriteRenderer>().sprite.texture.width;
        _inGameWidth = width / this.GetComponent<SpriteRenderer>().sprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player == null)
            return;
        if (Mathf.Abs(_player.position.x - this.transform.position.x) >= ( _inGameWidth))
        {
            Vector3 pos = this.transform.position;
            pos.x = _player.position.x;

            this.transform.position = pos;
        }
    }
}
