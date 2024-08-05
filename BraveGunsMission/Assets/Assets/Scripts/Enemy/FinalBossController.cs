using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossController : MonoBehaviour
{
    [SerializeField] private GameObject player;     //  プレイヤーを格納
    [SerializeField] private GameObject enemydown;    //  　敵のダウンモーションを格納
    [SerializeField] private GameObject maincamera;     //    カメラを格納
    [SerializeField] private GameObject Laserposition;   //　レーザーの発射場所
    [SerializeField] private GameObject Laser;   //　レーザーを格納
    [SerializeField] private GameObject LaserCharge;   //　チャージ中のレーザーを格納
    [SerializeField] private GameObject MovePos;    //  移動先を格納
    [SerializeField] private GameObject MoveWall;    //  動く壁を格納
    [SerializeField] private PlayerHPController playerHPController;     //  プレイヤーのHPを管理するスクリプトを格納
    [SerializeField] private GameOverController gameOverController;   //  ゲームオーバーを管理するスクリプトを格納
    [SerializeField] private FinalBoss_ArmAttack RightArm;   //  右腕の攻撃を管理するスクリプトを格納
    [SerializeField] private FinalBoss_ArmAttack LeftArm;   //  左腕の攻撃を管理するスクリプトを格納
    public float Attackinterval;    //  攻撃と攻撃の間の時間
    public float timecount;     //  経過時間を計測する
    public AudioClip hitsound;     //   弾が敵に当たった効果音
    public AudioClip downsound;    //  敵を倒した時の効果音
    public AudioClip chargesound;    //  レーザーをチャージするときの効果音
    public AudioClip lasersound;    //  レーザーを発射したときの効果音
    public AudioClip punchsound;     //   パンチ攻撃の効果音
    public AudioClip createwallsound;     //   動く壁生成の効果音
    public AudioClip magicsound;     //   動く壁生成の魔法の効果音
    public AudioSource audioSource;     //  オーディオソースを格納
    public int enemypower = 3;    //  敵の攻撃力
    public float EnemyHP = 3f;   //  敵のHP
    private Vector3 playerpos;     //  プレイヤーの位置
    private Vector3 wallpos;    //  動く壁の生成位置
    private int RandomAttack;     //    攻撃を決める変数
    private float RandomWallPos;     //    壁を作る位置を決める変数
    private bool Attackswich = true;  //　攻撃処理のON、OFFを制御
    private bool Moveswich = true;  //  移動処理のON、OFFを制御
    private bool attackpunchswich = false;     //  パンチ攻撃の終了処理のON、OFFを制御

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

            //  パンチ攻撃の終了処理が有効な時
            if(attackpunchswich == true)
            {
                //  右腕の攻撃準備処理が有効な時
                if(RightArm.setswich == true)
                {
                    //  左腕の攻撃準備処理が有効な時
                    if(LeftArm.setswich == true)
                    {
                        //  攻撃状態を終了する
                        AttackStart();
                        //  パンチ攻撃の終了処理を無効にする
                        attackpunchswich = false;   
                    }    
                }
            }

            //  経過時間を計測する
            timecount += Time.deltaTime;     
            //  プレイヤーの位置を更新する
            playerpos = player.transform.position;

            //  経過時間が攻撃間隔より大きいとき
            if(timecount >= Attackinterval)
            {
                if(Attackswich == true)
                {
                    //  1～100のランダムな値をRandomAttackに格納する
                    RandomAttack = Random.Range(1, 100);

                    if(RandomAttack <= 33)
                    {
                        //  ランダム移動を停止する
                        Moveswich = false;
                        //  パンチ攻撃の処理を開始する（コルーチンの実行）  
                        StartCoroutine(AttackPunch()); 
                        //  攻撃を無効にする
                        Attackswich = false;
                    }
                    else if(RandomAttack <= 66)
                    {
                        //  ランダム移動を停止する
                        Moveswich = false;
                        //  チャージの効果音を鳴らす
                        audioSource.PlayOneShot(chargesound);
                        //  チャージ中のレーザーを有効にする
                        LaserCharge.SetActive(true);
                        //  攻撃を開始する
                        Invoke(nameof(AttackLaser), 4.5f);
                        //  攻撃を無効にする
                        Attackswich = false;
                    }
                    else
                    {
                        //  ランダム移動を停止する
                        Moveswich = false;
                        //  動く壁を生成する魔法の効果音を鳴らす
                        audioSource.PlayOneShot(magicsound);
                        //  動く壁の攻撃処理を開始する（コルーチンの実行）  
                        StartCoroutine(AttackWall()); 
                        //  攻撃を無効にする
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
                //　撃破モーションを指定位置に生成
                Instantiate(enemydown, pos, rot);
                //　このオブジェクトを破壊する
                Destroy(this.gameObject);
            }
        }
    }

    IEnumerator AttackPunch()
    {
        //  右腕の攻撃を開始
        RightArm.punchswich = true;
        //  パンチ攻撃の効果音を鳴らす
        audioSource.PlayOneShot(punchsound);

        //  ２秒待機する
        yield return new WaitForSeconds(2f);

        //  左腕の攻撃を開始
        LeftArm.punchswich = true;
        //  パンチ攻撃のの効果音を鳴らす
        audioSource.PlayOneShot(punchsound);
        //  パンチ攻撃の終了処理を有効にする
        attackpunchswich = true;
    }

    void AttackLaser()
    {
        //　生成位置
        Vector3 pos = Laserposition.transform.position;
        //　生成時の回転
        Quaternion rot = Laser.transform.rotation;
        //　レーザーを指定位置に生成
        Instantiate(Laser, pos, rot);
        //  チャージ中のレーザーを無効にする
        LaserCharge.SetActive(false);
        //  レーザーを発射する効果音を鳴らす
        audioSource.PlayOneShot(lasersound);
        //  攻撃状態を終了する
        Invoke(nameof(AttackStart), 1f);
    }

    IEnumerator AttackWall()
    {
        //  3秒待機する
        yield return new WaitForSeconds(3f);

        //  動く壁の生成位置を設定する
        wallpos = new Vector3(MoveWall.transform.position.x, MoveWall.transform.position.y, -7);
        //　生成時の回転
        Quaternion rot = MoveWall.transform.rotation;
        //　動く壁を指定位置に生成
        Instantiate(MoveWall, wallpos, rot);
        //  動く壁を生成する効果音を鳴らす
        audioSource.PlayOneShot(createwallsound);
        
        //  5秒待機する
        yield return new WaitForSeconds(5f);

        //  動く壁の生成位置を設定する
        wallpos = new Vector3(MoveWall.transform.position.x, MoveWall.transform.position.y, 7);
        //　動く壁を指定位置に生成
        Instantiate(MoveWall, wallpos, rot);
        //  動く壁を生成する効果音を鳴らす
        audioSource.PlayOneShot(createwallsound);

        //  5秒待機する
        yield return new WaitForSeconds(5f);

        //  -7～7の間のランダムな値をRandomWallPosに格納する
        RandomWallPos = Random.Range(-7, 7);
        //  動く壁の生成位置を設定する
        wallpos = new Vector3(MoveWall.transform.position.x, MoveWall.transform.position.y, RandomWallPos);
        //　動く壁を指定位置に生成
        Instantiate(MoveWall, wallpos, rot);
        //  動く壁を生成する効果音を鳴らす
        audioSource.PlayOneShot(createwallsound);
        //  攻撃状態を終了する
        Invoke(nameof(AttackStart), 12f);
    }

    void AttackStart()
    {
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
