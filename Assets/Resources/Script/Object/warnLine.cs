using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warnLine : MonoBehaviour
{
    public enum state
    {
        rain,
        normal,
        breath
    }
    public state states;

    public GameObject Player;
    GameObject meteorPrefab, breathPrefab;
    Vector2 playerPos;
    Vector2 nowPos, movePos;

    float moveSpeed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        meteorPrefab = Resources.Load<GameObject>("Prefabs/object/meteor");
        breathPrefab = Resources.Load<GameObject>("Prefabs/object/breath");

        StartCoroutine(shot());
    }

    IEnumerator shot()
    {
        yield return new WaitForSeconds(2f);
        if (states == state.normal)
        {
            var meteor = Instantiate(meteorPrefab,
                 (transform.position + new Vector3(0, 5.5f, 0)), transform.rotation);
            Destroy(this.gameObject);
        }
        else if (states == state.breath)
        {
            var breath = Instantiate(breathPrefab,
                 (transform.position + new Vector3(0.4f, -2.5f, 0)), Quaternion.Euler(0,0,-90f));
            Destroy(this.gameObject);
        }
        else if (states == state.rain)
        {
            var breath = Instantiate(meteorPrefab,
                 (transform.position + new Vector3(0, 5.5f, 0)), transform.rotation);
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (states == state.normal)
        {
            nowPos = transform.position;
            playerPos = new Vector2(Player.transform.position.x, 0);
            float dir = playerPos.x - nowPos.x;
            transform.Translate(new Vector2(dir, 0) * moveSpeed * Time.deltaTime);
        }
        else
        {

        }
    }
}
