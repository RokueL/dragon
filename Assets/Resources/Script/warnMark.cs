using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warnMark : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    IEnumerator warning_IN()
    {
        float fadeInTime = 0f;
        while (fadeInTime < 1f)
        {
            fadeInTime += 0.1f * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(255, 255, 255, fadeInTime);
        }
        StartCoroutine(warning_OUT());
    }

    IEnumerator warning_OUT()
    {
        float fadeInTime = 1f;
        while (fadeInTime < 0f)
        {
            fadeInTime -= 0.1f * Time.deltaTime;
            yield return new WaitForSeconds(0.1f);
            spriteRenderer.color = new Color(255, 255, 255, fadeInTime);
        }
    }



    // Update is called once per frame
    void Update()
    {
        StartCoroutine(warning_IN());

    }
}
