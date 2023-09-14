using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using SceneType;
using Datas; //ü���� ���� ������Ʈ�� ����

public class Player : MonoBehaviour
{
    Stats stats = new Stats();
    Define define = new Define();

    float h, delayTime;

    Vector3 dir;
    Vector3 nowPos, movePos;
    public bool touch_Left, touch_Right;
    bool fireReady;

    GameObject bulletPrefab;
    public GameObject FirePos;


    void Setup()
    {
        define.sceneType = sceneManager.Instance.define.sceneType;

        stats.HP = 100f;
        stats.Speed = 4f;
        stats.Damage = 10f;

        Debug.Log("HP = " + stats.HP);
        Debug.Log("Speed = " + stats.Speed);
    }

    private void Awake()
    {
        bulletPrefab = Resources.Load<GameObject>("Prefabs/object/bullet");
        Debug.Log(bulletPrefab.name);
        fireReady = true;
        delayTime = 0.05f;
        Setup();
    }

    // Start is called before the first frame update
    void Start()
    {
        dir = new Vector3(0, -3.21f,0) - transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y <= -3.2f)
        {
            transform.Translate(dir * 1.5f * Time.deltaTime);
        }
        else
        {
            GameManager.Instance.gameReady = true;
        }

        if (define.sceneType == Define.SceneType.Game && GameManager.Instance.gameReady)
        {
            Move();
        }
    }

    #region PLAYER_FIRE

    void AutoFire()
    {
        if (fireReady == true)
        {
            var bullet = Instantiate(bulletPrefab, FirePos.transform.position, FirePos.transform.rotation);
            StartCoroutine(FireDelay());
        }
    }

    IEnumerator FireDelay()
    {
        fireReady = false;
        yield return new WaitForSeconds(delayTime);
        fireReady = true;
    }
    #endregion

    #region PLAYER_MOVE

    void Move()
    {
        //��ǲ
        h = Input.GetAxisRaw("Horizontal");

        //ȭ�� �� �Ѿ�� �ʵ���
        if ((touch_Left && h == -1) || (touch_Right && h == 1))
        {
            h = 0;
        }

        //���� ��ġ + ���� ��ġ ��� �Ͽ� �̵�
        nowPos = transform.position;
        movePos = new Vector3(h,0,0) * stats.Speed * Time.deltaTime;
        transform.position = nowPos + movePos;
        AutoFire();

    }

    void OnHit(float dmg)
    {
        stats.HP -= dmg;
        if(stats.HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Board")
        {
            switch(collision.gameObject.name) 
            {
                case "wall_Left" :
                    touch_Left = true;
                    break;
                case "wall_Right":
                    touch_Right = true;
                    break;
            }
        }

        if(collision.gameObject.tag == "Meteor")
        {
            meteor meteo = collision.GetComponent<meteor>();
            OnHit(meteo.stats.Damage);
            Debug.Log("HIT");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Board")
        {
            switch (collision.gameObject.name)
            {
                case "wall_Left":
                    touch_Left = false;
                    break;
                case "wall_Right":
                    touch_Right = false;
                    break;
            }
        }
    }
    #endregion
}
