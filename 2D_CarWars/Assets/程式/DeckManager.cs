using UnityEngine;
using System.Collections.Generic; // 系統.集合.一般
using UnityEngine.UI;
using UnityEngine.SceneManagement;//引用場景管理API
using System.Collections;//引用系統集合、管理API(協同程式:非同步處理)

public class DeckManager : MonoBehaviour
{
    [Header("商店內的角色資訊")]
    public List<CardData> deck = new List<CardData>(); //牌組清單

    [Header("開始遊戲按鈕")]
    public Button btnStart;

    [Header("遊戲載入畫面")]
    public GameObject gameView;

    public static GameObject bigPicture;
    public static DeckManager instance;
    public bool exitdeck;
    public Image loading;
    public float imageCD = 0;

    public GameObject Spanner;
    public GameObject sword;
    public GameObject gun;
    public GameObject Bazooka;
    public Text weaponText;
    public GameObject weapinstore;
    /// <summary>
    /// 牌組管理器實體物件
    /// </summary>

    //protected 保護:允許子類別使用成員
    //virtual 虛擬:允許子類別用 override 覆寫
    protected virtual void Awake()
    {
        instance = this;
        btnStart.onClick.AddListener(StartBattle); //添加監聽(開始遊戲)
        LVsave.exitdeck = false;
    }
    public void Start()
    {
        LVsave.index = 1;
    }
    /// <summary>
    /// 開始遊戲
    /// </summary>
    private void StartBattle()
    {
        gameView.SetActive(true); //顯示遊戲載入畫面
        StartCoroutine(Loading());         //啟動協程
    }


    private IEnumerator Loading()
    {
        //SceneManager.LoadScene("關卡1");  //載入場景
        AsyncOperation ao = SceneManager.LoadSceneAsync("關卡" + LVsave.lastLV);

        ao.allowSceneActivation = false;     //關閉自動載入場景


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
    public void chooseSpanner()
    {
        Spanner.SetActive(true);
        sword.SetActive(false);
        gun.SetActive(false);
        Bazooka.SetActive(false);
        weaponText.text = "Spanner";
        LVsave.weapon = "Spanner";
        weapinstore.SetActive(false);
    }
    public void chooseSword()
    {
        Spanner.SetActive(false);
        sword.SetActive(true);
        gun.SetActive(false);
        Bazooka.SetActive(false);
        weaponText.text = "Sword";
        LVsave.weapon = "Sword";
        weapinstore.SetActive(false);
    }
    public void chooseGun()
    {
        Spanner.SetActive(false);
        sword.SetActive(false);
        gun.SetActive(true);
        Bazooka.SetActive(false);
        weaponText.text = "Gun";
        LVsave.weapon = "Gun";
        weapinstore.SetActive(false);
    }
    public void chooseBazooka()
    {
        Spanner.SetActive(false);
        sword.SetActive(false);
        gun.SetActive(false);
        Bazooka.SetActive(true);
        weaponText.text = "Bazooka";
        LVsave.weapon = "Bazooka";
        weapinstore.SetActive(false);
    }
}
