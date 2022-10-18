using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	//�O�l�̃J����
	[SerializeField] CinemachineVirtualCamera _ThirdPersonCamera;
	//�ォ��̎O�l�̃J����
	[SerializeField] CinemachineVirtualCamera _FromTopCamera;

	[SerializeField] GameObject _Player;

	private void Start()
	{
		_FromTopCamera.gameObject.SetActive(true);
		_FromTopCamera.m_Priority = 0;
	}
	private void Update()
	{
		transform.position= new (
		_Player.transform.position.x,
		_Player.transform.position.y + 18f,
		_Player.transform.position.z + -16f
		);

		//Debug.Log(_Player.transform.position.y);
	}
	//�����蔻��ɂ������Ă����
	private void OnCollisionEnter(Collision collision)
	{
		if(_FromTopCamera.m_Priority != 0 || _ThirdPersonCamera.m_Priority != 0)
		{
			//�ォ��̃J����
			_FromTopCamera.gameObject.GetComponent<CinemachineVirtualCamera>().m_Priority = 10;
			//�O�l�̃J����
			_ThirdPersonCamera.gameObject.GetComponent<CinemachineVirtualCamera>().m_Priority = 0;
			Debug.Log("�������Ă���");
		}
	}
	//�����蔻�肩�痣�ꂽ�Ƃ�
	private void OnCollisionExit(Collision collision)
	{
		if (_FromTopCamera.m_Priority != 0 || _ThirdPersonCamera.m_Priority != 0)
		{
			//�ォ��̃J����
			_FromTopCamera.gameObject.GetComponent<CinemachineVirtualCamera>().m_Priority = 0;
			//�O�l�̃J����
			_ThirdPersonCamera.gameObject.GetComponent<CinemachineVirtualCamera>().m_Priority = 10;
			Debug.Log("����Ă���");
		}
	}
}
