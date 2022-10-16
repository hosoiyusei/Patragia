using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour
{
    //変数
    //回転量
    [SerializeField] float _RotateSpeed = 3f;
    //移動速度
    [SerializeField] float _Speed = 20f;
    //重力
    [SerializeField] float _Gravity = 1000f;

    private Vector3 _MoveDirection = Vector3.zero;
    [SerializeField] CharacterController _Controller;
    //アニメ―ションのスクリプト
    [SerializeField] AnimScript _AnimScript;
    //カメラのマネージャー
    [SerializeField] PlayerCameraManager _CameraManager;
    //当たり判定の関数
    protected void OnCollisionStay(Collision collision)
    {       
        //オブジェクトの抗力
        GetComponent<Rigidbody>().drag = 3;
    }

    protected void OnCollisionExit(Collision collision)
    {
        //オブジェクトの抗力
        GetComponent<Rigidbody>().drag = 0;
    }

    ////////////////////////////////////////////////////////
    ///概要   ： プレイヤーの共通の移動
    ///引数   ： なし
    ///返り値 ： なし
    ////////////////////////////////////////////////////////
    private void MovePlayer()
    {
        _MoveDirection = transform.TransformDirection(_MoveDirection);
        //走っているなら　＋　這いずり状態じゃないなら
        if (_AnimScript.GetIsRun() && !_AnimScript.GetIsProne())
            _MoveDirection *= (_Speed * 3.0f);
        //這いずり状態なら
        else if (_AnimScript.GetIsProne())
            _MoveDirection *= (_Speed * 0.5f);
        //歩行状態なら(それ以外)
        else
            _MoveDirection *= _Speed;
    }
    ////////////////////////////////////////////////////////
    ///概要   ： 一人称視点のプレイヤーの動き
    ///引数   ： なし
    ///返り値 ： なし
    ////////////////////////////////////////////////////////  
    private void FirstPersonPlayerMove()
    {
        _MoveDirection = new(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();//プレイヤーの動き

        //プレイヤーの回転処理
        float x = _RotateSpeed * Gamepad.current.rightStick.ReadValue().x;
        transform.Rotate(0, x, 0);
    }
    ////////////////////////////////////////////////////////
    ///概要   ： 三人称視点のプレイヤーの動き
    ///引数   ： なし
    ///返り値 ： なし
    ////////////////////////////////////////////////////////
    private void ThirdPersonPlayerMove()
    {
        //プレイヤーが動いているなら
        if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
        {
            //見ている方向に進むように移動方向を設定する
            _MoveDirection = Vector3.forward;
            MovePlayer();//プレイヤーの動き
                         //向いている方向を取得
            Vector3 targetDirection = new(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            //視点が動いているときだけ
            if (targetDirection.magnitude > 0.1)
            {
                transform.localRotation = Quaternion.LookRotation(targetDirection);
            }

        }
    }

    ////////////////////////////////////////////////////////
    ///概要   ： 更新
    ///引数   ： なし
    ///返り値 ： なし
    //////////////////////////////////////////////////////// 
    private void Update()
	{
        if (_Controller.isGrounded)//床の上にいるなら
        {
            _Gravity = 1000;//重力を設定する
            _MoveDirection = Vector3.zero;//移動量を０にする

			//一人称視点なら
			if (_CameraManager.GetIsFirstPerson())
				FirstPersonPlayerMove();
			//三人称視点なら
			else
				ThirdPersonPlayerMove();
		}
        else//プレイヤーが空中にいるとき
            _Gravity = 250;//重力を設定する
		
        //重力
        _MoveDirection.y -= (_Gravity * Time.deltaTime);
        //プレイヤーの動き
        _Controller.Move(_MoveDirection * Time.deltaTime);

    }
}
