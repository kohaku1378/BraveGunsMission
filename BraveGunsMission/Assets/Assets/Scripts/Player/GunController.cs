using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GameObject Player;   //　プレイヤーの取得
    [SerializeField] private GameObject Bullet;   //　弾丸のプレハブを格納
    [SerializeField] private GameObject Launchsite;   //　弾丸の発射場所
    [SerializeField] private MagazineController magazineController;   //　マガジンのスクリプトを取得
    [SerializeField] private GameObject ReloadText;   //  リロード時のテキスト
    public float bullets = 15f;   //　残弾数
    public float magazines = 5f;   //　マガジンの数
    public bool gunswich;    //  銃の動きを制御
    private float firstbullets;   //　最大装填数 
    private bool reloadswich = false;   //　リロードのON、OFF
    public AudioClip outsound;     //   弾切れの効果音
    AudioSource audioSource;    //  オーディオソースを格納

    // Start is called before the first frame update
    void Start()
    {
        //　最大装填数を設定
        firstbullets = bullets;
        //  AudioSourceComponentを取得
        audioSource = GetComponent<AudioSource>();
        //  銃の動きを有効にする
        gunswich = true;
    }

    // Update is called once per frame
    void Update()
    {
        //  銃の動きが有効な時
        if(gunswich == true)
        {
            //  残弾数が0ではなく、リロード中ではない
            if(bullets > 0 && reloadswich == false)
            {
                //  マウスの左クリックを検知する
                if(Input.GetMouseButtonDown(0))
                {
                    //　生成位置
                    Vector3 pos = Launchsite.transform.position;
                    //　生成時の回転
                    Quaternion rot = Player.transform.rotation;
                    //　銃弾を指定位置に生成
                    Instantiate(Bullet, pos, rot);
                    //　残弾数を-1する
                    bullets -= 1f;
                }
            }
            //  残弾数が0以下で、リロード中ではない
            else if(bullets <= 0 && reloadswich == false)
            {
                //  マウスの左クリックを検知する
                if(Input.GetMouseButtonDown(0))
                {
                    //  マガジンの数が0以下ではない
                    if(magazines > 0)
                    {
                        //　リロード中はクリックしても反応しないようにする
                        reloadswich = true;
                        //　MagazineControllerのReload関数を実行
                        magazineController.Reload();
                    }
                    else
                    {
                        //弾切れの効果音を鳴らす
                        audioSource.PlayOneShot(outsound);
                    }
                }
            }
        }
    }

    public void Reload()
    {
        //　マガジンの数を-1する
        magazines -= 1f;
        //　残弾数を最大装填数に更新する
        bullets = firstbullets;
        //　クリックで銃の処理を出来るようにする
        reloadswich = false;
        //  リロードのテキストを消す
        ReloadText.SetActive(false);
    }
}
