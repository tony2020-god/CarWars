using UnityEngine;
using System.Collections.Generic;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)

public class Knife : MonoBehaviour
{
    public float damage = 0.02f;
    public string type;
    public void Start()
    {
        
    }
    public void Update()
    {
        Track();
    }
    private void Track()
    {
        Vector3 posA = transform.position;    //取得攝像機座標
        Vector3 posB = gameObject.transform.parent.position;       //取得目標座標
        if (gameObject.transform.parent.tag == "敵方")
        {
            posB.x = posB.x -1.45f;       //取得目標座標
            posA.y = posB.y;
        }
        else if (gameObject.transform.parent.tag == "我方")
        {
            posB.x = posB.x + 1.504f;       //取得目標座標
            posB.y = posB.y - 0.331f;
        }
            //一禎的時間 Time.deltaTime
        posA = Vector3.Lerp(posA, posB, 100f * Time.deltaTime);//插值
        transform.position = posA;            //攝影機座標 = A點
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "我方")//碰到怪物物件玩家
        {
            if(gameObject.transform.parent.tag == "敵方") other.GetComponent<Alliance>().Damage(damage);//將傷害值傳給玩家
            print("傷害"+damage);
        }
        else if (other.tag == "敵方")//碰到怪物物件玩家 
        {
            if (gameObject.transform.parent.tag == "我方") other.GetComponent<Enemy>().Damage(damage);//將傷害值傳給玩家
        }    
    }
}
