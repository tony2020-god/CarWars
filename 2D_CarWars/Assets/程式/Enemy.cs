using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    public float hp;
    public float damage;
    public float weight;
    public float speed;
    public bool Stop;
    public Transform PathB;
    [Header("敵人資料")]
    public EnemyData data;
    private GameManager gm;
    public bool crush;
    public float hpMax;
    public Image imgHp;
    public Rigidbody2D rig;
    public AudioClip crashsound;
    public AudioClip deadsound;
    public AudioSource aud;
    /// <summary>
    /// 動畫控制器
    /// </summary>
    private Animator ani;

    public bool dead = false;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        hp = data.hp;
        weight = data.weight;
        speed = data.speed;
        hpMax = hp;
        aud = gameObject.GetComponent<AudioSource>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        rig.mass = weight;
        damage = speed * weight / 100;
    }
    public IEnumerator Win()
    {
        gm.StartGame = false;
        yield return new WaitForSeconds(1f);
        gm.pass.SetActive(true);      
    }
    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收收到的傷害值</param>
    public void Damage(float damage)
    {
        if(gm.StartGame==true)
        {
            hp -= damage;
            imgHp.fillAmount = hp / hpMax;
            if (dead == false)
            {
                if (hp <= 0)
                {
                    aud.PlayOneShot(deadsound, 1);
                    dead = true;
                    if (LVsave.lastLV == 3)
                    {
                        gm.NextLv();
                    }
                    else
                    {
                        StartCoroutine(Win());
                    }                
                }
            }
        }
    }

    public void Update()
    {
        if(gm.StartGame==true)
        {
            MoveMethod();
            
        }
        if (dead == true) Track();
    }
    /// <summary>
    /// 移動方法
    /// </summary>
    private void MoveMethod()
    {
        if (dead == false)
        {
            if (Stop == false)
            {
                transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);

            }
        }
    }
    private void Track()
    {
        transform.Rotate(0, 0, -720 * Time.deltaTime);
        Vector3 posA = transform.position;    //取得攝像機座標
        Vector3 posB = PathB.position;       //取得目標座標
        //一禎的時間 Time.deltaTime
        posA = Vector3.Lerp(posA, posB, 0.03f * Time.deltaTime);//插值
        transform.position = posA;            //攝影機座標 = A點
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "我方")//碰到怪物物件玩家
        {
            if (crush == false)
            {
                crush = true;
                other.gameObject.GetComponent<Alliance>().Damage(damage);//將傷害值傳給敵人
                aud.PlayOneShot(crashsound, 1);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "我方")//碰到怪物物件玩家
        {
            if (crush == true)
            {
                crush = false;
            }
        }
    }
}