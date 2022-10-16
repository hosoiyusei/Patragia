using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimScript : MonoBehaviour
{
    // Animator コンポーネント
    public Animator animator;

    // 設定したフラグの名前
    private const string key_isRun = "IsRun";
    private const string key_isWalk = "IsWalk";
    private const string key_isProne = "IsProne";


	//モデルが走っているかどうかを返す
	public bool GetIsRun()
	{
		return animator.GetBool(key_isRun);
	}
	//モデルが歩いているかどうかを返す
	public bool GetIsWalk()
	{
		return animator.GetBool(key_isWalk);
	}
	//モデルが這いずり状態かどうかを返す
	public bool GetIsProne()
	{
		return animator.GetBool(key_isProne);
	}

	//移動キーを押しているかどうか
	private bool IsMove()
	{
		//コントローラ―の移動
		if (Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f)
			return true;
			//押していないのでfalseを返す
			return false;
	}


    // Update is called once per frame
    void Update()
    {
		//匍匐前進しているかどうか
		if(Input.GetKeyDown(KeyCode.Space) && !animator.GetBool(key_isRun) ||
		   Input.GetButtonDown("ChangeHohuku") && !animator.GetBool(key_isRun))
		{
			if(animator.GetBool(key_isProne))
			animator.SetBool(key_isProne, false);
			else
			animator.SetBool(key_isProne, true);
		}
		if (IsMove())//動いているか
		{
			//走っているかどうか
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("Dash"))
				animator.SetBool(key_isRun, true);//Wait or Walk からRunに遷移する
			else//歩く（移動）しているかどうか
			{
				// WaitからWalkに遷移する
				animator.SetBool(key_isWalk, true);
				//RunからWait or Walkに遷移する
				animator.SetBool(key_isRun, false);
			}
		}
		else//動いていない
		{
			// WalkからWaitに遷移する
			animator.SetBool(key_isWalk, false);
			//RunからWaitに遷移する
			animator.SetBool(key_isRun, false);
		}

	}
}
