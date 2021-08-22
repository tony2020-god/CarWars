using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class Alliance : MonoBehaviour
{
    public float hp;
    public float damage;
    public float weight;
    public float speed;
    public bool Stop;
    public bool crush;
    public Transform PathB;
    private GameManager gm;
    public float hpMax;
    public Rigidbody2D rig;
    public Image imgHp;

    public GameObject Spanner;
    public GameObject sword;
    public GameObject gun;
    public GameObject Bazooka;

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
        imgHp = GameObject.Find("玩家血條").transform.Find("血條").GetComponent<Image>();
        PathB = GameObject.Find("我方飛出區").transform;
        hpMax = hp;
        rig = gameObject.GetComponent<Rigidbody2D>();
        aud = gameObject.GetComponent<AudioSource>();
        rig.mass = weight;
        damage = speed * weight / 100;
        WhichWeapon();
    }

    /// <summary>
    /// 受傷
    /// </summary>
    /// <param name="damage">接收收到的傷害值</param>
    public void Damage(float damage)
    {
        if(gm.StartGame == true)
        {
            hp -= damage;
            imgHp.fillAmount = hp / hpMax;
            print(hp);
            if (dead == false)
            {
                if (hp <= 0)
                {
                    aud.PlayOneShot(deadsound, 1);
                    dead = true;
                    StartCoroutine(Lose());
                }
            }
        }  
    }
    public void WhichWeapon()
    {
        if (LVsave.weapon == "Spanner")
        {
            Vector3 pos = new Vector2(-6.8f, -2.3f); //座標
            Quaternion qua = Quaternion.Euler(0, 0, 0); //角度
            GameObject temp = Instantiate(Spanner, pos, qua); //生成
            temp.transform.parent = gameObject.transform;
        }
        else if (LVsave.weapon == "Sword")
        {
            Vector3 pos = new Vector2(0f, 0); //座標
            Quaternion qua = Quaternion.Euler(0, 0, -90); //角度
            GameObject temp = Instantiate(sword, pos, qua); //生成
            temp.transform.parent = gameObject.transform;
        }
        else if (LVsave.weapon == "Gun")
        {
            Vector3 pos = new Vector2(-6.857f, -1.962f); //座標
            Quaternion qua = Quaternion.Euler(0, 0, 0); //角度
            GameObject temp = Instantiate(gun, pos, qua); //生成
            temp.transform.parent = gameObject.transform;
        }
        else if (LVsave.weapon == "Bazooka")
        {
            Vector3 pos = new Vector2(0f, 0); //座標
            Quaternion qua = Quaternion.Euler(0, 0, 0); //角度
            GameObject temp = Instantiate(Bazooka, pos, qua); //生成
            temp.transform.parent = gameObject.transform;
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
                transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
            }
        }
    }
    public IEnumerator Lose()
    {
        gm.StartGame = false;
        yield return new WaitForSeconds(1f);
        gm.lose.SetActive(true);
    }
    private void Track()
    {
        Stop = true;
        transform.Rotate(0, 0,720 * Time.deltaTime);
        Vector3 posA = transform.position;    //取得攝像機座標
        Vector3 posB = PathB.position;       //取得目標座標
        //一禎的時間 Time.deltaTime
        posA = Vector3.Lerp(posA, posB, 0.03f * Time.deltaTime);//插值
        transform.position = posA;            //攝影機座標 = A點
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "敵方")//碰到怪物物件玩家
        {
            if(crush == false)
            {
                crush = true;
                other.gameObject.GetComponent<Enemy>().Damage(damage);//將傷害值傳給敵人
            }
        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "敵方")//碰到怪物物件玩家
        {
            if (crush == true)
            {
                crush = false;
            }
        }
    }
}