using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rb;   //  Rigidbodyの変数
    [SerializeField] GameObject target;    //   ターゲットのオブジェクトを格納
    public float bulletspeed = 6.0f;     //   弾の速度
    private Vector3 FirstTargetPos;     //  弾の向かう先の格納場所

    // Start is called before the first frame update
    void Start()
    {
        //  TargetPositionというオブジェクトを見つける
        target = GameObject.Find("TargetPosition");
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
          bulletspeed * Time.deltaTime);
        //  ターゲットの座標に到達した時
        if(this.transform.position == FirstTargetPos)
        {
            //  このオブジェクトを破壊する
            Destroy(this.gameObject);
        }
    }

    //　オブジェクトとの接触を検知
    void OnCollisionEnter(Collision other)
    {
        //　このオブジェクトを破壊する
        Destroy(this.gameObject);
    }
}
