using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] private GameObject player;     //  プレイヤーを格納
    [SerializeField] private PlayerHPController playerHPController;    //  プレイヤーのHP管理スクリプトを格納
    private Animator anim;  //　Animatorを定義

    // Start is called before the first frame update
    void Start()
    {
        //　変数animに、Animatorコンポーネントを設定する
        anim = gameObject.GetComponent<Animator>();
    }

    void OnCollisionEnter(Collision other){
        //  敵との衝突を検知する
        if(other.gameObject.tag == "enemy")
        {
            //  無敵時間の処理を呼び出す
            StartCoroutine("Invincible");
        }
    }

    IEnumerator Invincible()
    {
        //  無敵時間のアニメーションを実行する
        anim.Play("InvincibleMotion",0 ,0.0f);
        //  ダメージ処理を先に発生させるため0.1f遅らせる
        yield return new WaitForSeconds(0.1f);
        //  プレイヤーのレイヤーを変え、無敵にする
        player.layer = 3;
        //  無敵時間
        yield return new WaitForSeconds(3.0f);
        //  プレイヤーのレイヤーを戻し、無敵を解除する
        player.layer = 6;
        //  ダメージ判定を有効にする
        playerHPController.hit = true;
    }
}
