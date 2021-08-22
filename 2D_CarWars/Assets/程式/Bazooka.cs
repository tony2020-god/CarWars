using UnityEngine;
using System.Collections.Generic;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)

public class Bazooka : MonoBehaviour
{
    public GameObject bullet;
    public Transform point;
    public string type;
    public bool crash;
    public bool fire;
    private GameManager gm;
    public Vector3 gamepos;
    public Vector3 parentpos;
    public void Start()
    {
        gm = FindObjectOfType<GameManager>();
        gamepos = gameObject.transform.position;
        parentpos = gameObject.transform.parent.position;       //取得目標座標
    }
    public void Update()
    {
        Track();
        if (fire == false)
        {
            StartCoroutine(Fire());
        }
    }
    private void Track()
    {
        Vector3 posA = transform.position;    //取得攝像機座標
        Vector3 posB = gameObject.transform.parent.position;       //取得目標座標
        Vector3 posC = parentpos - gamepos;
        if (gameObject.transform.parent.tag == "我方")
        {
            posB.x = posB.x - 1.18f;       //取得目標座標
            posB.y = posB.y + 0.28f;
        }
        else if (gameObject.transform.parent.tag == "敵方")
        {
            posB.x = posB.x - posC.x;
            posB.y = posB.y - posC.y;
        }
        //一禎的時間 Time.deltaTime
        posA = Vector3.Lerp(posA, posB, 100f * Time.deltaTime);//插值
        transform.position = posA;            //攝影機座標 = A點
    }
    public IEnumerator Fire()
    {
        if (gm.StartGame == true)
        {
            fire = true;
            GameObject temp = Instantiate(bullet, point.position, point.rotation);
            if (gameObject.transform.parent.tag == "敵方")
            {
                temp.GetComponent<Rigidbody2D>().AddForce(transform.right * 5000);
                temp.layer = 11;
            }
            else if (gameObject.transform.parent.tag == "我方")
            {
                temp.GetComponent<Rigidbody2D>().AddForce(transform.right * -5000);
                temp.layer = 10;
            }
            yield return new WaitForSeconds(0.5f);
            fire = false;
        }
           
    }
}
