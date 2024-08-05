using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealController : MonoBehaviour
{
    [SerializeField] GameObject playerHP;     //  プレイヤーのHP管理オブジェクトを格納
    [SerializeField] GameObject heal;     //  アイテムリスポーンのオブジェクトを格納
    [SerializeField] GameObject player;     //  プレイヤーのオブジェクトを格納
    [SerializeField] private int Healcount;   //  回復するHPの値
    PlayerHPController playerHPController;  //  プレイヤーのHP管理スクリプトを格納する変数
    ItemRespawn itemRespawn;  //  アイテムリスポーンのスクリプトを格納する変数
    PlayerController playerController;  //  プレイヤーのスクリプトを格納
    
    // Start is called before the first frame update
    void Start()
    {
        //  プレイヤーのHP管理を行うオブジェクトを取得
        playerHP = GameObject.Find("PlayerHPController");
        //  プレイヤーのHP管理を行うスクリプトを取得
        playerHPController = playerHP.GetComponent<PlayerHPController>();
        //  回復アイテムの管理を行うオブジェクトを取得
        heal = GameObject.Find("HealItemPanel");
        //  回復アイテムの管理を行うスクリプトを取得
        itemRespawn = heal.GetComponent<ItemRespawn>();
        //  プレイヤーの制御を行うオブジェクトを取得
        player = GameObject.Find("player");
        //  プレイヤーの制御を行うスクリプトを取得
        playerController = player.GetComponent<PlayerController>();
    }

    void OnCollisionStay(Collision other)
    {
        //  プレイヤーとの接触を検知
        if(other.gameObject.tag == "Player")
        {
            //  プレイヤーのHPが10の時
            if(playerHPController.PlayerHP == 10)
            {
                //  リスポーンまでの計測時間を0にする
                itemRespawn.timecount = 0;
                //  音の処理を有効にする
                playerController.healswich = true;
                //　このオブジェクトを破壊する
                Destroy(this.gameObject);
                Debug.Log("破壊");
            }
            //  プレイヤーのHPが10以下の時
            else if(playerHPController.PlayerHP < 10)
            {
                //  プレイヤーのHPの値を10に回復する
                playerHPController.PlayerHP = Healcount;
                //  プレイヤーのHPゲージを最大にする
                playerHPController.gauge.value = Healcount;
                //  リスポーンまでの計測時間を0にする
                itemRespawn.timecount = 0;
                //  音の処理を有効にする
                playerController.healswich = true;
                //　このオブジェクトを破壊する
                Destroy(this.gameObject);
                Debug.Log("回復");
            }
        }
    }
}
