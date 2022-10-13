using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class PlayerCameraManager : MonoBehaviour
{

     /// <summary>
     /// �v���C���[�̃J�����̕\����\�����Ǘ�����N���X
     /// �A�j���[�V�����̏�Ԃ��󂯎��A
     /// ����ɉ����ăJ�����̐؂�ւ�������
     /// </summary>

    //����l�̏�Ԃ�
    private bool _IsFP;

    //TP��Ԃ̃J����
    [SerializeField] CinemachineVirtualCamera _TPCamera;
    //FP��Ԃ̃J����
    [SerializeField] CinemachineVirtualCamera _WalkFPCamera;
    [SerializeField] CinemachineVirtualCamera _DashFPCamera;
    [SerializeField] CinemachineVirtualCamera _ProneFPCamera;
    [SerializeField] CinemachineVirtualCamera[] _CameraArray;

    //�A�j���\�V�����̃X�N���v�g
    [SerializeField] AnimScript _AnimScript;
    void Start()
    {
        _CameraArray = Transform.FindObjectsOfType<CinemachineVirtualCamera>();

    }
    ////////////////////////////////////////////////////////
    ///�T�v   �F ��l�̎��_�ƎO�l�̎x�X�̃J������؂�ւ���֐�
    ///����   �F �Ȃ�
    ///�Ԃ�l �F �Ȃ�
    ////////////////////////////////////////////////////////
    private void SwitchViewPointCamera()
    {
        _IsFP = !_IsFP;//�t���O�𔽑΂�

        if (_IsFP)//��l�̎��_�̎�
        {
            _TPCamera.gameObject.SetActive(false);
            _WalkFPCamera.gameObject.SetActive(true);
        }
        else//�O�l�̎��_�̎�
        {
            _TPCamera.gameObject.SetActive(true);
            _WalkFPCamera.gameObject.SetActive(false);
        }
    }
    
    //�J�����̃I���I�t��؂�ւ���֐�
    private void SwitchCameraActive() 
    {
        //�O�l�̎��_�ȊO�J�������I�t�ɂ���
        _TPCamera.gameObject.SetActive(true);
        _WalkFPCamera.gameObject.SetActive(false);
        _DashFPCamera.gameObject.SetActive(false);
        _ProneFPCamera.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FirstPerson"))//���_�̐؂�ւ�
        {
            _IsFP = !_IsFP;//�t���O�𔽑΂�
            //��l�̎��_�̎�
            if(_IsFP)
            {
               //if(_AnimScript.GetIsProne())
               
               //else if(_AnimScript.GetIsProne())

               //else if(_AnimScript.GetIsProne())
			}
            //�O�l�̎��_�̎�
            else 
            {
                //�O�l�̎��_�ȊO�J�������I�t�ɂ���
                _TPCamera.gameObject.SetActive(true);
                _WalkFPCamera.gameObject.SetActive(false);
                _DashFPCamera.gameObject.SetActive(false);
                _ProneFPCamera.gameObject.SetActive(false);
            }
        }


    }
}
