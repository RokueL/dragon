using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warnMark : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    float fadeSpeed = 1f;
    bool check;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(warning_INOUT());
    }

    IEnumerator warning_INOUT()
    {
        while(!check)
        {
            fadeSpeed -= 0.2f;
            yield return new WaitForSeconds(0.01f);
            spriteRenderer.color = new Color(255, 255, 255, fadeSpeed);
            if(fadeSpeed <= 0f )
            {
                check = true;
            }
        }
        while (check)
        {
            fadeSpeed += 0.1f;
            yield return new WaitForSeconds(0.5f);

            spriteRenderer.color = new Color(255, 255, 255, fadeSpeed);
            if (fadeSpeed >= 1f)
            {
                check = false;
            }
        }
    }




    // Update is called once per frame
    void Update()
    {

    }
}
