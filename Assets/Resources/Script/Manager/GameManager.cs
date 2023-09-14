using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    GameObject meteorLine, meteorPrefab;

    public float gameSpeed;
    float spawnTime;
    float gameTime;

    bool spawnReady, meteorReady;
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
    //=======================<   ENEMYSPAWN       >=====================
    IEnumerator SpawnCoolTime()
    {
        spawnReady = false;
        yield return new WaitForSeconds(3f);
        spawnReady = true;
    }

    void spawn(int enemytype, int point)
    {
        GameObject enemy = Instantiate(Enemies[enemytype], 
            SpawnPoint[point].transform.position, SpawnPoint[point].transform.rotation);
        Rigidbody2D rb2 = enemy.GetComponent<Rigidbody2D>();
        EnemyController enemyLogic = enemy.GetComponent<EnemyController>();
        rb2.velocity = new Vector2(0, (enemyLogic.stats.Speed + gameSpeed) * (-1));
    }

    void EnemySpawn()
    {
        if (spawnReady)
        {
            int ranSpawn = Random.Range(0, 4);
            for(int i = 0; i < 4; i++)
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
    void MeteorSpawn() //���׿� ��Ʈ.1 ------- �ڷ�ƾ ȣ��
    {
        if (meteorReady == true && gameTime >= 15f)
        {
            StartCoroutine(MeteorReady());
            StartCoroutine(MeteorCoolTime());
        }
    }
    IEnumerator MeteorReady() // ���׿� ��Ʈ.2 ------ ��� ��ȯ
    {
        //���� ��ġ ����
        int ranSpawn = Random.Range(0, 4); 
        var meteor_Line = Instantiate(meteorLine, 
            (SpawnPoint[ranSpawn].transform.position + new Vector3(0, -5.5f, 0)),
            SpawnPoint[ranSpawn].transform.rotation);
        yield return new WaitForSeconds(2f);
        //2���� ���׿� ��ȯ �� ������ �״�� �ı�
        MeteorShot(meteor_Line.transform);
        Destroy(meteor_Line);
    }
    private void MeteorShot(Transform spawn) //���׿� ��Ʈ.3 -----���׿� ��ȯ
    {
        //���׿��� �������� ��ũ��Ʈ ����
        var meteor = Instantiate(meteorPrefab,
            (spawn.transform.position + new Vector3 (0, 5.5f, 0))
            ,spawn.transform.rotation);
    }
    IEnumerator MeteorCoolTime() //���׿� ��Ʈ.4  -----��Ÿ��
    {
        meteorReady = false;
        yield return new WaitForSeconds(6f);
        meteorReady = true;
    }
    //==================================================================
    //���Ӿ����� ��������Ʈ �¾�
    public void spawnPointSet()
    {
        for(int i = 0; i < SpawnPoint.Length; i++)
        {
            SpawnPoint[i] = GameObject.Find("SpawnPoint" + i);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        meteorLine = Resources.Load<GameObject>("Prefabs/object/warn_line");
        meteorPrefab = Resources.Load<GameObject>("Prefabs/object/meteor");
        spawnReady = true;
        StartCoroutine(MeteorCoolTime() );
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
                    EnemySpawn();
                    MeteorSpawn();
                    gameSpeed += 0.05f * Time.deltaTime;
                    gameTime += Time.deltaTime;
                }
                break;
        }

    }
}
