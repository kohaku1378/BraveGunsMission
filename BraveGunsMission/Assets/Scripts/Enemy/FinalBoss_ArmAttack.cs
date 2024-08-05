using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_ArmAttack : MonoBehaviour
{
    [SerializeField] private GameObject player;     //  プレイヤーを格納
    public Vector3 LaunchPosition;    //  腕の発射位置
    private Vector3 AttackPos;     //  攻撃場所を格納
    private bool backswich = false;     //  攻撃前の場所に戻る処理のON、OFFを制御する
    public bool punchswich = false;    //  攻撃の処理のON、OFFを制御する
    public bool setswich = true;      //  攻撃前の準備処理のON、OFFを制御する
    public float Enemyspeed;    //  腕の移動速度  

    // Update is called once per frame
    void FixedUpdate()
    {
        //  攻撃の準備処理が有効な時
        if(setswich == true)
        {
            //  攻撃前の腕の位置を更新する
            LaunchPosition = this.transform.position;
            //  攻撃場所を更新する
            AttackPos = player.transform.position;
            //  攻撃前の位置に戻る処理が無効にする
            backswich = false;
        }
        //  攻撃の処理が有効な時
        if(punchswich == true)
        {
            //  攻撃の準備処理を無効にする
            setswich = false;
            //  このオブジェクトを攻撃場所まで移動させる
            this.transform.position = Vector3.MoveTowards(this.transform.position, AttackPos, Enemyspeed * Time.deltaTime);
            //  このオブジェクトが攻撃場所まで移動したとき
            if(this.transform.position == AttackPos)
            {
                //  攻撃前の位置に戻る処理を有効にする
                backswich = true;
            }
        }
        //  攻撃前の位置に戻る処理が有効な時
        if(backswich == true)
        {
            //  攻撃の処理を無効にする
            punchswich = false;
            //  このオブジェクトを攻撃前の位置に移動させる
            this.transform.position = Vector3.MoveTowards(this.transform.position, LaunchPosition, Enemyspeed * Time.deltaTime);
            //  このオブジェクトが攻撃前の位置に戻ったとき
            if(this.transform.position == LaunchPosition)
            {
                //  攻撃の準備処理を有効にする
                setswich = true;
            }
        }
    }
}
