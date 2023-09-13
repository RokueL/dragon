using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class sceneManager : MonoBehaviour
{
    public static sceneManager Instance;

    public GameObject loadCircle;
    public GameObject logoCharacter;

    public Button loginButton;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI progressText;

    float time;

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

                    if (operation.progress >= 0.9f && time >= 3f)
                    {
                        operation.allowSceneActivation = true;
                    }

                    yield return null;
                }
                break;
            case "GameScene":

                while (!operation.isDone)
                {
                    time += Time.deltaTime;

                    if (operation.progress >= 0.9f && time >= 3f)
                    {
                        operation.allowSceneActivation = true;
                    }

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
        if(SceneManager.GetActiveScene().name == "FirstScene")
            BloginButton.onClick.AddListener(LoadLobbyScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
