using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPController : MonoBehaviour
{
    public int PlayerHP = 10;    //  体力ゲージ現状値
    public bool hit = true;   //  無敵時間の判定
    public Slider gauge;    //  ゲージ表示用
    public AudioClip damageclip;     //   ダメージを受ける効果音
    public AudioSource audioSource;     //  オーディオソースを格納
    void Start()
    {
        //  HPゲージをプレイヤーの初期HPに合わせる
        gauge.value = PlayerHP;    
    }

    public void Damage(int x)
    {
        //  敵の攻撃力が0より大きい時
        if (x > 0)
        {
            //  ダメージ判定が有効の時
            if(hit == true)
            {
                //　ダメージを受ける効果音を鳴らす
                audioSource.PlayOneShot(damageclip);
                //  PlayerHPを敵の攻撃力分減らす
                PlayerHP = PlayerHP - x;
                //  HPゲージを減らす
                gauge.value = PlayerHP;
                //  ダメージ判定を無効にする
                hit = false;
            }
        }
    }
}
