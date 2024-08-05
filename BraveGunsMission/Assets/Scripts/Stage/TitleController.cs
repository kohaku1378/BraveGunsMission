using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleController : MonoBehaviour
{
    [SerializeField] private string tyutorialscene;     //  チュートリアルシーン名を格納
    [SerializeField] private string stage1_1;     //  ステージ1_1を格納
    [SerializeField] private string stage1_2;     //  ステージ1_2を格納
    [SerializeField] private string stage1_3;     //  ステージ1_3を格納
    [SerializeField] FadeManagementScript fadeManagementScript; //  フェードアウトを制御するスクリプトを格納
    public AudioClip clickSound;     //   ボタンクリックの効果音
    public AudioSource audioSource;     //  オーディオソースを格納
    private int RandomStage;    //  ランダムにステージを選択する
    private string scene;   //  移動先のシーンを格納

    public void TyutorialButton()
    {
        //  遷移先のシーンをtyutorialsceneに設定する
        scene = tyutorialscene;
        //　ボタンクリックの効果音を鳴らす
        audioSource.PlayOneShot(clickSound);
        // フェードアウトを実行するクリプトを呼び出す
        fadeManagementScript.CallCoroutine();
        //  シーンを切り替える処理を呼び出す
        StartCoroutine("SceneChange");
    }

    public void FirstStageButton()
    {
        //  1～100のランダムな値をRandomStageに格納する
            RandomStage = Random.Range(1, 100);
            //  RadomStageの値が33以下の場合
            if(RandomStage <= 33)
            {
                //  遷移先のシーンをstage1_1に設定する
                scene = stage1_1;
            }
            //  RadomStageの値が66以下の場合
            else if(RandomStage <= 66)
            {
                //  遷移先のシーンをstage1_2に設定する
                scene = stage1_2;
            }
            //  RadomStageの値がそれ以外の場合
            else
            {
                //  遷移先のシーンをstage1_3に設定する
                scene = stage1_3;
            }
            //　ボタンクリックの効果音を鳴らす
            audioSource.PlayOneShot(clickSound);
            // フェードアウトを実行するクリプトを呼び出す
            fadeManagementScript.CallCoroutine();
            //  シーンを切り替える処理を呼び出す
            StartCoroutine("SceneChange");
    }

    public void GameExitButton()
    {
        //　ボタンクリックの効果音を鳴らす
        audioSource.PlayOneShot(clickSound);
        //  ゲームを終了する処理を呼び出す
        StartCoroutine("GameQuit");
    }

    IEnumerator SceneChange()
    {
        //  効果音を鳴らすため1f遅らせる
        yield return new WaitForSeconds(1.0f);
        //  シーンを遷移する
        SceneManager.LoadScene(scene);
    }

    IEnumerator GameQuit()
    {
        //  効果音を鳴らすため1f遅らせる
        yield return new WaitForSeconds(1.0f);
        //  ゲームを終了する
        Application.Quit();
    }
}
