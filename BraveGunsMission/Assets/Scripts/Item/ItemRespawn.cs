using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRespawn : MonoBehaviour
{
    [SerializeField] private GameObject Item;   //　アイテムのプレハブを格納
    [SerializeField] private GameObject ItemPos;   //　アイテムの出現場所
    public float itemcount = 1;   //   現在あるアイテムの数を格納
    public float timecount;    //  時間を計測する
    public float respawntime;    //  アイテムが復活する時間
    private Vector3 pos;    //  アイテムの位置を格納
    private Quaternion rot;     //  アイテムの角度を格納
    
    // Start is called before the first frame update
    void Start()
    {
        //　生成位置
        pos = ItemPos.transform.position;
        //  生成角度
        rot = Item.transform.rotation;
        //　アイテムを指定位置に生成
        Instantiate(Item, pos, rot);
    }

    // Update is called once per frame
    void Update()
    {
        //  経過時間を計測する
        timecount += Time.deltaTime;
        //アイテムが0以下の時
        if(itemcount <= 0)
        {
            //  経過時間がリスポーン時間以上の時
            if(timecount >= respawntime)
            {
                //　アイテムを指定位置に生成
                Instantiate(Item, pos, rot);
                //  アイテムの数を＋1する
                itemcount = itemcount + 1;
                //  経過時間を0にする
                timecount = 0;
            }
        }
    }
}
