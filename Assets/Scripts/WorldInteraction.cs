	using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WorldInteraction : MonoBehaviour 
{	
	[SerializeField] GameObject camera_ISO;
	[SerializeField] GameObject camera_3;
	[SerializeField] GameObject camera_1;
	[SerializeField] Animator anim_Control_player;
	[SerializeField] Animator anim_Control_camera_sprite;
	[SerializeField] Animator anim_Control_character_sprite;
	[SerializeField] Animator anim_Control_camera_panel;
	[SerializeField] GameObject panel_camera_change;
	[SerializeField] GameObject fire_l_hand;
	[SerializeField] GameObject fire_r_hand;
	[SerializeField] GameObject fire_l_foot;
	[SerializeField] GameObject fire_r_foot;
	[SerializeField] Animator load_controller_e;
	[SerializeField] Animator load_controller_auto;
	[SerializeField] float move_speed;
	[SerializeField] float spin_speed;

	float time_res_camera = 2f;
	float time_res_camera_elapsed;

	[SerializeField] float time_res_atk;
	float time_res_atk_elapsed;

	[SerializeField] float time_res_e;
	float time_res_e_elapsed;


	bool can_change_camera;
	bool can_atk;
	bool buff_fire;
	bool can_buff_fire;
	private cameras atual_camera;

	enum cameras
	{
		ISO, T3, T1
	};

	void Start()
	{
		buff_fire = false;
		fire_l_hand.SetActive (false);
		fire_l_foot.SetActive (false);
		fire_r_hand.SetActive (false);
		fire_r_foot.SetActive (false);

		anim_Control_camera_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 0);
		anim_Control_character_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 0);

		can_change_camera = true;
		can_atk = true;
		can_buff_fire = true;

		time_res_camera_elapsed = 0;
		time_res_atk_elapsed = 0;
		time_res_e_elapsed = 0;
	
		anim_Control_player = GetComponent<Animator> ();
		atual_camera = cameras.ISO;

		camera_ISO.SetActive (true);

		camera_3.SetActive (false);
		camera_1.SetActive (false);

		//camera_3.SetActive (true);
		//camera_1.SetActive (true);

	}

	void Update()
	{
		camera_change ();
		move_controller ();
		atk_controller ();
		skill_controller ();

	}

	void skill_controller()
	{
		if(Input.GetButtonDown("skill_1") && can_buff_fire)
		{
			if(buff_fire)
			{
				disable_buff_fire();
			}
			else
			{
				enable_buff_fire();
			}
		}

		if (!can_buff_fire) 
		{
			time_res_e_elapsed += Time.deltaTime;
			if (time_res_e_elapsed >= time_res_e) 
			{
				can_buff_fire = true;
				time_res_e_elapsed = 0;
			}
		}
	}

	void disable_buff_fire()
	{
		buff_fire = false;
		fire_l_hand.SetActive (false);
		fire_l_foot.SetActive (false);
		fire_r_hand.SetActive (false);
		fire_r_foot.SetActive (false);

		can_buff_fire = false;
		load_controller_e.SetTrigger ("active");
		time_res_e_elapsed = 0;
	}
	
	void enable_buff_fire()
	{
		buff_fire = true;
		fire_l_hand.SetActive (true);
		fire_l_foot.SetActive (true);
		fire_r_hand.SetActive (true);
		fire_r_foot.SetActive (true);
	}

	void atk_controller()
	{
		if (Input.GetButtonDown ("atk") && can_atk) 
		{
			anim_Control_player.SetTrigger ("Attack1Trigger");
			can_atk = false;
			load_controller_auto.SetTrigger ("active");
			time_res_atk_elapsed = 0;
		}
		if (!can_atk) 
		{
			time_res_atk_elapsed += Time.deltaTime;
			if (time_res_atk_elapsed >= time_res_atk) 
			{
				can_atk = true;
				time_res_atk_elapsed = 0;
			}
		}
	}

	void move_controller()
	{
		if(Input.GetAxisRaw("Horizontal") != 0)
		{
			if (Input.GetAxisRaw ("Horizontal") > 0) 
			{
				transform.Rotate (new Vector3 (0, 1, 0) * spin_speed * Time.deltaTime);
			} 
			else 
			{
				transform.Rotate (new Vector3 (0, -1, 0) * spin_speed * Time.deltaTime);
			}
		}
		if (Input.GetAxisRaw ("Vertical") > 0) 
		{
			anim_Control_player.SetBool ("Moving", true);
			transform.Translate (new Vector3 (1, 0, 0) * move_speed * Time.deltaTime);
		} 
		else 
		{
			anim_Control_player.SetBool ("Moving", false);
		}

	}

	void camera_change()
	{
		if(Input.GetButtonDown("ChangeCamera") && can_change_camera)
		{
			if (atual_camera == cameras.ISO) 
			{				
				atual_camera = cameras.T3;
				camera_3.SetActive (true);

				camera_ISO.SetActive (false);
				camera_1.SetActive (false);


				anim_Control_camera_panel.SetTrigger ("change");
				anim_Control_camera_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 255);
				anim_Control_character_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 255);
				anim_Control_camera_sprite.SetTrigger ("change");
				Invoke ("offPanel", 2f);

				can_change_camera = false;
				time_res_camera_elapsed = 0;


			} 
			else if (atual_camera == cameras.T3) 
			{
				atual_camera = cameras.T1;

				camera_1.SetActive (true);

				camera_ISO.SetActive (false);
				camera_3.SetActive (false);

				anim_Control_camera_panel.SetTrigger ("change");
				anim_Control_camera_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 255);
				anim_Control_character_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 255);
				anim_Control_camera_sprite.SetTrigger ("change");
				anim_Control_character_sprite.SetTrigger ("change");
				Invoke ("offPanel", 2f);

				can_change_camera = false;
				time_res_camera_elapsed = 0;
			} 
			else 
			{
				atual_camera = cameras.ISO;

				camera_ISO.SetActive (true);

				camera_3.SetActive (false);
				camera_1.SetActive (false);

				anim_Control_camera_panel.SetTrigger ("change");
				anim_Control_camera_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 255);
				anim_Control_character_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 255);
				anim_Control_camera_sprite.SetTrigger ("change");
				anim_Control_character_sprite.SetTrigger ("change");
				Invoke ("offPanel", 2f);

				can_change_camera = false;
				time_res_camera_elapsed = 0;
			}
		}	
		if (!can_change_camera) 
		{
			time_res_camera_elapsed += Time.deltaTime;
			if (time_res_camera_elapsed >= time_res_camera) 
			{
				can_change_camera = true;
				time_res_camera_elapsed = 0;
			}

		}
	}

	void offPanel()
	{
		anim_Control_camera_panel.SetTrigger ("change");
		anim_Control_camera_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 0);
		anim_Control_character_sprite.gameObject.GetComponent<Image> ().color = new Color (255, 255, 255, 0);
	}

	
		
}
