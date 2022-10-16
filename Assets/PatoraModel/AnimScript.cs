using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour
{
    // Animator �R���|�[�l���g
    public Animator animator;

    // �ݒ肵���t���O�̖��O
    private const string key_isRun = "IsRun";
    private const string key_isWalk = "IsWalk";
    private const string key_isProne = "IsProne";


	//���f���������Ă��邩�ǂ�����Ԃ�
	public bool GetIsRun()
	{
		return animator.GetBool(key_isRun);
	}
	//���f���������Ă��邩�ǂ�����Ԃ�
	public bool GetIsWalk()
	{
		return animator.GetBool(key_isWalk);
	}
	//���f�������������Ԃ��ǂ�����Ԃ�
	public bool GetIsProne()
	{
		return animator.GetBool(key_isProne);
	}

	//�ړ��L�[�������Ă��邩�ǂ���
	private bool IsMove()
	{
		//�R���g���[���\�̈ړ�
		if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
			return true;
			//�����Ă��Ȃ��̂�false��Ԃ�
			return false;
	}


    // Update is called once per frame
    void Update()
    {
		//�����O�i���Ă��邩�ǂ���
		if(Input.GetKeyDown(KeyCode.Space) && !animator.GetBool(key_isRun) ||
		   Input.GetButtonDown("ChangeHohuku") && !animator.GetBool(key_isRun))
		{
			if(animator.GetBool(key_isProne))
			animator.SetBool(key_isProne, false);
			else
			animator.SetBool(key_isProne, true);
		}
		if (IsMove())//�����Ă��邩
		{
			//�����Ă��邩�ǂ���
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Dash"))
				animator.SetBool(key_isRun, true);//Wait or Walk ����Run�ɑJ�ڂ���
			else//�����i�ړ��j���Ă��邩�ǂ���
			{
				// Wait����Walk�ɑJ�ڂ���
				animator.SetBool(key_isWalk, true);
				//Run����Wait or Walk�ɑJ�ڂ���
				animator.SetBool(key_isRun, false);
			}
		}
		else//�����Ă��Ȃ�
		{
			// Walk����Wait�ɑJ�ڂ���
			animator.SetBool(key_isWalk, false);
			//Run����Wait�ɑJ�ڂ���
			animator.SetBool(key_isRun, false);
		}

	}
}
