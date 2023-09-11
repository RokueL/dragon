using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    //public static PoolManager instance;

    //GameObject bulletPrefab;

    //private IObjectPool<Bullet> bulletPool;

    //private void Awake()
    //{
    //    if (instance == null)
    //        instance = this;
    //    else
    //        Destroy(this.gameObject);

    //    bulletPrefab = Resources.Load<GameObject>("Prefabs/bullet");

    //    bulletPool = new ObjectPool<Bullet>(
    //        CreateBullet,
    //        OnGet,
    //        OnRelease,
    //        OnDestroyy,
    //        maxSize: 10
    //        );
    //}

    //private Bullet CreateBullet()
    //{
    //    Bullet bullet = Instantiate( bulletPrefab ).GetComponent<Bullet>();
    //    bullet.SetManagedPool(bulletPool);
    //    return bullet;
    //}

    //private void OnGet(Bullet bullet)
    //{
    //    bullet.gameObject.SetActive(true);
    //}

    //private void OnRelease(Bullet bullet)
    //{
    //    bullet.gameObject.SetActive(true);
    //}

    //private void OnDestroyy(Bullet bullet)
    //{
    //    Destroy(bullet.gameObject);
    //}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
