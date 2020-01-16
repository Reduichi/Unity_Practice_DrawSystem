using UnityEngine;
using UnityEngine.UI;       // 引用介面 API  
using System.Collections;

public class Randomcard : MonoBehaviour
{
    [Header("圖片 : 卡片")]
    public Sprite[] picturecard;     // 圖片陣列 
    [Header("捲動速度"), Range(0.00001f, 3f)]
    public float speed = 0.01f;
    [Header("捲動次數"), Range(1, 10)]
    public int count = 5;
    [Header("音效")]
    public AudioClip soundScroll;
    public AudioClip soundGetSkill;

    private Image img;               // 圖片元件
    private Button btn;              // 按鈕元件
    private AudioSource aud;         // 音源元件
    private int index;               // 隨機道具編號

    private void Start()
    {
        img = GameObject.Find("道具圖片").GetComponent<Image>();
        btn = GameObject.Find("抽牌按鈕").GetComponent<Button>();
        aud = GetComponent<AudioSource>();
        btn.onClick.AddListener(ChooseCard);
    }

    private void ChooseCard()
    {
        StartCoroutine(ScrollEffect());  // 啟動協程
    }

    // 定義協程方法 捲動效果
    /// <summary>
    /// 捲動效果
    /// </summary>
    /// <returns></returns>
    private IEnumerator ScrollEffect()
    {
        btn.interactable = false;  // 按鈕無法點選

        // 迴圈
        for (int j = 0; j < count; j++)
        {
            for (int i = 0; i < picturecard.Length; i++)  // 圖片元件.圖片 = 圖片陣列[編號]
            {
                img.sprite = picturecard[i];
                aud.PlayOneShot(soundScroll, 0.2f);
                yield return new WaitForSeconds(speed);
            }
        }

        int index = Random.Range(0, picturecard.Length);     // 隨機挑選 技能圖片陣列
        img.sprite = picturecard[index];
        aud.PlayOneShot(soundGetSkill, 0.8f);

        btn.interactable = true;    // 按鈕可以點選
    }
}

