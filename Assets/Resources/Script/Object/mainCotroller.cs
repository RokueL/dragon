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

    public TextMeshProUGUI endkill;
    public TextMeshProUGUI endDis;
    public TextMeshProUGUI endcoin;

    public Button button;

    float distanceScore;

    GameObject player;
    public GameObject endPannel;

    public Image[] life = new Image[2];
    public Sprite[] lifeSprite = new Sprite[2];

    bool isgame;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.gameStart();
        distanceScore = 0;
        player = GameObject.FindWithTag("Player");
        KillScore = GameObject.Find("TEXT_KillPoint").GetComponent<TextMeshProUGUI>();
        coinScore = GameObject.Find("TEXT_Money").GetComponent<TextMeshProUGUI>();
        DistanceScore = GameObject.Find("TEXT_DistancePoint").GetComponent<TextMeshProUGUI>();

        scoreManager.Instance.Reset();
        sceneManager.Instance.LobbyButtonSetup(button);
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
        if (!isgame)
        {
            distanceScore += 1111f * Time.deltaTime;
        }
        KillScore.text = scoreManager.Instance.killScore.ToString();
        coinScore.text = scoreManager.Instance.coinScore.ToString();
        DistanceScore.text = Mathf.Round(distanceScore).ToString();

        if (player.GetComponent<Player>().playerState == Player.PlayerState.Die)
        {
            GameEnd();
        }

        if (player.GetComponent<Player>().stats.HP == 3)
        {
            life[0].sprite = lifeSprite[0];
            life[1].sprite = lifeSprite[0];
        }
        else if(player.GetComponent<Player>().stats.HP == 2)
        {
            life[0].sprite = lifeSprite[0];
            life[1].sprite = lifeSprite[1];
        }
        else if(player.GetComponent<Player>().stats.HP == 1){
            life[0].sprite = lifeSprite[1];
            life[1].sprite = lifeSprite[1];
        }

        
    }

    void GameEnd()
    {
        isgame = true;
        endPannel.SetActive(true);
        GameManager.Instance.gameReady = false;
        endcoin.text = ("재화 : " + scoreManager.Instance.coinScore.ToString());
        endkill.text = ("처치 : " + scoreManager.Instance.coinScore.ToString());
        endDis.text = ("거리 : " + Mathf.Round(distanceScore).ToString());
    }
}
