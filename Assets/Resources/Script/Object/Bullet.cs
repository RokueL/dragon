using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Datas;

public class Bullet : MonoBehaviour
{
    public Stats stats = new Stats();

    Rigidbody2D rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        Setup();
    }

    void Setup()
    {
        stats.Speed = 18f;
        stats.Damage = 20f;
    }
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector3.up * stats.Speed;
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
