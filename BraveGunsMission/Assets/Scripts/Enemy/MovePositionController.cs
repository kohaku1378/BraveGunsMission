using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionController : MonoBehaviour
{
    public float movetime;      //  移動までの時間
    public float moverange = 22;     //  移動範囲の指定
    private float timecount;     //  経過時間を記録
    private float posx;      //  X方向の移動先
    private float posz;      //  Z方向の移動先

    // Start is called before the first frame update
    void Start()
    {
        timecount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //  経過時間を計測する
        timecount -= Time.deltaTime;
        
        //  移動までの時間が0以下の時
        if(timecount <= 0.0f)
        {
            //  X方向のランダムな位置を取得
            posx = Random.Range(-moverange,moverange);
            //  Z方向のランダムな位置を取得
            posz = Random.Range(-moverange,moverange);
            //  このオブジェクトの位置を変更する
            this.transform.position = new Vector3(posx, this.transform.position.y, posz);
            //  移動までの時間を設定する
            timecount = movetime;
        }
    }
}
