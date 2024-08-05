using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButton : MonoBehaviour
{
    [SerializeField] private string scene;     //  シーンを格納
    [SerializeField] FadeManagementScript fadeManagementScript;     //  フェードアウトを制御するスクリプトを格納
    public AudioClip ClickSound;     //   ボタンを押す効果音
    public AudioSource audioSource;     //  オーディオソースを格納

    public void ClickButton()
    {
        //　ボタンを押す効果音を鳴らす
        audioSource.PlayOneShot(ClickSound);
        // フェードアウトを実行するクリプトを呼び出す
        fadeManagementScript.CallCoroutine();
        //  シーンを遷移する処理を呼び出す
        StartCoroutine("SceneChange");
    }

    IEnumerator SceneChange()
    {
        //  効果音を鳴らすため1f遅らせる
        yield return new WaitForSeconds(1.0f);
        //  シーンを遷移する
        SceneManager.LoadScene(scene);
    }
}
