using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1DownMotion : MonoBehaviour
{
    [SerializeField] private float Destroytime;     //  このオブジェクトを破壊するまでの時間
    [SerializeField] private float downrotation_x;     //  オブジェクトを回転させる角度_X
    [SerializeField] private float downrotation_y;     //  オブジェクトを回転させる角度_Y
    [SerializeField] private float downrotation_z;     //  オブジェクトを回転させる角度_Z
    
    // Start is called before the first frame update
    void Start()
    {
        //  2秒後にこのオブジェクトを破壊する
        Destroy(this.gameObject, Destroytime);
    }

    // Update is called once per frame
    void Update()
    {
        //  ゆっくりと前方に倒れ込む
        this.transform.Rotate(new Vector3(downrotation_x, downrotation_y, downrotation_z) * Time.deltaTime);
    }
}
