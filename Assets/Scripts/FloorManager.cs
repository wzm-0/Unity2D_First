using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
    [SerializeField] GameObject[] floorPrefabs;
    int m = 0;
    int lastr = 0;
    public void SpawnFloor(){
        // 随机生成一个数字
        int r =Random.Range(0,floorPrefabs.Length);
        // 判断出现几次
        if(r == lastr)
            m++;
        // 如果出现次数过多就修改生成的类型
        if(m > 2)
        {   
            if(r==0)
                r = 1;
            else
                r = 0;
            m = 0;
        }
        lastr = r;
        GameObject floor = Instantiate(floorPrefabs[r], transform);
        floor.transform.position = new Vector3(Random.Range(-3f,3f),-5f,0f);
    }
}
