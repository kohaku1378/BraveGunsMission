using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameOverController gameOverController;     //  ゲームオーバーを管理するスクリプトを格納
    [SerializeField] private GunController gunController;   //  銃のスクリプトを格納
    [SerializeField] private PlayerController playerController;     //  プレイヤーのスクリプトを格納
    [SerializeField] private PointofviewController pointofviewController;   //  視点移動のスクリプトを格納
    [SerializeField] private FadeManagementScript fadeManagementScript; //  フェードアウトを制御するスクリプトを格納
    [SerializeField] private GameObject PausePanel;     //  ポーズ画面を格納
    [SerializeField] private string titlescene;     //  タイトルシーン名を格納
    [SerializeField] private string thisscene;     //  現在のシーン名を格納
    public AudioClip clickSound;     //   ボタンクリックの効果音
    public AudioClip openSound;     //   ポーズを開いた時の効果音
    public AudioClip closeSound;     //   ポーズを閉じた時の効果音
    public AudioSource audioSource;     //  オーディオソースを格納
    private string scene;   //  移動先のシーンを格納

    // Update is called once per frame
    void Update()
    {
        //  Escapeキーを押したとき
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //  ポーズ画面が開かれていない時
            if(PausePanel.activeSelf == false)
            {
                //  ポーズ画面を有効にする
                PausePanel.SetActive(true);
                //　ポーズを開いた時の効果音を鳴らす
                audioSource.PlayOneShot(openSound);
                //  ゲームの各処理を停止する
                gameOverController.enemyswich = false;
                gunController.gunswich = false;
                playerController.playerswich = false;
                pointofviewController.viewswich = false;
            }
            //  ポーズ画面が開かれている時
            else if(PausePanel.activeSelf == true)
            {
                //  ポーズ画面を無効にする
                PausePanel.SetActive(false);
                //　ポーズを閉じた時の効果音を鳴らす
                audioSource.PlayOneShot(closeSound);
                //  ゲームの各処理を有効にする
                gameOverController.enemyswich = true;
                gunController.gunswich = true;
                playerController.playerswich = true;
                pointofviewController.viewswich = true;
            }
        }
    }

    public void TitleButton()
    {
        //  遷移先のシーンをtitlesceneに設定する
        scene = titlescene;
        //　ボタンクリックの効果音を鳴らす
        audioSource.PlayOneShot(clickSound);
        // フェードアウトを実行するクリプトを呼び出す
        fadeManagementScript.CallCoroutine();
        //  シーンを切り替える処理を呼び出す
        StartCoroutine("SceneChange");
    }

    public void RestartButton()
    {
        //  遷移先のシーンをthissceneに設定する
        scene = thisscene;
        //　ボタンクリックの効果音を鳴らす
        audioSource.PlayOneShot(clickSound);
        // フェードアウトを実行するクリプトを呼び出す
        fadeManagementScript.CallCoroutine();
        //  シーンを切り替える処理を呼び出す
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
