using UnityEngine;

public class BazookaBullet : MonoBehaviour
{
    public float damage = 2f;
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "敵方" && gameObject.layer == 10)//碰到怪物物件玩家
        {

            other.GetComponent<Enemy>().Damage(damage);//將傷害值傳給敵人
            Destroy(gameObject);
        }
        if (other.gameObject.tag == "我方" && gameObject.layer == 11)//碰到怪物物件玩家
        {

            other.GetComponent<Alliance>().Damage(damage);//將傷害值傳給敵人
            Destroy(gameObject);
        }

    }

}
