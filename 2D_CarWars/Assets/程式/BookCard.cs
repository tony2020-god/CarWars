using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BookCard : MonoBehaviour
{
    /// <summary>
    /// 卡片圖鑑的編號
    /// </summary>
    public int index;
    public GameObject picture;
    public LVsave LV;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(ChooseBookCard); // 取得按鈕.點擊.添加監聽器(方法)
        picture = GameObject.Find("選擇的圖示");      
    }

    /// <summary>
    /// 選擇圖鑑內的卡片
    /// </summary>
    private void ChooseBookCard()
    {
        print("選取的圖鑑編號為 :" + index);
        LVsave.index = index;    
        picture.GetComponent<Image>().sprite = gameObject.GetComponent<Image>().sprite; // 尋找圖片子物件.圖片 = 來源.載入<圖片>(檔案名稱)
    }
    public void Update()
    {

        

    }
    
}
