using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    [SerializeField] private GameObject warp;   //  次のステージに進めるワープを格納
    [SerializeField] private GameObject cleartext;    //  クリアのテキストを格納
    public float enemycount;    //  ステージにいる敵の数
    public AudioClip clearsound;     //   クリア時の効果音
    public AudioSource audioSource;      //  オーディオソースを格納
    public AudioSource audioSource_Battle;     //  通常時のBGMのオーディオソースを格納
    public AudioSource audioSource_Clear;     //  クリア時のBGMのオーディオソースを格納
    private bool clearjudge = true;    //  クリア判定
    
    // Start is called before the first frame update
    void Start()
    {
        //  通常時のBGMを流す
        audioSource_Battle.Play();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(enemycount <= 0)
        {
            if(clearjudge == true)
            {
                //  クリアの効果音を鳴らす
                audioSource.PlayOneShot(clearsound);
                //  通常時のBGMを停止する
                audioSource_Battle.Stop();
                //  クリア後のBGMを流す
                audioSource_Clear.Play();
                //  クリアテキストを表示する
                cleartext.SetActive(true);
                //  次のステージへのワープを出現させる
                warp.SetActive(true);
                //  クリア判定を一度だけ実行する
                clearjudge = false;
            }
        }
    }
}
