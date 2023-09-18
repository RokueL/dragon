using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{
    public static scoreManager Instance;

    public float coinScore;
    public float killScore;

    public void killCount(int type)
    {
        if(type == 0)
        {
            killScore += 50;
        }
        else if(type == 1)
        {
            killScore += 100;
        }
        else if (type == 2)
        {
            killScore += 500;
        }
    }

    public void pickCoin(int type)
    {
        if(type == 0)
        {
            coinScore += 10;
        }
        else if (type == 1)
        {
            coinScore += 50;
        }
        else if (type == 2)
        {
            coinScore += 100;
        }
        else if(type == 3)
        {
            coinScore += 500;
        }
    }

    public void Reset()
    {
        coinScore = 0;
        killScore = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        Reset();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
