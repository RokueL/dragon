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

    Define.Pattern define = new Define.Pattern();

    GameObject meteorLine, meteorPrefab;

    public float gameSpeed;
    float spawnTime;
    float gameTime;

    bool spawnReady, meteorReady, rainReady;
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
    //=======================<   METEOREVENT       >=====================



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
        //2초후 메테오 소환 후 라인은 그대로 파괴
        MeteorShot(meteor_Line.transform);
        Destroy(meteor_Line);
    }
    private void MeteorShot(Transform spawn)  //메테오 루트.3 -----메테오 소환
    {
        //메테오에 떨어지는 스크립트 있음
        var meteor = Instantiate(meteorPrefab,
            (spawn.transform.position + new Vector3 (0, 5.5f, 0))
            ,spawn.transform.rotation);
    }
    IEnumerator MeteorCoolTime()              //메테오 루트.4  -----쿨타임
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
    IEnumerator RotationPattern()             //게임 패턴 로테이션
    {
        while (gameReady)
        {
            yield return new WaitForSeconds(10f);
            int ran = Random.Range(0, 2);
            define = (Define.Pattern)ran;
            if(define.ToString() == "MeteorRain")
            {
                rainReady = true;
            }
        }
        yield return null;
    }

    void GameLogic()                          //패턴에 따른 이벤트
    {
        StartCoroutine(RotationPattern());
        Debug.Log(define.ToString());
        switch (define.ToString())
        {
            case "EnemySpawn":
                EnemySpawn();
                MeteorSpawn();
                break;
            case "MeteorRain":
                
                break;
        }
    }                   
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        meteorLine = Resources.Load<GameObject>("Prefabs/object/warn_line");
        meteorPrefab = Resources.Load<GameObject>("Prefabs/object/meteor");
        
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
