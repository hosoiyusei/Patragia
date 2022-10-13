using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class Player : MonoBehaviour
{
    //�ϐ�
    //��]��
    [SerializeField] float _RotateSpeed = 3f;
    //�ړ����x
    [SerializeField] float _Speed = 20f;
    //�d��
    [SerializeField] float _Gravity = 1000f;

    private Vector3 _MoveDirection = Vector3.zero;
    [SerializeField] CharacterController _Controller;
    //�A�j���\�V�����̃X�N���v�g
    [SerializeField] AnimScript _AnimScript;

    //����l�̏�Ԃ�
    private bool _IsFP;

    //TP��Ԃ̃J����
    [SerializeField] CinemachineVirtualCamera _TPCamera;
    //FP��Ԃ̃J����
    [SerializeField] CinemachineVirtualCamera _WalkFPCamera;
    [SerializeField] CinemachineVirtualCamera _DashFPCamera;
    [SerializeField] CinemachineVirtualCamera _ProneFPCamera;

    //�����蔻��̊֐�
    protected void OnCollisionStay(Collision collision)
    {       
        //�I�u�W�F�N�g�̍R��
        GetComponent<Rigidbody>().drag = 3;
    }

    protected void OnCollisionExit(Collision collision)
    {
        //�I�u�W�F�N�g�̍R��
        GetComponent<Rigidbody>().drag = 0;
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
    ////////////////////////////////////////////////////////
    ///�T�v   �F �v���C���[�̋��ʂ̈ړ�
    ///����   �F �Ȃ�
    ///�Ԃ�l �F �Ȃ�
    ////////////////////////////////////////////////////////
    private void MovePlayer()
    {
        _MoveDirection = transform.TransformDirection(_MoveDirection);
        //�����Ă���Ȃ�@�{�@���������Ԃ���Ȃ��Ȃ�
        if (_AnimScript.GetIsRun() && !_AnimScript.GetIsProne())
            _MoveDirection *= (_Speed * 3.0f);
        //���������ԂȂ�
        else if (_AnimScript.GetIsProne())
            _MoveDirection *= (_Speed * 0.5f);
        //���s��ԂȂ�(����ȊO)
        else
            _MoveDirection *= _Speed;
    }
    ////////////////////////////////////////////////////////
    ///�T�v   �F ��l�̎��_�̃v���C���[�̓���
    ///����   �F �Ȃ�
    ///�Ԃ�l �F �Ȃ�
    ////////////////////////////////////////////////////////  
    private void FirstPersonPlayerMove()
    {
        _MoveDirection = new(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        MovePlayer();//�v���C���[�̓���

        //�v���C���[�̉�]����
        float x = _RotateSpeed * Gamepad.current.rightStick.ReadValue().x;
        transform.Rotate(0, x, 0);
    }
    ////////////////////////////////////////////////////////
    ///�T�v   �F �O�l�̎��_�̃v���C���[�̓���
    ///����   �F �Ȃ�
    ///�Ԃ�l �F �Ȃ�
    ////////////////////////////////////////////////////////
    private void ThirdPersonPlayerMove()
    {
        //�v���C���[�������Ă���Ȃ�
        if (Input.GetAxis("Vertical") != 0f || Input.GetAxis("Horizontal") != 0f)
        {
            //���Ă�������ɐi�ނ悤�Ɉړ�������ݒ肷��
            _MoveDirection = Vector3.forward;
            MovePlayer();//�v���C���[�̓���
                         //�����Ă���������擾
            Vector3 targetDirection = new(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            //���_�������Ă���Ƃ�����
            if (targetDirection.magnitude > 0.1)
            {
                transform.localRotation = Quaternion.LookRotation(targetDirection);
            }

        }
    }

    ////////////////////////////////////////////////////////
    ///�T�v   �F �X�V
    ///����   �F �Ȃ�
    ///�Ԃ�l �F �Ȃ�
    //////////////////////////////////////////////////////// 
    private void Update()
	{
        if (Input.GetButtonDown("FirstPerson"))//���_�̐؂�ւ�        
            SwitchViewPointCamera();//�J������؂�ւ���

        if (_Controller.isGrounded)//���̏�ɂ���Ȃ�
        {
            _Gravity = 1000;//�d�͂�ݒ肷��
            _MoveDirection = Vector3.zero;//�ړ��ʂ��O�ɂ���

            //��l�̎��_�Ȃ�
            if (_IsFP)
                FirstPersonPlayerMove();
            //�O�l�̎��_�Ȃ�
            else
                ThirdPersonPlayerMove();
        }
        else//�v���C���[���󒆂ɂ���Ƃ�
            _Gravity = 250;//�d�͂�ݒ肷��
		
        //�d��
        _MoveDirection.y -= (_Gravity * Time.deltaTime);
        //�v���C���[�̓���
        _Controller.Move(_MoveDirection * Time.deltaTime);

    }
}
