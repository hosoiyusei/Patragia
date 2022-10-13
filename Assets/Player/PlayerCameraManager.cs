using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class PlayerCameraManager : MonoBehaviour
{

     /// <summary>
     /// プレイヤーのカメラの表示非表示を管理するクラス
     /// アニメーションの状態を受け取り、
     /// それに応じてカメラの切り替えをする
     /// </summary>

    //今一人称状態か
    private bool _IsFP;

    //TP状態のカメラ
    [SerializeField] CinemachineVirtualCamera _TPCamera;
    //FP状態のカメラ
    [SerializeField] CinemachineVirtualCamera _WalkFPCamera;
    [SerializeField] CinemachineVirtualCamera _DashFPCamera;
    [SerializeField] CinemachineVirtualCamera _ProneFPCamera;
    [SerializeField] CinemachineVirtualCamera[] _CameraArray;

    //アニメ―ションのスクリプト
    [SerializeField] AnimScript _AnimScript;
    void Start()
    {
        _CameraArray = Transform.FindObjectsOfType<CinemachineVirtualCamera>();

    }
    ////////////////////////////////////////////////////////
    ///概要   ： 一人称視点と三人称支店のカメラを切り替える関数
    ///引数   ： なし
    ///返り値 ： なし
    ////////////////////////////////////////////////////////
    private void SwitchViewPointCamera()
    {
        _IsFP = !_IsFP;//フラグを反対に

        if (_IsFP)//一人称視点の時
        {
            _TPCamera.gameObject.SetActive(false);
            _WalkFPCamera.gameObject.SetActive(true);
        }
        else//三人称視点の時
        {
            _TPCamera.gameObject.SetActive(true);
            _WalkFPCamera.gameObject.SetActive(false);
        }
    }
    
    //カメラのオンオフを切り替える関数
    private void SwitchCameraActive() 
    {
        //三人称視点以外カメラをオフにする
        _TPCamera.gameObject.SetActive(true);
        _WalkFPCamera.gameObject.SetActive(false);
        _DashFPCamera.gameObject.SetActive(false);
        _ProneFPCamera.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FirstPerson"))//視点の切り替え
        {
            _IsFP = !_IsFP;//フラグを反対に
            //一人称視点の時
            if(_IsFP)
            {
               //if(_AnimScript.GetIsProne())
               
               //else if(_AnimScript.GetIsProne())

               //else if(_AnimScript.GetIsProne())
			}
            //三人称視点の時
            else 
            {
                //三人称視点以外カメラをオフにする
                _TPCamera.gameObject.SetActive(true);
                _WalkFPCamera.gameObject.SetActive(false);
                _DashFPCamera.gameObject.SetActive(false);
                _ProneFPCamera.gameObject.SetActive(false);
            }
        }


    }
}
