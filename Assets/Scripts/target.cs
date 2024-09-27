using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.Burst.Intrinsics;
using System;
public class target : MonoBehaviour
{

    [SerializeField] float speed;

    GameObject currentFloor; 

    [SerializeField] int Hp ;

    [SerializeField] GameObject Hpbar;

    [SerializeField] Text Num;
    public GameObject bulletPrefab; // 子弹预制体
    public Transform firePoint; // 发射点
    public float bulletForce = 20f; // 子弹速度
    private Vector2 movementDirection; // 设定一个二维向量
    public int score;
    float scoretime;

    float xInput;
    /// <summary>
    /// 初始化
    /// </summary>
    private void Start()
    {
        // InitializePool();
        Hp=10;
        score = 0;
        scoretime = 0;
    }
    /// <summary>
    /// 角色碰撞产生的效果
    /// </summary>
    /// <param name="collision"> 碰撞到的对象 </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ceiling")
        {
            //撞到天花板就把当前脚下的对象刚性取消
            // Debug.Log("撞到天花板了！");
            currentFloor.GetComponent<BoxCollider2D>().enabled = false;
            ModifyHP(-2); 
        }
        else if(collision.gameObject.tag == "Normal")
        {
            // Debug.Log("撞到第一种阶梯！");
            // 碰撞后对象的法向量为向上的单位向量，则切换碰撞对象
            if(collision.contacts[0].normal == new Vector2(0f,1f))
            {
                currentFloor = collision.gameObject;
                ModifyHP(1); 
            }
        }
        else if(collision.gameObject.tag == "Nails")
        {
            // Debug.Log("撞到第二种阶梯！");
            // 碰撞后对象的法向量为向上的单位向量，则切换碰撞对象
            if(collision.contacts[0].normal == new Vector2(0f,1f))
            {
                currentFloor = collision.gameObject;
                ModifyHP(-1); 
            }
        }
    }
    // Update is called once per frame
    /// <summary>
    /// 控制角色移动
    /// </summary>
    void Update()
    {
        if(Hp == 0 || transform.position.y < -4.5f){
            Invoke("Restart",2f);
        }
        // 范围是-1~1
        xInput = Input.GetAxis("Horizontal");
        
        movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        // 根据输入方向设置速度
        transform.Translate(xInput * speed, 0, 0) ;
        if(xInput < 0 )
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        else if(xInput > 0 )
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        
        UpdateScore();
        if (Input.GetKeyDown(KeyCode.Space)) // 按空格键发射子弹
        {
            Shoot(gameObject.GetComponent<SpriteRenderer>().flipX);
        }
    }
    /// <summary>
    /// 控制球体（子弹）属性
    /// </summary>
    void Shoot(bool vector2)
    {
        // 在指定位置实例化子弹
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // 根据鼠标所在位置的方向
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Vector2 direction = (mousePos - firePoint.position).normalized;

        // 根据对象移动的方向
        Vector2 direction = new Vector2();
        if(!vector2)
            direction = new Vector2(-1,0);
        else
            direction = new Vector2(1,0);
        
        // 获取刚体组件并应用力
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // rb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
        }

        // 设置子弹销毁时间，比如2秒后销毁
        Destroy(bullet, 2f);
    }
    /// <summary>
    /// 扣血
    /// </summary>
    /// <param name="num"></param>
    void ModifyHP(int num)
    {
        Hp += num;
        if(Hp>10){
            Hp = 10;
        }
        else if(Hp<0){
            Hp = 0;
        }
        UpdateHpbar();
    }
    /// <summary>
    /// 更新血条
    /// </summary>
    void UpdateHpbar()
    {
        for(int i = 0;i < Hpbar.transform.childCount;i++){
            if(Hp>i)
            {
                Hpbar.transform.GetChild(i).gameObject.SetActive(true);
            }
            else
            {
                Hpbar.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    public touch touch1;
    public int temp = 0;
    /// <summary>
    /// 更新分数
    /// </summary>
    void UpdateScore()
    {
        scoretime+=Time.deltaTime;
        if(scoretime > 1f)
        {
            score++;
            scoretime = 0f;
            Num.text = score.ToString();
        }
    }

    public void Restart()
    {
        StartCoroutine(QuitGameCoroutine());
    }

    IEnumerator QuitGameCoroutine()
    {
        // 释放资源
        yield return Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("SampleScene");
    }
}
