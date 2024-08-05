using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManagementScript : MonoBehaviour
{
    [SerializeField] float colorset;    //  パネルの
    public Image fadePanel;     //  フェード用のパネルを格納
    public float fadeDuration = 1.0f;   //  フェードにかかる時間
    public bool start;  //  フェードの開始を制御

    private void Start()
    {
        // パネルを無効化
        fadePanel.enabled = false;                 
        if(start == true)
        {
            //  フェードアウトの処理を呼び出す
            StartCoroutine(FadeManagement());
        }
    }

    public void CallCoroutine()
    {
        //  フェードアウトの処理を呼び出す
        StartCoroutine(FadeManagement());
    }

    public IEnumerator FadeManagement()
    {
        //  パネルを有効化
        fadePanel.enabled = true;                 
        //  経過時間を初期化
        float elapsedTime = 0.0f;                 
        //  フェードパネルの開始色を取得
        Color startColor = fadePanel.color;       
        //  フェードパネルの最終色を設定
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, colorset); 

        //  フェードアウトアニメーションを実行
        while (elapsedTime < fadeDuration)
        {
            //  経過時間を増やす
            elapsedTime += Time.deltaTime;                        
            //  フェードの進行度を計算
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);  
            //   パネルの色を変更してフェードアウト
            fadePanel.color = Color.Lerp(startColor, endColor, t);
            //   1フレーム待機 
            yield return null;                                     
        }
        // フェードが完了したら最終色に設定
        fadePanel.color = endColor;  
        if(start == true)
        {
            //  パネルを無効化
            fadePanel.enabled = false;
        }
    }
}
