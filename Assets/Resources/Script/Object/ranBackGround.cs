using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ranBackGround : MonoBehaviour
{

    void randomBG()
    {
        GameObject[] bgs = new GameObject[2];
        for (int i = 0; i < bgs.Length; i++)
        {
            bgs[i] = GameObject.Find("BG0" + i);
        }

        int ran = Random.Range(0, 5);
        bgs[0].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/"+ran);
        bgs[1].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Textures/" + ran);
    }

    // Start is called before the first frame update
    void Start()
    {
        randomBG();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
