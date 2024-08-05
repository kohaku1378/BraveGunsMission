using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingrollController : MonoBehaviour
{
    Vector3 Staffrollposition;     //   指定したUIの座標を格納
    public RectTransform rectTransform;     //  スクロールするUIを格納
    public float Endpos;    //  スクロールが終了するY座標
    public float rollspeed;     //  スクロールする速度


    // Start is called before the first frame update
    void Start()
    {
        //  最初の座標を格納
        Staffrollposition = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //  スクロールするUIが指定したY座標より小さいとき
        if (rectTransform.anchoredPosition.y < Endpos) {
            //  動かす座標を変更
            Staffrollposition.y += rollspeed;
            //  座標を移動
            rectTransform.anchoredPosition = Staffrollposition;
        }
        else
        {
            //  シーンを遷移する
            SceneManager.LoadScene("TitleScene");
        }

    }
}
