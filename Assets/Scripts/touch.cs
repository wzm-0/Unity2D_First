using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class touch : MonoBehaviour
{
    [SerializeField] public int moveSpeed = 2;
    // public int temp ;
    // int count = 2;
    // float time;
    // Start is called before the first frame update
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        // time+=Time.deltaTime;
        // if(time > 1f)
        // {
        //     temp++;
        //     time = 0f;
        // }
        // if(temp > 10)
        // {
        //     count = (int)Math.Ceiling((double)temp/10);
        //     Debug.Log("倍数：" + count);
        //     moveSpeed = moveSpeed + count;
        // }
        transform.Translate(0,moveSpeed * Time.deltaTime, 0);
        if(transform.position.y > 4.5f){
            Destroy(gameObject);
            transform.parent.GetComponent<FloorManager>().SpawnFloor();
        }
    }
}
