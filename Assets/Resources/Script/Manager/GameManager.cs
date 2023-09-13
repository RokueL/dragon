using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    GameObject meteorLine, meteorPrefab;

    float spawnTime;
    bool spawnReady, meteorReady;
    public GameObject[] Enemies = new GameObject[1];
    public GameObject[] SpawnPoint = new GameObject[4];


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
    IEnumerator SpawnEnemies()
    {
        spawnReady = false;
        yield return new WaitForSeconds(1f);
        spawnReady = true;
    }
    void EnemySpawn()
    {
        if (spawnReady)
        {
            int ranEnemy = Random.Range(0, 1);
            int ranSpawn = Random.Range(0, 4);
            GameObject enemy = Instantiate(Enemies[ranEnemy], SpawnPoint[ranSpawn].transform.position, SpawnPoint[ranSpawn].transform.rotation);
            Rigidbody2D rb2 = enemy.GetComponent<Rigidbody2D>();
            EnemyController enemyLogic = enemy.GetComponent<EnemyController>();

            //��ȣ �ٽ� �۵��Ѵ�
            rb2.velocity = new Vector2(0, enemyLogic.stats.Speed * (-1));
            StartCoroutine(SpawnEnemies());
        }
    }
    //=======================<   METEOR       >=========================
    void MeteorSpawn() //���׿� ��Ʈ.1
    {
        if (meteorReady == true)
        {
            StartCoroutine(MeteorReady());
            StartCoroutine(MeteorCoolTime());
        }
    }
    IEnumerator MeteorReady() // ���׿� ��Ʈ.2
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
    private void MeteorShot(Transform spawn) //���׿� ��Ʈ.3
    {
        //���׿��� �������� ��ũ��Ʈ ����
        var meteor = Instantiate(meteorPrefab,
            (spawn.transform.position + new Vector3 (0, 5.5f, 0))
            ,spawn.transform.rotation);
    }
    IEnumerator MeteorCoolTime() //���׿� ��Ʈ.4
    {
        meteorReady = false;
        yield return new WaitForSeconds(4f);
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
        meteorLine = Resources.Load<GameObject>("Prefabs/warn_line");
        meteorPrefab = Resources.Load<GameObject>("Prefabs/meteor");
        spawnReady = true;
        StartCoroutine(MeteorCoolTime() );
    }



    // Update is called once per frame
    void Update()
    {
        switch(SceneManager.GetActiveScene().name) 
        {
            case "FirstScene":
                break;
            case "LobbyScene":
                break;
            case "GameScene":
                EnemySpawn();
                MeteorSpawn();
                break;
        }

    }
}
