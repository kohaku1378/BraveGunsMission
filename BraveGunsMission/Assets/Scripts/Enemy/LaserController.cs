using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    private Rigidbody rb;   //  Rigidbodyの変数
    [SerializeField] private GameObject playerHP;     //  プレイヤーのHPを管理するオブジェクトを格納
    [SerializeField] private GameObject gameOver;   //  ゲームオーバーを管理するオブジェクトを格納
    PlayerHPController playerHPController;      //  プレイヤーのHP管理スクリプトを格納
    GameOverController gameOverController;      //  ゲームオーバー管理スクリプトを格納
    public float laserspeed = 6.0f;     //   レーザーの速度
    public int laserpower = 5;    //  レーザーの攻撃力

    // Start is called before the first frame update
    void Start()
    {
        //  Rigidbodyをrbと定義
        rb = GetComponent<Rigidbody> ();
        //  プレイヤーのHP管理を行うオブジェクトを取得
        playerHP = GameObject.Find("PlayerHPController");
        //  プレイヤーのHP管理を行うスクリプトを取得
        playerHPController = playerHP.GetComponent<PlayerHPController>();
        //  ゲームオーバーの制御を行うオブジェクトを取得
        gameOver = GameObject.Find("GameOverController");
        //  ゲームオーバーの制御を行うスクリプトを取得
        gameOverController = gameOver.GetComponent<GameOverController>();
        //　3秒後にこのオブジェクトを破壊する
        Destroy(this.gameObject, 3);
    }

    void FixedUpdate()
    {
        //  X方向に移動する
        this.transform.position += new Vector3(1, 0, 0) * laserspeed * Time.deltaTime;
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
                playerHPController.Damage(laserpower);
            }
        }
    }
}
