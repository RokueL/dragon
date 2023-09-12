using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class sceneManager : MonoBehaviour
{
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

    IEnumerator LoadAsynSceneCoroutine(string sceneName)
    {
        yield return null;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        while(!operation.isDone) 
        {
            time += Time.deltaTime;

            progressText.text = "·ÎµùÁß... ( " + (operation.progress * 100) + " / 100 )";

            if(operation.progress >= 0.9f && time >= 3f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        loginButton.onClick.AddListener(LoadLobbyScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
