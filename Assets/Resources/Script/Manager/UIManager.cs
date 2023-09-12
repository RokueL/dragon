using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void UI_Float(GameObject ui_object, float origin,float floatingSpeed)
    {
        float moveSpeed = 1;
        float max = origin + 0.2f;
        float min = origin - 0.2f;

        //if (ui_object.transform.position.y >= upMax)
        //{
        //    Debug.Log("Down");
        //    direc = -1;
        //}
        //else if(ui_object.transform.position.y <= downMax)
        //{
        //    Debug.Log("Up");
        //    direc = 1;
        //}

        //ui_object.transform.position += new Vector3(0, 1 * direc, 0) * floatingSpeed * Time.deltaTime;


        Vector3 tr = ui_object.gameObject.transform.position;

        if (tr.y <= min)
        {
            moveSpeed = 0.5f;
            //Debug.Log(tr.y);
        }
        else if (tr.y >= max)
        {
            moveSpeed = -0.5f;
            //Debug.Log (tr.y);
        }

        tr.y += moveSpeed * Time.deltaTime;
        ui_object.transform.position = tr;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
