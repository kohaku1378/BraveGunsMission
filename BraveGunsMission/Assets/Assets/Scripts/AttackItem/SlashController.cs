using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{
    private Rigidbody rb;   //  Rigidbodyの変数
    [SerializeField] private GameObject target;    //   ターゲットのオブジェクトを格納
    [SerializeField] private GameObject playerHP;     //  プレイヤーのHPを管理するオブジェクトを格納
    [SerializeField] private GameObject gameOver;   //  ゲームオーバーを管理するオブジェクトを格納
    PlayerHPController playerHPController;      //  プレイヤーのHP管理スクリプトを格納
    GameOverController gameOverController;      //  ゲームオーバー管理スクリプトを格納
    public float slashspeed = 8.0f;     //   砲弾の速度
    public int slashpower = 6;    //  砲弾の攻撃力
    private Vector3 FirstTargetPos;     //  砲弾の向かう先の格納場所

    // Start is called before the first frame update
    void Start()
    {
        //  攻撃対象のオブジェクトを見つける
        target = GameObject.Find("player");
        //  プレイヤーのHP管理を行うオブジェクトを取得
        playerHP = GameObject.Find("PlayerHPController");
        //  プレイヤーのHP管理を行うスクリプトを取得
        playerHPController = playerHP.GetComponent<PlayerHPController>();
        //  ゲームオーバーの制御を行うオブジェクトを取得
        gameOver = GameObject.Find("GameOverController");
        //  ゲームオーバーの制御を行うスクリプトを取得
        gameOverController = gameOver.GetComponent<GameOverController>();
        //  弾が向かう先を固定する
        FirstTargetPos = target.transform.position;
        //  Rigidbodyをrbと定義
        rb = GetComponent<Rigidbody> ();
        //　3秒後にこのオブジェクトを破壊する
        Destroy(this.gameObject, 3);
    }

    void FixedUpdate()
    {
        //スタート位置、ターゲットの座標、速度を決め動かす
        transform.position = Vector3.MoveTowards(
          transform.position,
          FirstTargetPos,
          slashspeed * Time.deltaTime);
        //  ターゲットの座標に到達した時
        if(this.transform.position == FirstTargetPos)
        {
            //  このオブジェクトを破壊する
            Destroy(this.gameObject);
        }
    }

    //　オブジェクトとの接触を検知
    void OnCollisionStay(Collision other)
    {
        //  プレイヤーとの接触を検知
        if(other.gameObject.tag == "Player")
        {
            //  敵の動きが有効な時
            if(gameOverController.enemyswich == true)
            {
                //  プレイヤーのダメージ処理を呼び出す
                playerHPController.Damage(slashpower);
            }
        }
    }
}
