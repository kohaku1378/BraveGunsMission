using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private ItemRespawn healitemRespawn;  //  回復アイテムのリスポーンのスクリプトを格納する変数
    [SerializeField] private ItemRespawn magazineitemRespawn;  //  マガジン補充のアイテムリスポーンのスクリプトを格納する変数
    public float playerspeed; //　プレイヤーの移動速度
    public float maxSpeed; //　最高移動速度
    public float jumpForce; //　ジャンプ力
    public bool playerswich; //  プレイヤーの動きを制御
    public AudioClip HealSound;     //   回復の効果音
    public AudioClip MagazineSound;     //   アイテム入手の効果音
    public AudioSource audioSource;     //  オーディオソースを格納
    public bool healswich = true;  //  回復アイテム判定の処理を制御
    public bool magazineswich = true;  //  マガジンアイテム判定の処理を制御
    private bool isJumping; //　地面との接触判定
    private Rigidbody rb;   //  Rigidbodyの変数
    private Vector3 FirstPos;   //  プレイヤーの開始位置
    
    // Start is called before the first frame update
    void Start()
    {
        //  Rigidbodyを変数に格納する
        rb = GetComponent<Rigidbody> ();
        //  ジャンプを有効にする
        isJumping = false;
        //  プレイヤーの動きを有効にする
        playerswich = true;
        //  プレイヤーの初期位置を記録
        FirstPos = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //  プレイヤーの動きが有効な時
        if(playerswich == true)
        {
            //　プレイヤーを前に移動
            if (Input.GetKey(KeyCode.W))
            {
                rb.AddForce(transform.forward * playerspeed);
            }
            //　プレイヤーを後ろに移動
            if (Input.GetKey(KeyCode.S))
            {
                rb.AddForce(-transform.forward * playerspeed);
            }
            //　プレイヤーを左に移動
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(-transform.right * playerspeed);
            }
            //　プレイヤーを右に移動
            if (Input.GetKey(KeyCode.D))
            {
                rb.AddForce(transform.right * playerspeed);
            }

            //　プレイヤーのジャンプ処理
            if(Input.GetKey(KeyCode.Space))
            {
                if(isJumping == false)
                {
                    rb.velocity = new Vector3(0,jumpForce,0);
                }
            //  空中でジャンプできないようにする
            isJumping = true;
            }

            //　プレイヤーの速度制限
            if (rb.velocity.magnitude > maxSpeed)
            {
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }

            //  プレイヤーが高さ-10以下になった時
            if(this.transform.position.y <= -10)
            {
                //  プレイヤーの位置を初期位置に移動させる
                this.transform.position = FirstPos;
            }
        }
    }

    void OnCollisionStay(Collision other){
        //  地面との接触を検知
        if(other.gameObject.tag == "ground")
        {
            isJumping = false;
        }

        //  回復アイテムとの接触を検知
        if(other.gameObject.tag == "healitem")
        { 
            //  処理を一度だけ呼び出す
            if(healswich == true)
            {
                //　回復の効果音を鳴らす
                audioSource.PlayOneShot(HealSound);
                //  アイテムの数を-1する
                healitemRespawn.itemcount = healitemRespawn.itemcount - 1;
                //  処理が繰り返さないようにする
                healswich = false;
            }
        }

        //  マガジン補充アイテムとの接触を検知
        if(other.gameObject.tag == "magazineitem")
        { 
            //  処理を一度だけ呼び出す
            if(magazineswich == true)
            {
                //　マガジン取得の効果音を鳴らす
                audioSource.PlayOneShot(MagazineSound);
                //  アイテムの数を-1する
                magazineitemRespawn.itemcount = magazineitemRespawn.itemcount - 1;
                //  処理が繰り返さないようにする
                magazineswich = false;
            }
        }
    }
}
