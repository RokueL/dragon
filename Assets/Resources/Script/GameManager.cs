using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

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

            //이거 왜 작동 안하지...? 밑으로 내려 보내야 하는데.....
            rb2.velocity = new Vector2(0, enemyLogic.stats.Speed * (-1));
            StartCoroutine(SpawnEnemies());
        }
    }


    IEnumerator MeteorReady()
    {
        int ranSpawn = Random.Range(0, 4);
        var meteor_Line = Instantiate(meteorLine, 
            (SpawnPoint[ranSpawn].transform.position + new Vector3(0, -5.5f, 0)),
            SpawnPoint[ranSpawn].transform.rotation);
        yield return new WaitForSeconds(3f);
        MeteorShot(meteor_Line.transform);
        Destroy(meteor_Line);
    }

    IEnumerator MeteorCoolTime()
    {
        meteorReady = false;
        yield return new WaitForSeconds(4f);
        meteorReady = true;
    }

    public void MeteorShot(Transform spawn)
    {
        //메테오에 떨어지는 스크립트 있음
        var meteor = Instantiate(meteorPrefab,
            (spawn.transform.position + new Vector3 (0, 5.5f, 0))
            ,spawn.transform.rotation);
    }

    void MeteorSpawn()
    {
        if (meteorReady == true)
        {
            StartCoroutine(MeteorReady());
            StartCoroutine(MeteorCoolTime());
        }
    }






    // Start is called before the first frame update
    void Start()
    {
        meteorLine = Resources.Load<GameObject>("Prefabs/warn_line");
        meteorPrefab = Resources.Load<GameObject>("Prefabs/meteor");
        spawnReady = true;
        meteorReady = true;
    }



    // Update is called once per frame
    void Update()
    {
        EnemySpawn();
        MeteorSpawn();
    }
}
