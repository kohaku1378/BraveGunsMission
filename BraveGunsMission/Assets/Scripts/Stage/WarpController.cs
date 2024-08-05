using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WarpController : MonoBehaviour
{
    [SerializeField] private string scene1;     //  シーン1を格納
    [SerializeField] private string scene2;     //  シーン2を格納
    [SerializeField] private string scene3;     //  シーン3を格納
    [SerializeField] FadeManagementScript fadeManagementScript; //  フェードアウトを制御するスクリプトを格納
    public AudioClip WarpSound;     //   ワープの効果音
    public AudioSource audioSource;     //  オーディオソースを格納
    private int RandomStage;    //  ランダムにステージを選択する
    private string scene;   //  移動先のシーンを格納

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            //  1～100のランダムな値をRandomStageに格納する
            RandomStage = Random.Range(1, 100);
            //  RadomStageの値が33以下の場合
            if(RandomStage <= 33)
            {
                //  遷移先のシーンをscene1に設定する
                scene = scene1;
            }
            //  RadomStageの値が66以下の場合
            else if(RandomStage <= 66)
            {
                //  遷移先のシーンをscene2に設定する
                scene = scene2;
            }
            //  RadomStageの値がそれ以外の場合
            else
            {
                //  遷移先のシーンをscene3に設定する
                scene = scene3;
            }
            //　ワープの効果音を鳴らす
            audioSource.PlayOneShot(WarpSound);
            // フェードアウトを実行するクリプトを呼び出す
            fadeManagementScript.CallCoroutine();
            //  シーンを切り替える処理を呼び出す
            StartCoroutine("SceneChange");
        }
    }

    IEnumerator SceneChange()
    {
        //  効果音を鳴らすため1f遅らせる
        yield return new WaitForSeconds(1.0f);
        //  シーンを遷移する
        SceneManager.LoadScene(scene);
    }

}
