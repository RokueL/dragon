using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_Logo : MonoBehaviour
{

    float y;

    // Start is called before the first frame update
    void Start()
    {
        y = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.UI_Float(this.gameObject,y, 1f);
    }
}

