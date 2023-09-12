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

    public void UI_Float(GameObject ui_object, float floatingSpeed)
    {
        Vector2 originPos = ui_object.transform.position;
        Vector2 movePos = ui_object.transform.position;
        if (movePos.y - originPos.y >= 1f)
        {
            ui_object.transform.position = movePos + Vector2.down * floatingSpeed * Time.deltaTime;
        }
        else if (movePos.y - originPos.y <= -1f)
        {
            ui_object.transform.position = movePos + Vector2.up * floatingSpeed * Time.deltaTime;
        }
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
