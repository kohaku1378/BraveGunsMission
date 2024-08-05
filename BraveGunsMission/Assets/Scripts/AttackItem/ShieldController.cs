using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldController : MonoBehaviour
{
    [SerializeField] private GameObject maincamera;     //    カメラを格納
    public float ShieldHP;  //  盾のHP
    public AudioClip shieldsound;    //  盾を攻撃した時の効果音
    public AudioClip destroysound;    //  盾を壊した時の効果音
    public AudioSource audioSource;     //  オーディオソースを格納

    // Update is called once per frame
    void Update()
    {
        if(ShieldHP <= 0)
        {
            //  撃破の効果音を鳴らす
            AudioSource.PlayClipAtPoint(destroysound, maincamera.transform.position);
            //　このオブジェクトを破壊する
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "bullet")
        {
            //  弾が盾に当たった効果音を鳴らす
            audioSource.PlayOneShot(shieldsound);
            //  盾のHPを1減らす
            ShieldHP -= 1f;
        }
    }
}
