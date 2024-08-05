using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpEnemyController : MonoBehaviour
{
    [SerializeField] private GameObject player;     //  プレイヤーを格納
    [SerializeField] private GameObject enemydown;    //  　敵のダウンモーションを格納
    [SerializeField] private GameObject maincamera;     //    カメラを格納
    [SerializeField] private Transform self;    //  この敵の位置を格納
    [SerializeField] private PlayerHPController playerHPController;     //  プレイヤーのHPを管理するスクリプトを格納
    [SerializeField] private StageController stageController;   //  ステージを管理するスクリプトを格納
    [SerializeField] private GameOverController gameOverController;   //  ゲームオーバーを管理するスクリプトを格納
    public float Attackinterval;    //  攻撃と攻撃の間の時間
    public float timecount;     //  経過時間を計測する
    public AudioClip hitsound;     //   弾が敵に当たった効果音
    public AudioClip downsound;    //  敵を倒した時の効果音
    public AudioClip jumpsound;    //  ジャンプした時の効果音
    public AudioSource audioSource;     //  オーディオソースを格納
    public int enemypower = 3;    //  敵の攻撃力
    public float EnemyHP = 3f;   //  敵のHP
    private Vector3 target;     //  攻撃対象の位置

    // Update is called once per frame
    void Update()
    {
        //  敵の動きが有効な時
        if(gameOverController.enemyswich == true)
        {
            //  経過時間を計測する
            timecount += Time.deltaTime;     
            //  攻撃対象の位置を更新する
            target = player.transform.position;
            self.LookAt(target);


            //  経過時間が攻撃間隔より大きいとき
            if(timecount >= Attackinterval)
            {
                //  攻撃対象の真上に移動
                this.transform.position = new Vector3(target.x, target.y + 10, target.z);
                //  弾が敵に当たった効果音を鳴らす
                audioSource.PlayOneShot(jumpsound);
                //  経過時間を0にする
                timecount = 0;  
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
