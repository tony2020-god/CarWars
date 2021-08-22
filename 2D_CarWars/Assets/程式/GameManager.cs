using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)
public class GameManager : MonoBehaviour
{
    public static GameManager instance; //對戰管理實體物件
    public bool gamestart;
    public SpawnAlliance data;
    public GameObject pass;
    public GameObject lose;
    public Image loading;
    public int index;
    [Header("遊戲載入畫面")]
    public GameObject gameView;

    public float imageCD = 0.9f;

    public float imageCD2 = 0f;
    public Text StartTimerText;
    public Text GameTimerText;
    public float StartTimer;
    public bool StartGame;
    public float gameTime;
    public AudioClip trunsound;
    public AudioClip countsound;
    public AudioClip gosound;
    public AudioClip timesUpsound;
    public AudioSource aud;
    public AudioSource aud2;
    public void Awake()
    {
        instance = this;
    }
    public void Start()
    {
        aud = gameObject.GetComponent<AudioSource>();
        index = LVsave.index;
        StartCoroutine(Startloadingimage());
        CarSpawn(index, GetCard.instance);
        StartTimer = 3f;
        gameTime = 30;
        
        StartTimerText.text = StartTimer.ToString();
    }
    public IEnumerator Startloadingimage()
    {
        while (imageCD > 0)        //迴圈 while(布林值) "當布林值為 true 時執行敘述"
        {
            imageCD = imageCD - 0.01f;
            loading.fillAmount = imageCD / 0.9f;                            //更新載入吧條
                                                                            //等待
            if (imageCD <= 0)    //判斷式 if(布林值) "當布林值為true時執行一次"  
            {
                gameView.SetActive(false); //關閉遊戲載入畫面
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    public IEnumerator StartCountDown()
    {
        gamestart = true;
        yield return new WaitForSeconds(1f);
        gamestart = false;
        if(gameTime == 30) aud.PlayOneShot(trunsound, 1);
        while (StartTimer > 1 )
        {
            
            aud2.PlayOneShot(countsound, 1);
            StartTimer -= 1;
            int timer = (int)StartTimer;         
            StartTimerText.text =timer.ToString();
            gamestart = true;
            yield return new WaitForSeconds(1f);
            gamestart = false;
        }
        if (gameTime ==30)
        {
            StartTimerText.text = "GO!";
            aud2.PlayOneShot(gosound, 1);
            StartGame = true;
            yield return new WaitForSeconds(1f);
            StartTimerText.text = "";
        }
    }
    public IEnumerator GameTimeCountDown()
    {
        aud.PlayOneShot(trunsound, 1);
        while (gameTime > 0 && StartGame == true)
        {   
            gameTime -= 1;
            int timer = (int)gameTime;
            GameTimerText.text = "Time: " + timer.ToString();
            gamestart = true;
            yield return new WaitForSeconds(1f);
            gamestart = false;
        }
        if (StartGame == true)
        {
            StartTimerText.text = "Times Up!";
            StartGame = false;
            aud.PlayOneShot(timesUpsound, 1);
            yield return new WaitForSeconds(1f);
            lose.SetActive(true);
        } 
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BackToMenu()
    {
        LVsave.weapon = "";
        SceneManager.LoadScene("選車畫面");
    }
    public void NextLv()
    {
        LVsave.lastLV = LVsave.lastLV + 1;
        StartCoroutine(Endloadingimage());
    }
    public void BackToChooseRoleWin()
    {
        LVsave.weapon = "";
        LVsave.lastLV = LVsave.lastLV + 1;
        SceneManager.LoadScene("選車畫面");      
    }
    public IEnumerator Endloadingimage()
    {
        AsyncOperation ao = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        ao.allowSceneActivation = false;
        gameView.SetActive(true); //關閉遊戲載入畫面
        while (imageCD < 1)        //迴圈 while(布林值) "當布林值為 true 時執行敘述"
        {
            imageCD = imageCD + 0.01f;
            loading.fillAmount = imageCD / 0.9f;                            //更新載入吧條
                                                                            //等待
            if (imageCD >= 0.9f)    //判斷式 if(布林值) "當布林值為true時執行一次"  
            {
                gameView.SetActive(false); //關閉遊戲載入畫面
                ao.allowSceneActivation = true;    //允許自動載入場景
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void CarSpawn(int index, GetCard cards)
    {
        CardData card = cards.cards[index - 1]; //卡片資料(Element從0開始，編號從1開始，故-1)
        if (index >4)
        {
            Vector3 pos = new Vector2(-6.57f, -2.37f); //座標
            Quaternion qua = Quaternion.Euler(0, 0, 0); //角度
            GameObject temp = Instantiate(data.Spawn[index - 1].Alliance, pos, qua); //生成
            temp.gameObject.GetComponent<Alliance>().hp = card.hp; //添加元件<盟友>.血量 = 卡片.血量      
            temp.gameObject.GetComponent<Alliance>().speed = card.speed; //添加元件<盟友移動> 速度 = 卡牌.速度
            temp.gameObject.GetComponent<Alliance>().weight = card.weight; //添加元件<盟友移動>.攻擊速度 = 卡片.攻擊速度
        }
        else if (index <= 4)
        {
            Vector3 pos = new Vector2(-6.84f, -2.76f); //座標
            Quaternion qua = Quaternion.Euler(0, 0, 0); //角度
            GameObject temp = Instantiate(data.Spawn[index - 1].Alliance, pos, qua); //生成
            temp.gameObject.GetComponent<Alliance>().hp = card.hp; //添加元件<盟友>.血量 = 卡片.血量      
            temp.gameObject.GetComponent<Alliance>().speed = card.speed; //添加元件<盟友移動> 速度 = 卡牌.速度
            temp.gameObject.GetComponent<Alliance>().weight = card.weight; //添加元件<盟友移動>.攻擊速度 = 卡片.攻擊速度
        }
    }
    public void Update()
    {
        if(StartGame == true)
        {
            if (gamestart == false) StartCoroutine(GameTimeCountDown());
        }
        else
        {
            if (gamestart == false) StartCoroutine(StartCountDown());
        }
    }
}
