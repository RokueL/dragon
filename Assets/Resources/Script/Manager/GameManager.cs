using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SceneType;
using System.Drawing;
using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Define.Pattern define = new Define.Pattern();

    GameObject meteorLine, meteorLineStatic, breathLine, meteorPrefab;

    public float gameSpeed;
    float spawnTime;
    float gameTime;
    float rotationTime;

    bool spawnReady, meteorReady, rainReady, rotationReady, breathReady;
    public bool gameReady;

    public GameObject[] Enemies = new GameObject[2];
    GameObject[] SpawnPoint = new GameObject[5];


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
    //=======================<   BREATH       >=====================
    void breathSpawn()                        //브레스 루트.1 ------- 코루틴 호출
    {
        if (!breathReady)
        {
            StartCoroutine(BreathReady());
            StartCoroutine(BreathCoolTime());
        }
    }
    IEnumerator BreathReady()                 // 브레스 루트.2 ------ 경고선 소환
    {
        //랜덤 위치 스폰
        int ranSpawn = Random.Range(1, 4);
        Debug.Log(breathLine);
        var meteor_Line = Instantiate(breathLine,
            (SpawnPoint[ranSpawn].transform.position + new Vector3(0, -5.5f, 0)),
            SpawnPoint[ranSpawn].transform.rotation);
        yield return null;
    }
    IEnumerator BreathCoolTime()              //브래스 루트.3  -----쿨타임
    {
        breathReady = true;
        yield return new WaitForSeconds(5f);
        breathReady = false;
    }
    //=======================<   METEOR_RAIN       >=====================
    void MeteorRainSpawn()                        //메테오레인 루트.1 ------- 코루틴 호출
    {
        if (!rainReady)
        {
            StartCoroutine(MeteorRainReady());
            StartCoroutine(MeteorRainCoolTime());
        }
    }
    IEnumerator MeteorRainReady()                 // 메테오레인 루트.2 ------ 경고선 소환
    {
        //랜덤 위치 스폰
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
    IEnumerator MeteorRainCoolTime()              //메테오레인 루트.3  -----쿨타임
    {
        rainReady = true;
        yield return new WaitForSeconds(11f);
        Debug.Log("RainOn");
        rainReady = false;
    }
    //=======================<   ENEMYSPAWN       >=====================
    IEnumerator SpawnCoolTime()                //스폰 쿨타임
    {
        spawnReady = false;
        yield return new WaitForSeconds(3f);
        spawnReady = true;
    }
    void spawn(int enemytype, int point)       // 적 타입 과 스폰 지점을 받아오는 함수
    {
        GameObject enemy = Instantiate(Enemies[enemytype], 
            SpawnPoint[point].transform.position, SpawnPoint[point].transform.rotation);
        Rigidbody2D rb2 = enemy.GetComponent<Rigidbody2D>();
        EnemyController enemyLogic = enemy.GetComponent<EnemyController>();
        rb2.velocity = new Vector2(0, (enemyLogic.stats.Speed + gameSpeed) * (-1));
    }
    void EnemySpawn()                          // 스폰 준비가 되면 랜덤 포인트 한 곳 제외 일반 드래곤 소환
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
    void MeteorSpawn()                        //메테오 루트.1 ------- 코루틴 호출
    {
        if (meteorReady == true && gameTime >= 15f)
        {
            StartCoroutine(MeteorReady());
            StartCoroutine(MeteorCoolTime());
        }
    }
    IEnumerator MeteorReady()                 // 메테오 루트.2 ------ 경고선 소환
    {
        //랜덤 위치 스폰
        int ranSpawn = Random.Range(0, 4); 
        var meteor_Line = Instantiate(meteorLine, 
            (SpawnPoint[ranSpawn].transform.position + new Vector3(0, -5.5f, 0)),
            SpawnPoint[ranSpawn].transform.rotation);
        yield return new WaitForSeconds(2f);
        ////2초후 메테오 소환 후 라인은 그대로 파괴
        //MeteorShot(meteor_Line.transform);
        //Destroy(meteor_Line);
    }
    IEnumerator MeteorCoolTime()              //메테오 루트.3  -----쿨타임
    {
        meteorReady = false;
        yield return new WaitForSeconds(6f);
        meteorReady = true;
    }
    //==================================================================
    public void spawnPointSet()               //게임씬에서 스폰포인트 셋업
    {
        for(int i = 0; i < SpawnPoint.Length; i++)
        {
            SpawnPoint[i] = GameObject.Find("SpawnPoint" + i);
        }
    }
    void RotationPattern()
    {
        rotationTime += 1 * Time.deltaTime;
        if (rotationTime >= 10)
        {
            int ran = Random.Range(0, 10);
            if(ran <= 6)
            {
                define = (Define.Pattern)0;
            }
            else if( ran ==7 || ran == 6)
            {
                define = (Define.Pattern)1;
            }
            else if (ran == 8)
            {
                define = (Define.Pattern)2;
            }
            else
            {
                Debug.Log("BOSS");
                define = (Define.Pattern)2;
            }
            Debug.Log("ran = " + ran);
            rotationTime = 0;
        }
    }
    void GameLogic()                          //패턴에 따른 이벤트
    {
        RotationPattern();
        //Debug.Log(define.ToString());
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
        }
    }                   
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        meteorLine = Resources.Load<GameObject>("Prefabs/object/warn_line");
        meteorLineStatic = Resources.Load<GameObject>("Prefabs/object/warn_line_static");
        breathLine = Resources.Load<GameObject>("Prefabs/object/breath_line");

        spawnReady = true;
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
