using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_1_world : MonoBehaviour 
{
	[SerializeField] float step_up_down;
	[SerializeField] float step_left_right;

	[SerializeField] float time_up_down;
	[SerializeField] float time_left_right;
	[SerializeField] float time_wait;

	float time_up_down_elapsed;
	float time_left_right_elapsed;
	float time_wait_elapsed;

	behavior atual_behavior_ud;
	behavior atual_behavior_lr;

	enum behavior 
	{
		LEFT, RIGHT , UP, DOWN , WAIT_R, WAIT_L
	}

	// Use this for initialization
	void Start () 
	{
		atual_behavior_ud = behavior.DOWN;
		atual_behavior_lr = behavior.LEFT;

		time_wait_elapsed = 0;
		time_up_down_elapsed = 0;
		time_left_right_elapsed = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (atual_behavior_ud == behavior.UP) 
		{
			transform.Translate (new Vector3 (0, 1, 0) * step_up_down);
		}
		else 
		{
			transform.Translate (new Vector3 (0, -1, 0) * step_up_down);
		}

		if (atual_behavior_lr == behavior.LEFT) 
		{
			transform.Rotate (new Vector3 (0, 1, 0) * step_left_right);
		}
		if (atual_behavior_lr == behavior.RIGHT) {
			transform.Rotate (new Vector3 (0, -1, 0) * step_left_right);
		} 
		else 
		{
			transform.Rotate (new Vector3 (0, 0, 0) * step_left_right);
		}

		time_wait_elapsed += Time.deltaTime;
		time_up_down_elapsed += Time.deltaTime;
		time_left_right_elapsed += Time.deltaTime;

		if (time_up_down_elapsed >= time_up_down) 
		{
			change_ud ();
			time_up_down_elapsed = 0;
		}
		if(time_left_right_elapsed >= time_left_right)
		{
			change_lr ();
			time_left_right_elapsed = 0;
		}
		if (time_wait_elapsed >= time_wait) 
		{
			change_lr ();
			time_wait_elapsed = 0;
		}
	}

	void change_lr()
	{
		if (atual_behavior_lr == behavior.LEFT)
		{
			atual_behavior_lr = behavior.WAIT_R;
		}
		if(atual_behavior_lr == behavior.RIGHT)
		{
			atual_behavior_lr = behavior.WAIT_L;
		}
		if(atual_behavior_lr == behavior.WAIT_L)
		{
			atual_behavior_lr = behavior.LEFT;
		}
		if(atual_behavior_lr == behavior.WAIT_R)
		{
			atual_behavior_lr = behavior.RIGHT;
		}
	}

	void change_ud()
	{
		if (atual_behavior_ud == behavior.DOWN) 
		{
			atual_behavior_ud = behavior.UP;
		}
		else 
		{
			atual_behavior_ud = behavior.DOWN;
		}
	}
}
