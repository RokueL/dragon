using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    Vector3 nowPos, movePos;
    float moveSpeed = 1f;
    float mapMinRange = -10.2f;
    float mapMaxRange = 20.4f;

    // Start is called before the first frame update
    void Start()
    {
        nowPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveBG();
    }

    void moveBG()
    {
        if (transform.position.y < mapMinRange)
        {
            transform.position += Vector3.up * mapMaxRange;
        }


        nowPos = transform.position;
        movePos = Vector3.down * (moveSpeed + GameManager.Instance.gameSpeed) * Time.deltaTime;
        transform.position = nowPos + movePos;
    }
}
