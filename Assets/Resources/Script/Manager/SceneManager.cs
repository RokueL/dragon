using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using SceneType;

public class sceneManager : MonoBehaviour
{
    public Define define = new Define();

    public static sceneManager Instance;

    public GameObject loadCircle;
    public GameObject logoCharacter;

    public Button loginButton;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI progressText;

    float time;
    float loadingTime = 2f;

    public void LoadLobbyScene()
    {        
        progressText.gameObject.SetActive(true);
        infoText.gameObject.SetActive(false);
        loginButton.gameObject.SetActive(false);
        loadCircle.gameObject.SetActive(true);
        StartCoroutine(LoadAsynSceneCoroutine("LobbyScene"));
    }

    public void LoadGameScene()
    {
        StartCoroutine(LoadAsynSceneCoroutine("GameScene"));
    }

    IEnumerator LoadAsynSceneCoroutine(string sceneName)
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        switch (sceneName) {
            case "LobbyScene" :
                while (!operation.isDone)
                {
                    time += Time.deltaTime;

                    progressText.text = "·ÎµùÁß... ( " + (operation.progress * 100) + " / 100 )";

                    if (operation.progress >= 0.9f && time >= loadingTime)
                    {
                        operation.allowSceneActivation = true;
                    }
                    define.sceneType = Define.SceneType.Lobby;
                    yield return null;
                }
                break;
            case "GameScene":

                while (!operation.isDone)
                {
                    time += Time.deltaTime;

                    if (operation.progress >= 0.9f && time >= loadingTime)
                    {
                        operation.allowSceneActivation = true;
                    }
                    define.sceneType = Define.SceneType.Game;
                    yield return null;
                }
                break;
    }
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public void GameButtonSetup(Button button)
    {
        button.onClick.AddListener(LoadGameScene);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "FirstScene")
        {
            loginButton.onClick.AddListener(LoadLobbyScene);
            define.sceneType = Define.SceneType.First;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
