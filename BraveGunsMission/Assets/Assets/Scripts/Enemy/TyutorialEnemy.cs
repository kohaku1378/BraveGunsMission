using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TyutorialEnemy : MonoBehaviour
{
    public float EnemyHP = 3f;   //  敵のHP
    [SerializeField] private GameObject maincamera;     //    カメラを格納   //  プレイヤーのHPを管理するスクリプトを格納
    [SerializeField] private StageController stageController;   //  ステージを管理するスクリプトを格納
    [SerializeField] private GameOverController gameOverController;   //  ゲームオーバーを管理するスクリプトを格納
    public AudioClip hitsound;     //   弾が敵に当たった効果音
    public AudioClip downsound;    //  敵を倒した時の効果音
    public AudioSource audioSource;     //  オーディオソースを格納

    // Update is called once per frame
    void FixedUpdate()
    {
        //  敵の動きが有効な時
        if(gameOverController.enemyswich == true)
        {
            if(EnemyHP <= 0)
            {
                //  撃破の効果音を鳴らす
                AudioSource.PlayClipAtPoint(downsound, maincamera.transform.position);
                //  ステージにいた敵の数を-1する
                stageController.enemycount -= 1f;
                //　このオブジェクトを破壊する
                Destroy(this.gameObject);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "bullet")
        {
            //  弾が敵に当たった効果音を鳴らす
            audioSource.PlayOneShot(hitsound);
            //  敵のHPを1減らす
            EnemyHP -= 1f;
        }
    }
}
