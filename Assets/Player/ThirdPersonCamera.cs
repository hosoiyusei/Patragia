using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
	//三人称カメラ
	[SerializeField] CinemachineVirtualCamera _ThirdPersonCamera;
	//上からの三人称カメラ
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
	//当たり判定にあたっている間
	private void OnCollisionEnter(Collision collision)
	{
		if(_FromTopCamera.m_Priority != 0 || _ThirdPersonCamera.m_Priority != 0)
		{
			//上からのカメラ
			_FromTopCamera.gameObject.GetComponent<CinemachineVirtualCamera>().m_Priority = 10;
			//三人称カメラ
			_ThirdPersonCamera.gameObject.GetComponent<CinemachineVirtualCamera>().m_Priority = 0;
			Debug.Log("当たっている");
		}
	}
	//当たり判定から離れたとき
	private void OnCollisionExit(Collision collision)
	{
		if (_FromTopCamera.m_Priority != 0 || _ThirdPersonCamera.m_Priority != 0)
		{
			//上からのカメラ
			_FromTopCamera.gameObject.GetComponent<CinemachineVirtualCamera>().m_Priority = 0;
			//三人称カメラ
			_ThirdPersonCamera.gameObject.GetComponent<CinemachineVirtualCamera>().m_Priority = 10;
			Debug.Log("離れている");
		}
	}
}
