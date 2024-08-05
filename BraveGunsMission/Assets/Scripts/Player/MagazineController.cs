using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagazineController : MonoBehaviour
{
    [SerializeField] private GunController gunController;   //　銃のスクリプトを取得
    [SerializeField] private GameObject ReloadText;   //  リロード時のテキスト
    private Animator anim;  //　Animatorを定義
    public AudioClip reloadout;     //   マガジンを外す効果音
    public AudioClip reloadin;     //   マガジンをつける効果音
    AudioSource audioSource;    //  オーディオソースを格納
    
    // Start is called before the first frame update
    void Start()
    {
        //　変数animに、Animatorコンポーネントを設定する
        anim = gameObject.GetComponent<Animator>();
        //AudioSourceComponentを取得
        audioSource = GetComponent<AudioSource>();
    }

    public void Reload()
    {
        //　リロードのアニメーションを実行
        anim.SetBool("blReload", true);
        //  リロードのテキストを消す
        ReloadText.SetActive(true);
        //  RealoadSound関数を0.5秒後に呼び出す。
        Invoke(nameof(ReloadSound), 0.5f);
        //  RealoadSound関数を3秒後に呼び出す。
        Invoke(nameof(OnAnimationEnd), 3f);
    }

    public void ReloadSound()
    {
        //　マガジンを外す効果音を鳴らす
        audioSource.PlayOneShot(reloadout);
    }

    public void OnAnimationEnd()
    {
        //　マガジンをつける効果音を鳴らす
        audioSource.PlayOneShot(reloadin);
        //　blReloadをfalseに切り替える
        anim.SetBool("blReload", false);
        //  gunControllerのReload関数を実行
        gunController.Reload();
    }

}
