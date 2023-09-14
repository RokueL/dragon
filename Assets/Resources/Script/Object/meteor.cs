using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datas;

public class meteor : MonoBehaviour
{
    public Stats stats = new Stats();

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        stats.Speed = 3f;
        stats.Damage = 20f;

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = stats.Speed + GameManager.Instance.gameSpeed;
        if(speed >= 10f)
        {
            speed = 10;
        }

        rb.velocity = Vector3.down * (speed); ;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Board")
        {
            Destroy(this.gameObject);
        }
    }
}
