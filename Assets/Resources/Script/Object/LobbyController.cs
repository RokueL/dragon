using SceneType;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    Define define;
    public Button startButton;
    // Start is called before the first frame update
    void Awake()
    {
        startButton = GameObject.Find("B_Start").GetComponent<Button>();
        startButton.onClick.AddListener(sceneManager.Instance.LoadGameScene);
        sceneManager.Instance.define.sceneType = Define.SceneType.Lobby;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
