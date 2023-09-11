using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{


//{
//    public static PoolManager Instance;

//    GameObject bulletPrefab;

//    public IObjectPool<GameObject> bulletpool { get; private set; }


//    int defCapacity = 10;
//    int maxSize = 15;

//    void Awake()
//    {
//        bulletPrefab = Resources.Load<GameObject>("Prefabs/bullet");
//        if (Instance == null)
//        {
//            Instance = this;
//        }
//        else
//        {
//            Destroy(this.gameObject);
//        }

//        bulletpool = new ObjectPool<GameObject>(
//            OnCreate,
//            OnGet,
//            OnRelease,
//            OnDestroyer,
//            true,
//            defCapacity,
//            maxSize
//            );


//        // 미리 오브젝트 생성 해놓기   미리 만든 크레이트 후 릴리스로 액티브 비활성으로 한다
//        for (int i = 0; i < defCapacity; i++)
//        {
//            Bullet bullet = OnCreate().GetComponent<Bullet>();
//            bullet.bulletPool.Release(bullet.gameObject);
//        }
//    }



//    GameObject OnCreate()
//    {
//        GameObject poolGo = Instantiate(bulletPrefab);
//        poolGo.GetComponent<Bullet>().bulletPool = this.bulletpool;
//        return poolGo;
//    }


//    void OnGet(GameObject poolGo)
//    {
//        poolGo.SetActive(true);
//    }

//    void OnRelease(GameObject poolGo) 
//    {
//        poolGo.SetActive(false);
//    }

//    private void OnDestroyer(GameObject poolGo)
//    {
//        Invoke("OnDestroy", 1f);
//    }

//    private void OnDestroy()
//    {
//        Destroy(this.gameObject);
//    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
