using SceneType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datas;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    Stats stats = new Stats();

    GameObject bullet, deadEffect;
    GameObject dropCoin;
    GameObject[] dropsGem = new GameObject[3];
    GameObject[] dropsItem = new GameObject[4];

    enum Pattern
    {
        BulletShot,
        SpreadShot
    }
    Pattern pattern;

    bool moveEnd, bulletShootReady;
    public GameObject firePos;

    Vector3 dir;
    Vector2 ranDir;

    int ranGem, ranItem;

    float rotationTime;
    float ranX;

    void SetUp()
    {
        stats.HP = 200;
    }

    private void Awake()
    {
        SetUp();
    }

    // Start is called before the first frame update
    private void Start()
    {
        bullet = Resources.Load<GameObject>("Prefabs/object/bossBullet");
        deadEffect = Resources.Load<GameObject>("Prefabs/Object/smoke");
        dropCoin = Resources.Load<GameObject>("Prefabs/Object/item_coin");
        for (int i = 0; i < 3; i++)
        {
            dropsGem[i] = Resources.Load<GameObject>("Prefabs/Object/item_gem" + i);
        }
        for (int i = 0; i < 4; i++)
        {
            dropsItem[i] = Resources.Load<GameObject>("Prefabs/Object/item_special" + i);
        }


        dir = new Vector3(0, 3.9f, 0) - transform.position;
    }

    #region PATTERN
    //=======================<   보스 패턴 변화       >=====================
    void BossPattern()
    {
        rotationTime += Time.deltaTime;
        if (rotationTime > 3f){
            int ran = Random.Range(0, 2);
            switch (ran)
            {
                case 0:
                    pattern = (Pattern)0;
                    break;
                case 1:
                    pattern = (Pattern)0;
                    break;
            }
            rotationTime = 0;
        }
    }
   // =======================<   총알 발사       >=====================
   void bulletShot()
    {
        if (!bulletShootReady)
        {
            Instantiate(bullet, firePos.transform.position, firePos.transform.rotation);
            StartCoroutine(bulletShotCoolTime());
        }
    }
    IEnumerator bulletShotCoolTime()
    {
        bulletShootReady = true;
        yield return new WaitForSeconds(0.5f);
        bulletShootReady = false;
    }
    // ===================================================================
    void GameLogic() 
    {
        BossPattern();
        switch (pattern.ToString())
        {
            case "BulletShot":
                bulletShot();
                break;
        }
    }
#endregion
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= 4f)
        {
            transform.Translate(dir * 0.1f * Time.deltaTime);
        }
        else
        {
            moveEnd = true;
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        if (moveEnd)
        {
            GameLogic();
        }
    }
    #region HIT_LOGIC
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && moveEnd)
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.stats.Damage);
        }
        if (collision.gameObject.name == "wall_Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    void OnHit(float dmg)
    {
        stats.HP -= dmg;
        if (stats.HP <= 0)
        {
            OnDie();
        }
    }

    void OnDie()
    {
        GameManager.Instance.bossReady = false;
        GameManager.Instance.define = (Define.Pattern)0;
        this.gameObject.SetActive(false);
        Destroy(this.gameObject, 5f);
        var smoke = Instantiate(deadEffect, transform.position, transform.rotation);
        Destroy(smoke, 0.5f);
        for(int i = 0; i< Random.Range(5,10); i++)
        {
            ranDrop();
        }
    }
    void ranDrop()
    {
        int ran = Random.Range(0, 10);
        if (ran == 7 || ran == 8)
        {
            ranGem = Random.Range(0, 3);
            ranX = Random.Range(-1.5f, 1.5f);
            ranDir = new Vector2(ranX, Random.Range(2,6));
            var gem = Instantiate(dropsGem[ranGem], transform.position, transform.rotation);
            gem.gameObject.GetComponent<Rigidbody2D>().AddForce(ranDir, ForceMode2D.Impulse);
        }
        else if (ran == 9)
        {
            ranItem = Random.Range(0, 4);
            ranX = Random.Range(-1.5f, 1.5f);
            ranDir = new Vector2(ranX, Random.Range(2, 6));
            var item = Instantiate(dropsItem[ranItem], transform.position, transform.rotation);
            item.gameObject.GetComponent<Rigidbody2D>().AddForce(ranDir, ForceMode2D.Impulse);
        }
        else
        {
            ranX = Random.Range(-1.5f, 1.5f);
            ranDir = new Vector2(ranX, Random.Range(2, 6));
            var coin = Instantiate(dropCoin, transform.position, transform.rotation);
            coin.gameObject.GetComponent<Rigidbody2D>().AddForce(ranDir, ForceMode2D.Impulse);
        }

    }
}
#endregion
