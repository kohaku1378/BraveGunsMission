using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBossController : MonoBehaviour
{
    [SerializeField] private GameObject player;     //  プレイヤーを格納
    [SerializeField] private GameObject enemydown;    //  　敵のダウンモーションを格納
    [SerializeField] private GameObject maincamera;     //    カメラを格納
    [SerializeField] private GameObject Slashposition;   //　斬撃の発射場所
    [SerializeField] private GameObject Slash;   //　斬撃を格納
    [SerializeField] private GameObject MovePos;    //  移動先を格納
    [SerializeField] private Transform self;    //  この敵の位置を格納
    [SerializeField] private PlayerHPController playerHPController;     //  プレイヤーのHPを管理するスクリプトを格納
    [SerializeField] private StageController stageController;   //  ステージを管理するスクリプトを格納
    [SerializeField] private GameOverController gameOverController;   //  ゲームオーバーを管理するスクリプトを格納
    public float Attackinterval;    //  攻撃と攻撃の間の時間
    public float timecount;     //  経過時間を計測する
    public AudioClip hitsound;     //   弾が敵に当たった効果音
    public AudioClip downsound;    //  敵を倒した時の効果音
    public AudioClip slashsound;    //  斬撃を発射したときの効果音
    public AudioClip rushsound;     //   突撃したときの効果音
    public AudioSource audioSource;     //  オーディオソースを格納
    public int enemypower = 3;    //  敵の攻撃力
    public float EnemyHP = 3f;   //  敵のHP
    public float Enemyspeed;
    private Vector3 playerpos;     //  プレイヤーの位置
    private Vector3 AttackPos;     //  攻撃場所を格納
    private int RandomAttack;     //    攻撃を決める変数
    private Animator anim;  //　Animatorを定義
    private bool Attackswich = true;  //　攻撃のON、OFFを制御
    private bool Moveswich = true;  //  移動のON、OFFを制御
    private bool Rushswich = false;     //  突撃のON、OFFを制御

    void Start()
    {
        //　変数animに、Animatorコンポーネントを設定する
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame  
    void Update()
    {
        //  敵の動きが有効な時
        if(gameOverController.enemyswich == true)
        {
            if(Moveswich == true)
            {
                //  移動先に移動する
                this.transform.position = Vector3.MoveTowards(this.transform.position,MovePos.transform.position,0.1f);
            }

            if(Rushswich == true)
            {
                //  突撃する
                this.transform.position = Vector3.MoveTowards(this.transform.position, AttackPos, Enemyspeed * Time.deltaTime);
            }

            //  経過時間を計測する
            timecount += Time.deltaTime;     
            //  プレイヤーの位置を更新する
            playerpos = player.transform.position;
            //  プレイヤーの方向を向く
            self.LookAt(playerpos);

            //  経過時間が攻撃間隔より大きいとき
            if(timecount >= Attackinterval)
            {
                if(Attackswich == true)
                {
                    //  ランダムな値をRandomAttackに格納する
                    RandomAttack = Random.Range(1, 100);

                    if(RandomAttack <= 50)
                    {
                        //  ランダム移動を停止する
                        Moveswich = false;
                        //　攻撃準備のアニメーションを有効にする
                        anim.SetBool("Attack1", true);
                        //  攻撃を開始する
                        Invoke(nameof(Attack1), 2f);
                        //  攻撃を有効にする
                        Attackswich = false;
                    }
                    else
                    {
                        //  ランダム移動を停止する
                        Moveswich = false;
                        //　攻撃のアニメーションを有効にする
                        anim.SetBool("Attack2", true);
                        //  攻撃を開始する
                        Invoke(nameof(Attack2), 4.5f);
                        //  攻撃を有効にする
                        Attackswich = false;
                    }
                }
            }

            if(EnemyHP <= 0)
            {
                //　生成位置
                Vector3 pos = this.transform.position;
                //　生成時の回転
                Quaternion rot = this.transform.rotation;
                //  撃破の効果音を鳴らす
                AudioSource.PlayClipAtPoint(downsound, maincamera.transform.position);
                //  ステージにいた敵の数を-1する
                stageController.enemycount -= 1f;
                //　撃破モーションを指定位置に生成
                Instantiate(enemydown, pos, rot);
                //　このオブジェクトを破壊する
                Destroy(this.gameObject);
            }
        }
    }

    void Attack1()
    {
        // 　突撃場所を設定する
        AttackPos = player.transform.position;
        //  突撃攻撃を実行する
        Rushswich = true;
        //  突撃したときの効果音を鳴らす
        audioSource.PlayOneShot(rushsound);
        //　攻撃のアニメーションを無効にする
        anim.SetBool("Attack1", false);
        //  攻撃状態を終了する
        Invoke(nameof(AttackStart), 5f);
    }

    void Attack2()
    {
        //　生成位置
        Vector3 pos = Slashposition.transform.position;
        //　生成時の回転
        Quaternion rot = this.transform.rotation;
        //　攻撃のアニメーションを無効にする
        anim.SetBool("Attack2", false);
        //　斬撃を指定位置に生成
        Instantiate(Slash, pos, rot);
        //  斬撃を発射する効果音を鳴らす
        audioSource.PlayOneShot(slashsound);
        //  攻撃状態を終了する
        Invoke(nameof(AttackStart), 1f);
    }

    void AttackStart()
    {
        //  突撃攻撃を停止する
        Rushswich = false;
        //  ランダム移動を有効にする
        Moveswich = true;
        //  攻撃を有効にする
        Attackswich = true;
        //  経過時間を0にする
        timecount = 0;
    }

    void OnCollisionEnter(Collision other){
        if(other.gameObject.tag == "bullet")
        {
            //  弾が敵に当たった効果音を鳴らす
            audioSource.PlayOneShot(hitsound);
            //  敵のHPを1減らす
            EnemyHP -= 1f;
        }
    }

    void OnCollisionStay(Collision other)
    {
        //  プレイヤーとの接触を検知
        if(other.gameObject.tag == "Player")
        {
            if(gameOverController.enemyswich == true)
            {
                //  プレイヤーのダメージ処理を呼び出す
                playerHPController.Damage(enemypower);
            }
        }
    }
}
