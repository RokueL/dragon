using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_Logo : MonoBehaviour
{

    float originPos;
    float min, max;
    float direction = 1;
    public float moveSpeed = 0.1f;


    // Start is called before the first frame update
    void Start()
    {
        originPos = transform.position.y;
        min = originPos - 0.1f;
        max = originPos + 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tr = transform.position;

        if (tr.y <= min)
        {
            direction = 1;
            //Debug.Log(tr.y);
        }
        else if (tr.y >= max)
        {
            direction = -1;
            //Debug.Log (tr.y);
        }

        tr.y += direction * moveSpeed * Time.deltaTime;
        transform.position = tr;
    }
}

