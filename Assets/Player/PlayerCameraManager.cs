using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class PlayerCameraManager : MonoBehaviour
{

	enum CameraType
	{
        ThirdPersonCamera ,     //三人称視点
        WalkFirstPersonCamera, //一人称視点で歩く
        RunFirstPersonCamera, //一人称視点で走る
        ProneFirstPersonCamera,//一人称視点で匍匐
    }

     /// <summary>
     /// プレイヤーのカメラの表示非表示を管理するクラス
     /// アニメーションの状態を受け取り、
     /// それに応じてカメラの切り替えをする
     /// </summary>

    //今一人称状態か
    private bool _IsFP;
    //カメラ配列
    private CinemachineVirtualCamera[] _CameraArray;
    //アニメ―ションのスクリプト
    [SerializeField] AnimScript _AnimScript;

    ////////////////////////////////////////////////////////
    ///概要   ： 一人称視点かどうかを取得する関数
    ///引数   ： なし
    ///返り値 ：  一人称視点かどうか(bool型)
    //////////////////////////////////////////////////////// 
    public bool GetIsFirstPerson()
    {
        return _IsFP;
    }

    ////////////////////////////////////////////////////////
    ///概要   ： 引数のカメラタイプ以外を非表示にする関数
    ///引数   ： カメラのタイプ(CameraType型)
    ///返り値 ： なし
    //////////////////////////////////////////////////////// 
    private void SwitchCameraActive(CameraType cameraType)
    {

        //引数のカメラタイプ以外を非表示にする
        foreach (var item in _CameraArray)
		{
            if (item.name == cameraType.ToString())
                item.gameObject.SetActive(true);
            else
                item.gameObject.SetActive(false);

        }
    }

    void Start()
    {
        //バーチャルカメラを取得する
        _CameraArray = Transform.FindObjectsOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FirstPerson"))//視点の切り替え
        {
            _IsFP = !_IsFP;//フラグを反対に
        }
            //一人称視点の時
            if (_IsFP)
            {
                //匍匐
                if (_AnimScript.GetIsProne())
                {
                    Debug.Log(_AnimScript.GetIsProne());
                    SwitchCameraActive(CameraType.ProneFirstPersonCamera);//カメラを切り替える
				}
                //走る
                else if (_AnimScript.GetIsRun())
                    SwitchCameraActive(CameraType.RunFirstPersonCamera);//カメラを切り替える
                //上記以外
                else
                    SwitchCameraActive(CameraType.WalkFirstPersonCamera);//カメラを切り替える

            }
            //三人称視点の時
            else
                SwitchCameraActive(CameraType.ThirdPersonCamera);//カメラを切り替える
        }
    
}
