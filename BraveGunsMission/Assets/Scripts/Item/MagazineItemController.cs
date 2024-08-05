using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineItemController : MonoBehaviour
{
    [SerializeField] GameObject gun;     //  銃のオブジェクトを格納
    [SerializeField] GameObject magazine;     //  アイテムリスポーンのオブジェクトを格納
    [SerializeField] GameObject player;     //  プレイヤーのオブジェクトを格納
    GunController gunController;  //  銃のスクリプトを格納する変数
    ItemRespawn itemRespawn;  //  アイテムリスポーンのスクリプトを格納する変数
    PlayerController playerController;  //  プレイヤーのスクリプトを格納
    // Start is called before the first frame update
    void Start()
    {
        //  銃の制御を行うオブジェクトを取得
        gun = GameObject.Find("Gun");
        //  銃の制御を行うスクリプトを取得
        gunController = gun.GetComponent<GunController>();
        //  マガジンアイテムの管理を行うオブジェクトを取得
        magazine = GameObject.Find("MagazineItemPanel");
        //  マガジンアイテムの管理を行うスクリプトを取得
        itemRespawn = magazine.GetComponent<ItemRespawn>();
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
            //  所持しているマガジンの数を1増やす
            gunController.magazines += 1;
            //  リスポーンまでの計測時間を0にする
            itemRespawn.timecount = 0;
            //  音の処理を有効にする
            playerController.magazineswich = true;
            //　このオブジェクトを破壊する
            Destroy(this.gameObject);
        }
    }
}
