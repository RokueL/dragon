using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datas;

public class EnemyController : MonoBehaviour
{
    public Stats stats = new Stats();

    SpriteRenderer spriteRenderer;
    GameObject deadEffect;
    GameObject dropCoin;
    GameObject[] dropsGem = new GameObject[3];
    GameObject[] dropsItem = new GameObject[4];

    int ranGem, ranItem;
    float ranX;
    Vector2 ranDir;

    private void Awake()
    {
        stats.Damage = 25f;
        stats.HP = 50f;
        stats.Speed = 1.5f;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        deadEffect = Resources.Load<GameObject>("Prefabs/Object/smoke");
        dropCoin = Resources.Load<GameObject>("Prefabs/Object/item_coin");
        for(int i = 0; i<3; i++)
        {
            dropsGem[i] = Resources.Load<GameObject>("Prefabs/Object/item_gem" + i);
        }
        for(int i = 0;i<4; i++)
        {
            dropsItem[i] = Resources.Load<GameObject>("Prefabs/Object/item_special" + i);
        }
    }

    void OnHit(float dmg)
    {
        stats.HP -= dmg;
        if (stats.HP <= 0)
        {
            Destroy(gameObject);
            var smoke = Instantiate(deadEffect, transform.position, transform.rotation);
            Destroy(smoke, 0.5f);
            ranDrop();
        }
    }

    void ranDrop()
    {
        int ran = Random.Range(0, 10);
        if(ran == 7 || ran == 8)
        {
            ranGem = Random.Range(0, 3);
            ranX = Random.Range(-0.5f, 0.5f);
            ranDir = new Vector2(ranX, 1);
            var gem = Instantiate(dropsGem[ranGem],transform.position,transform.rotation);
            gem.gameObject.GetComponent<Rigidbody2D>().AddForce(ranDir, ForceMode2D.Impulse);
        }
        else if( ran == 9)
        {
            ranItem = Random.Range(0, 4);
            ranX = Random.Range(-0.5f, 0.5f);
            ranDir = new Vector2(ranX, 1);
            var item = Instantiate(dropsItem[ranItem], transform.position, transform.rotation);
            item.gameObject.GetComponent<Rigidbody2D>().AddForce(ranDir, ForceMode2D.Impulse);
        }
        else
        {
            ranDir = new Vector2(ranX, 1);
            var coin = Instantiate(dropCoin, transform.position, transform.rotation);
            coin.gameObject.GetComponent<Rigidbody2D>().AddForce(ranDir, ForceMode2D.Impulse);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Bullet bullet = collision.gameObject.GetComponent<Bullet>();
            OnHit(bullet.stats.Damage);
        }
        if (collision.gameObject.name == "wall_Enemy")
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
