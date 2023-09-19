using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneType;
using System.Drawing;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Define.Pattern define = new Define.Pattern();

    GameObject meteorLine, meteorLineStatic, breathLine, meteorPrefab, bossPrefab;

    public float gameSpeed;
    float spawnTime;
    float gameTime;
    float gameDistance;
    float rotationTime;

    bool spawnReady, meteorReady, rainReady, rotationReady, breathReady;
    public bool gameReady;
    public bool bossReady;

    public GameObject[] Enemies = new GameObject[2];
    GameObject[] SpawnPoint = new GameObject[5];

    public void gameStart()
    {
        spawnTime = 0;
        gameDistance = 0;
        gameSpeed = 0;
        gameTime = 0;
        rotationTime = 0;
        spawnReady = true;
        bossReady = false;
        StartCoroutine(MeteorCoolTime());
    }

    private void Awake()
    {
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


    #region LOBBY

    #endregion

    #region MAINGAME
    //=======================<   ITEM_USE       >=====================
    void useMagnet()
    {

    }
    void useDoubleShot()
    {

    }
    void useDoubleScore()
    {

    }
    public void useRush()
    {
        StartCoroutine(rush());
    }
    IEnumerator rush()
    {
        gameSpeed += 10f;
        yield return new WaitForSeconds(3f);
        gameSpeed -= 10f;
    }
    //=======================<   BOSS_SPAWN       >=====================
    void BossSpawn()       // �� Ÿ�� �� ���� ������ �޾ƿ��� �Լ�
    {
        GameObject enemy = Instantiate(bossPrefab,
            (SpawnPoint[2].transform.position + new Vector3(0, 2f, 0)), 
            SpawnPoint[2].transform.rotation);
    }
    //=======================<   BREATH       >=====================
    void breathSpawn()                        //�극�� ��Ʈ.1 ------- �ڷ�ƾ ȣ��
    {
        if (!breathReady)
        {
            StartCoroutine(BreathReady());
            StartCoroutine(BreathCoolTime());
        }
    }
    IEnumerator BreathReady()                 // �극�� ��Ʈ.2 ------ ��� ��ȯ
    {
        //���� ��ġ ����
        int ranSpawn = Random.Range(1, 4);
        Debug.Log(breathLine);
        var meteor_Line = Instantiate(breathLine,
            (SpawnPoint[ranSpawn].transform.position + new Vector3(0, -5.5f, 0)),
            transform.rotation);
        yield return null;
    }
    IEnumerator BreathCoolTime()              //�귡�� ��Ʈ.3  -----��Ÿ��
    {
        breathReady = true;
        yield return new WaitForSeconds(5f);
        breathReady = false;
    }
    //=======================<   METEOR_RAIN       >=====================
    void MeteorRainSpawn()                        //���׿����� ��Ʈ.1 ------- �ڷ�ƾ ȣ��
    {
        if (!rainReady)
        {
            StartCoroutine(MeteorRainReady());
            StartCoroutine(MeteorRainCoolTime());
        }
    }
    IEnumerator MeteorRainReady()                 // ���׿����� ��Ʈ.2 ------ ��� ��ȯ
    {
        //���� ��ġ ����
        for (int i = 0; i < 6; i++)
        {
            yield return new WaitForSeconds(0.5f);
            int ranSpawn = Random.Range(0, 5);
            var meteor_Line = Instantiate(meteorLineStatic,
                (SpawnPoint[ranSpawn].transform.position + new Vector3(0, -5.5f, 0)),
                SpawnPoint[ranSpawn].transform.rotation);
        }
        Debug.Log("Rain End");
    }
    IEnumerator MeteorRainCoolTime()              //���׿����� ��Ʈ.3  -----��Ÿ��
    {
        rainReady = true;
        yield return new WaitForSeconds(11f);
        Debug.Log("RainOn");
        rainReady = false;
    }
    //=======================<   ENEMYSPAWN       >=====================
    IEnumerator SpawnCoolTime()                //���� ��Ÿ��
    {
        spawnReady = false;
        yield return new WaitForSeconds(3f);
        spawnReady = true;
    }
    void spawn(int enemytype, int point)       // �� Ÿ�� �� ���� ������ �޾ƿ��� �Լ�
    {
        GameObject enemy = Instantiate(Enemies[enemytype], 
            SpawnPoint[point].transform.position, SpawnPoint[point].transform.rotation);
        Rigidbody2D rb2 = enemy.GetComponent<Rigidbody2D>();
        EnemyController enemyLogic = enemy.GetComponent<EnemyController>();
        enemyLogic.type = enemytype;
        rb2.velocity = new Vector2(0, (enemyLogic.stats.Speed + gameSpeed) * (-1));
    }
    void EnemySpawn()                          // ���� �غ� �Ǹ� ���� ����Ʈ �� �� ���� �Ϲ� �巡�� ��ȯ
    {
        if (spawnReady && gameTime >= 4f)
        {
            int ranSpawn = Random.Range(0, 4);
            for(int i = 0; i < SpawnPoint.Length; i++)
            {
                if (i != ranSpawn)
                {
                    spawn(0,i);
                }
                else
                {
                    spawn(1, i);
                }
            }
            StartCoroutine(SpawnCoolTime());
        }
    }
    //=======================<   METEOR       >=========================
    void MeteorSpawn()                        //���׿� ��Ʈ.1 ------- �ڷ�ƾ ȣ��
    {
        if (meteorReady == true && gameTime >= 15f)
        {
            StartCoroutine(MeteorReady());
            StartCoroutine(MeteorCoolTime());
        }
    }
    IEnumerator MeteorReady()                 // ���׿� ��Ʈ.2 ------ ��� ��ȯ
    {
        //���� ��ġ ����
        int ranSpawn = Random.Range(0, 4); 
        var meteor_Line = Instantiate(meteorLine, 
            (SpawnPoint[ranSpawn].transform.position + new Vector3(0, -5.5f, 0)),
            SpawnPoint[ranSpawn].transform.rotation);
        yield return new WaitForSeconds(2f);
    }
    IEnumerator MeteorCoolTime()              //���׿� ��Ʈ.3  -----��Ÿ��
    {
        meteorReady = false;
        yield return new WaitForSeconds(6f);
        meteorReady = true;
    }
    //==================================================================
    public void spawnPointSet()               //���Ӿ����� ��������Ʈ �¾�
    {
        for(int i = 0; i < SpawnPoint.Length; i++)
        {
            SpawnPoint[i] = GameObject.Find("SpawnPoint" + i);
        }
    }
    void RotationPattern()
    {
        rotationTime += 1 * Time.deltaTime;

        if (rotationTime >= 10 && !bossReady)
        {
            int ran = Random.Range(0, 10);
            if(ran <= 4)
            {
                define = (Define.Pattern)0;
            }
            else if( ran ==6 || ran == 5)
            {
                define = (Define.Pattern)1;
            }
            else if (ran == 7 || ran == 8)
            {
                define = (Define.Pattern)2;
            }
            else
            {
                define = (Define.Pattern)2;
            }
            Debug.Log("ran = " + ran);
            rotationTime = 0;
        }
    }
    void bossCheck()
    {
        if (gameDistance >= 30f)
        {
            bossReady = true;
            define = (Define.Pattern)3;
            BossSpawn();
            Debug.Log("boss ON");
            gameDistance = 0;
        }
        else
        {
            gameDistance += 1 * Time.deltaTime;
        }
    }
    void GameLogic()                          //���Ͽ� ���� �̺�Ʈ
    {
        //Debug.Log(define.ToString());
        bossCheck();
        RotationPattern();
        switch (define.ToString())
        {
            case "EnemySpawn":
                EnemySpawn();
                MeteorSpawn();
                break;
            case "MeteorRain":
                MeteorRainSpawn();
                break;
            case "FireBreath":
                breathSpawn();
                break;
            case "Boss":
                Debug.Log("ChangeBool : Boss");
                break;
        }
    }                   
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        meteorLine = Resources.Load<GameObject>("Prefabs/object/warn_line");
        meteorLineStatic = Resources.Load<GameObject>("Prefabs/object/warn_line_static");
        breathLine = Resources.Load<GameObject>("Prefabs/object/breath_line");
        bossPrefab = Resources.Load<GameObject>("Prefabs/Monster/Boss");

        spawnReady = true;
        bossReady = false;
        gameDistance = 0;
        StartCoroutine(MeteorCoolTime());
    }



    // Update is called once per frame
    void Update()
    {
        switch(sceneManager.Instance.define.sceneType.ToString()) 
        {
            case "First":
                break;
            case "Lobby":
                break;
            case "Game":
                if (gameReady)
                {
                    GameLogic();
                    gameSpeed += 0.05f * Time.deltaTime;
                    gameTime += Time.deltaTime;
                }
                break;
        }

    }
}
