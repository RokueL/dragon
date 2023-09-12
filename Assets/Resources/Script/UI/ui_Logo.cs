using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_Logo : MonoBehaviour
{

    Vector2 nowPos, updown;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UIManager.Instance.UI_Float(this.gameObject, 1f);
    }
}

