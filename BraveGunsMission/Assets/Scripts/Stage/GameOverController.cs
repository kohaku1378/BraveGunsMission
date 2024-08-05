using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject gameover;   //  ゲームオーバー画面を格納
    [SerializeField] private PlayerHPController playerHPController;     //  プレイヤーのHPを管理するスクリプトを格納
    [SerializeField] private GunController gunController;   //  銃のスクリプトを格納
    [SerializeField] private PlayerController playerController;     //  プレイヤーのスクリプトを格納
    [SerializeField] private PointofviewController pointofviewController;   //  視点移動のスクリプトを格納
    public bool enemyswich = true;   //  全ての敵の動きを制御する
    public AudioClip DeadSound;     //   ゲームオーバーの効果音
    public AudioSource audioSource;     //  オーディオソースを格納
    private bool gameoverswich = true;      //  ゲームオーバーを制御

    // Update is called once per frame
    void Update()
    {
        //  プレイヤーのHPが0以下の時
        if(playerHPController.PlayerHP <= 0)
        {
            //  ゲームオーバーの処理を有効にする
            if(gameoverswich == true)
            {
                //　ゲームオーバーの効果音を鳴らす
                audioSource.PlayOneShot(DeadSound);
                //  ゲームオーバーの画面を表示
                gameover.SetActive(true);
                //  ゲームの各処理を停止する
                enemyswich = false;
                gunController.gunswich = false;
                playerController.playerswich = false;
                pointofviewController.viewswich = false;
                //  ゲームオーバーの処理を無効にする
                gameoverswich = false;
            }
        }
    }

}
