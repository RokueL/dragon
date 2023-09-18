using SceneType;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mainCotroller : MonoBehaviour
{
    TextMeshPro KillScore;
    TextMeshPro DistanceScore;
    TextMeshPro coinScore;


    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.spawnPointSet();
        StartCoroutine(WaitSeconds());
    }

    IEnumerator WaitSeconds()
    {
        for(int i = 0; i< 3; i++)
        {
            yield return new WaitForSeconds(1f);
        }
        GameManager.Instance.gameReady = true;
    }

    

    // Update is called once per frame
    void Update()
    {

    }
}
