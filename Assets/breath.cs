using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Datas;

public class breath : MonoBehaviour
{
    public Stats stats = new Stats();
    // Start is called before the first frame update
    void Start()
    {
        stats.Damage = 20f;
        Destroy(this.gameObject, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
