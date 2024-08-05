using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss_DownController : MonoBehaviour
{
    [SerializeField] private GameObject Stage;  //  ステージを管理するオブジェクトを格納
    StageController stageController;   //  ステージを管理するスクリプトを格納

    // Start is called before the first frame update
    void Start()
    {
        //  ステージの管理を行うオブジェクトを取得
        Stage = GameObject.Find("StageController");
        //  ステージの管理を行うスクリプトを取得
        stageController = Stage.GetComponent<StageController>();
        //  ラスボスの撃破処理を開始する
        Invoke(nameof(DownProcess), 9f);
    }

    void DownProcess()
    {
        //  ステージにいた敵の数を-1する
        stageController.enemycount -= 1f;
        //  このオブジェクトを破壊する
        Destroy(this.gameObject);
    }
}
