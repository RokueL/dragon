using SceneType;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class mainCotroller : MonoBehaviour
{
    public TextMeshProUGUI KillScore;
    public TextMeshProUGUI DistanceScore;
    public TextMeshProUGUI coinScore;

    float distanceScore;


    // Start is called before the first frame update
    void Start()
    {
        distanceScore = 0;

        KillScore = GameObject.Find("TEXT_KillPoint").GetComponent<TextMeshProUGUI>();
        coinScore = GameObject.Find("TEXT_Money").GetComponent<TextMeshProUGUI>();
        DistanceScore = GameObject.Find("TEXT_DistancePoint").GetComponent<TextMeshProUGUI>();

        scoreManager.Instance.Reset();
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
        distanceScore += 1111f * Time.deltaTime;
        KillScore.text = scoreManager.Instance.killScore.ToString();
        coinScore.text = scoreManager.Instance.coinScore.ToString();
        DistanceScore.text = Mathf.Round(distanceScore).ToString();
    }
}
