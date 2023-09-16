// using UnityEngine;
// using System.Collections;

// public class moveAxis : MonoBehaviour
// {

// 	float _velocidade;
// 	float _girar;

// 	Rigidbody rigidbody;
// 	Animator animator;
// 	public float m_thrust = 20f;

// 	void Start()
// 	{
// 		_velocidade = 20.0F;
// 		_girar = 60.0F;
// 		rigidbody = GetComponent<Rigidbody>();
// 		animator = GetComponent<Animator>();
// 	}

// 	// Update is called once per frame
// 	void Update()
// 	{
// 		// float translate = (Input.GetAxis("Vertical") * _velocidade) * Time.deltaTime;
// 		// float rotate = (Input.GetAxis("Horizontal") * _girar) * Time.deltaTime;

// 		// transform.Translate(0, 0, translate);
// 		// transform.Rotate(0, rotate, 0);
// 		if (Input.GetButton("Jump"))
// 		{
// 			rigidbody.AddForce(transform.up * m_thrust);
// 			animator.SetFloat("Velocidade", 10);
// 		}
// 		if (Input.GetButton("Horizontal"))
// 		{
// 			rigidbody.AddForce(transform.forward * m_thrust);
// 			animator.SetFloat("Velocidade", 10);
// 		}
// 	}
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
	public float speed = 25.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;

	private float turner;
	public float sensitivity = 5;
	private Vector3 moveDirection = Vector3.zero;

	Animator animator;

	// Use this for initialization
	void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		CharacterController controller = GetComponent<CharacterController>();
		animator.SetFloat("Velocidade", moveDirection.z);
		// is the controller on the ground?
		if (controller.isGrounded)
		{
			//Feed moveDirection with input.
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			//Multiply it by speed.
			moveDirection *= speed;
			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}
		turner = Input.GetAxis("Mouse X") * sensitivity;
		if (turner != 0)
		{
			//Code for action on mouse moving right
			transform.eulerAngles += new Vector3(0, turner, 0);
		}
		//Applying gravity to the controller
		moveDirection.y -= gravity * Time.deltaTime;
		//Making the character move
		controller.Move(moveDirection * Time.deltaTime);
	}
}