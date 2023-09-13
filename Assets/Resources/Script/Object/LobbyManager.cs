using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public Button startButton;
    // Start is called before the first frame update
    void Awake()
    {
        startButton = GameObject.Find("B_Start").GetComponent<Button>();
        startButton.onClick.AddListener(sceneManager.Instance.LoadGameScene);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
