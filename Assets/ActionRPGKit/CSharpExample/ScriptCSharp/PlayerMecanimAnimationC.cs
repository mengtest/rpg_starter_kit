using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AttackTriggerC))]

public class PlayerMecanimAnimationC : MonoBehaviour {
	
	private GameObject player;
	private GameObject mainModel;
	public Animator animator;
	private CharacterController controller;
	
	public string moveHorizontalState = "horizontal";
	public string moveVerticalState = "vertical";
	public string jumpState = "jump";
	public string hurtState = "hurt";
	private bool  jumping = false;
	private bool  attacking = false;
	private bool  flinch = false;
	
	void  Start (){
		if(!player){
			player = this.gameObject;
		}
		mainModel = GetComponent<AttackTriggerC>().mainModel;
		if(!mainModel){
			mainModel = this.gameObject;
		}
		if(!animator){
			animator = mainModel.GetComponent<Animator>();
		}
		controller = player.GetComponent<CharacterController>();
		GetComponent<AttackTriggerC>().useMecanim = true;
	}
	
	void  Update (){
		//Set attacking variable = isCasting in AttackTrigger
		attacking = GetComponent<AttackTriggerC>().isCasting;
		flinch = GetComponent<AttackTriggerC>().flinch;
		//Set Hurt Animation
		animator.SetBool(hurtState , flinch);
		
		if(attacking || flinch){
			return;
		}
		
       
		if ((controller.collisionFlags & CollisionFlags.Below) != 0){
			float h = Input.GetAxis("Horizontal");
			float v = Input.GetAxis("Vertical");
			animator.SetFloat(moveHorizontalState , h);  // 控制行走

            animator.SetFloat(moveVerticalState , v);
			if(jumping){
				jumping = false;
				animator.SetBool(jumpState , jumping); //取消跳跃
				//animator.StopPlayback(jumpState);
			}
			
		}else{
			jumping = true;
			animator.SetBool(jumpState , jumping); //执行跳跃
			//animator.Play(jumpState);
		}
		
	}
	
    /// <summary>
    /// 播放攻击动画时，取消跳跃状态
    /// </summary>
    /// <param name="anim"></param>
	public void  AttackAnimation ( string anim  ){
		animator.SetBool(jumpState , false);
        animator.Play(anim);
	}
	
	public void  PlayAnim ( string anim  ){
		animator.Play(anim);
		
	}
	
	public void  SetWeaponType ( int val  , string idle  ){
		animator.SetInteger("weaponType" , val);
		animator.Play(idle);
	}

		
}