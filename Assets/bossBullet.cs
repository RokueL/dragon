using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datas;

public class bossBullet : MonoBehaviour
{
    GameObject bullet;
    Stats stats = new Stats();
    GameObject Player;
    Vector3 playerDir;
    Rigidbody2D rb;

    void Setup()
    {
        stats.Damage = 1;
        stats.Speed = 2f;
    }
    // Start is called before the first frame update
    void Start()
    {
        Setup();
        Player = GameObject.FindWithTag("Player");
        bullet = Resources.Load<GameObject>("Prefabs/Object/bossBullet");
        rb = GetComponent<Rigidbody2D>();
        playerDir = Player.transform.position;
        rb.velocity = playerDir * stats.Speed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
