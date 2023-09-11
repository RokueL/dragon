using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datas;

public class EnemyController : MonoBehaviour
{
    public Stats stats = new Stats();

    SpriteRenderer spriteRenderer;


    private void Awake()
    {
        stats.Damage = 25f;
        stats.HP = 50f;
        stats.Speed = 3f;
    }

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnHit(float dmg)
    {
        stats.HP -= dmg;
        if (stats.HP <= 0)
        {
            Destroy(gameObject);
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
