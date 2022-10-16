using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


public class PlayerCameraManager : MonoBehaviour
{

	enum CameraType
	{
        ThirdPersonCamera ,     //�O�l�̎��_
        WalkFirstPersonCamera, //��l�̎��_�ŕ���
        RunFirstPersonCamera, //��l�̎��_�ő���
        ProneFirstPersonCamera,//��l�̎��_�ř���
    }

     /// <summary>
     /// �v���C���[�̃J�����̕\����\�����Ǘ�����N���X
     /// �A�j���[�V�����̏�Ԃ��󂯎��A
     /// ����ɉ����ăJ�����̐؂�ւ�������
     /// </summary>

    //����l�̏�Ԃ�
    private bool _IsFP;
    //�J�����z��
    private CinemachineVirtualCamera[] _CameraArray;
    //�A�j���\�V�����̃X�N���v�g
    [SerializeField] AnimScript _AnimScript;

    ////////////////////////////////////////////////////////
    ///�T�v   �F ��l�̎��_���ǂ������擾����֐�
    ///����   �F �Ȃ�
    ///�Ԃ�l �F  ��l�̎��_���ǂ���(bool�^)
    //////////////////////////////////////////////////////// 
    public bool GetIsFirstPerson()
    {
        return _IsFP;
    }

    ////////////////////////////////////////////////////////
    ///�T�v   �F �����̃J�����^�C�v�ȊO���\���ɂ���֐�
    ///����   �F �J�����̃^�C�v(CameraType�^)
    ///�Ԃ�l �F �Ȃ�
    //////////////////////////////////////////////////////// 
    private void SwitchCameraActive(CameraType cameraType)
    {

        //�����̃J�����^�C�v�ȊO���\���ɂ���
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
        //�o�[�`�����J�������擾����
        _CameraArray = Transform.FindObjectsOfType<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("FirstPerson"))//���_�̐؂�ւ�
        {
            _IsFP = !_IsFP;//�t���O�𔽑΂�
        }
            //��l�̎��_�̎�
            if (_IsFP)
            {
                //����
                if (_AnimScript.GetIsProne())
                {
                    Debug.Log(_AnimScript.GetIsProne());
                    SwitchCameraActive(CameraType.ProneFirstPersonCamera);//�J������؂�ւ���
				}
                //����
                else if (_AnimScript.GetIsRun())
                    SwitchCameraActive(CameraType.RunFirstPersonCamera);//�J������؂�ւ���
                //��L�ȊO
                else
                    SwitchCameraActive(CameraType.WalkFirstPersonCamera);//�J������؂�ւ���

            }
            //�O�l�̎��_�̎�
            else
                SwitchCameraActive(CameraType.ThirdPersonCamera);//�J������؂�ւ���
        }
    
}
