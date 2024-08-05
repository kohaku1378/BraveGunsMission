using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointofviewController : MonoBehaviour
{
    [SerializeField] private float x_sensitivity = 3f;  //  x軸の視点の移動速度
    [SerializeField] private float y_sensitivity = 3f;  //  y軸の視点の移動速度
    public bool viewswich;   //  視点の動きを制御する
    
    void Start()
    {
        //  視点の動きを有効にする
        viewswich = true;
    }
    
    void Update()
    {
        //  視点の動きが有効な時
        if(viewswich == true)
        {
            //  マウスのx軸の入力を取得
            float x_mouse = Input.GetAxis("Mouse X");
            //  マウスのy軸の入力を取得
            float y_mouse = Input.GetAxis("Mouse Y");

            //  視点移動の処理
            Vector3 newRotation = transform.localEulerAngles;
            newRotation.y += x_mouse * x_sensitivity;
            newRotation.x += -y_mouse * y_sensitivity;
            transform.localEulerAngles = newRotation;
        }
    }
}
