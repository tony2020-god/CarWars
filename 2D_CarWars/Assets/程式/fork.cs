using UnityEngine;
using System.Collections.Generic;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)

public class fork : MonoBehaviour
{
    public float damage = 0.2f;
    public string type;
    public bool crash;
    public Vector3 gamepos;
    public Vector3 parentpos;
    public void Start()
    {
        gamepos = gameObject.transform.position;
        parentpos = gameObject.transform.parent.position;       //取得目標座標

    }
    public void Update()
    {
        Track();
    }
    private void Track()
    {
        transform.Rotate(0, 0, -50 * Time.deltaTime);
        Vector3 posA = transform.position;    //取得攝像機座標
        Vector3 posB = gameObject.transform.parent.position;       //取得目標座標
        Vector3 posC = parentpos - gamepos;
        if (gameObject.transform.parent.tag == "敵方")
        {
            posB.x = posB.x - posC.x;
            posA.y = posB.y;
        }
        else if (gameObject.transform.parent.tag == "我方")
        {
            posB.x = posB.x - 1.3f;
            posA.y = posB.y ;
        }
            //一禎的時間 Time.deltaTime
        posA = Vector3.Lerp(posA, posB, 100f * Time.deltaTime);//插值
        transform.position = posA;            //攝影機座標 = A點
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(crash == false)
        {
            if (other.tag == "我方")//碰到怪物物件玩家
            {
                if (gameObject.transform.parent.tag == "敵方") other.GetComponent<Alliance>().Damage(damage);//將傷害值傳給玩家
            }
            else if (other.tag == "敵方")//碰到怪物物件玩家 
            {
                if (gameObject.transform.parent.tag == "我方") other.GetComponent<Enemy>().Damage(damage);//將傷害值傳給玩家
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if(crash == true)
        if (other.tag == "我方")//碰到怪物物件玩家
        {
                if (gameObject.transform.parent.tag == "敵方") crash = false;
        }
        else if (other.tag == "敵方")//碰到怪物物件玩家 
        {
            if (gameObject.transform.parent.tag == "我方") crash = false;
            }
    }
}
