using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warnLine : MonoBehaviour
{
    public GameObject Player;
    Vector2 playerPos;
    Vector2 nowPos, movePos;

    float moveSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        nowPos = transform.position;
        playerPos = new Vector2(Player.transform.position.x, 0);
        float dir = playerPos.x - nowPos.x;
        transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
    }
}
