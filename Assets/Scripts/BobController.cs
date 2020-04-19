using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobController : MonoBehaviour {

	public float wallCheckRayLength;
	public float maxWalkSpeed;

	public float groundCheckRayLength;
	public float jumpVelocity;
	public float jumpSuccessCheckDeltaY;

	public GameObject bobModel;

	private BobStates state;
	private BobStates lastState;
	private Rigidbody myRigidBody;
	private Animator  myAnim;

	private Vector3 spawnPoint;

	private float walkRayHightFromGround = 0.25f;

	// Jumping variables
	private bool isJumping = false;
	private float jumpStartY;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		myAnim = GetComponent<Animator> ();
		state = BobStates.IDLE;

		spawnPoint = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		switch(state){
		case BobStates.WALK_FORWARD:
			WalkForward ();
			break;
		case BobStates.WALK_BACKWARDS:
			WalkBackwards ();
			break;
		case BobStates.JUMPING:
			Jumping ();
			break;
		}
	}

	void WalkForward(){
		Vector3 newVelocity = myRigidBody.velocity;
		if (newVelocity.z < maxWalkSpeed)
			newVelocity.z = maxWalkSpeed;
		myRigidBody.velocity = newVelocity;

		bobModel.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);

		RaycastHit hit;
		if (Physics.Raycast (transform.position + Vector3.up * walkRayHightFromGround, transform.TransformDirection (Vector3.forward), out hit, wallCheckRayLength)) {
			Debug.DrawRay (transform.position + Vector3.up * walkRayHightFromGround, transform.TransformDirection (Vector3.forward) * hit.distance, Color.red);
			ChangeState (BobStates.JUMPING);
		} else {
			Debug.DrawRay(transform.position+Vector3.up * walkRayHightFromGround, transform.TransformDirection(Vector3.forward) * wallCheckRayLength, Color.green);
		}
	}

	void WalkBackwards(){
		Vector3 newVelocity = myRigidBody.velocity;
		if (newVelocity.z > -maxWalkSpeed)
			newVelocity.z = -maxWalkSpeed;
		myRigidBody.velocity = newVelocity;

		bobModel.transform.localScale = new Vector3 (1.0f, 1.0f, -1.0f);

		RaycastHit hit;
		if (Physics.Raycast(transform.position+Vector3.up * walkRayHightFromGround, transform.TransformDirection(Vector3.back), out hit, wallCheckRayLength))
		{
			Debug.DrawRay(transform.position+Vector3.up * walkRayHightFromGround, transform.TransformDirection(Vector3.back) * hit.distance, Color.red);
			ChangeState (BobStates.JUMPING);
		}else {
			Debug.DrawRay(transform.position+Vector3.up * walkRayHightFromGround, transform.TransformDirection(Vector3.back) * wallCheckRayLength, Color.green);
		}
	}

	void Jumping(){
		RaycastHit hit;
		if (Physics.Raycast(transform.position+Vector3.up, transform.TransformDirection(Vector3.down), out hit, groundCheckRayLength))
		{
			if (isJumping == false) {
				Debug.DrawRay (transform.position + Vector3.up, transform.TransformDirection (Vector3.down) * hit.distance, Color.green);

				// Add Jump Velocity
				Vector3 newVelocity = myRigidBody.velocity;
				newVelocity.y = jumpVelocity;
				if (lastState == BobStates.WALK_FORWARD)
					newVelocity.z = maxWalkSpeed;
				else
					newVelocity.z = -maxWalkSpeed;
				myRigidBody.velocity = newVelocity;

				// Set Jumping Variables
				isJumping = true;
				jumpStartY = transform.position.y;
			} else {
				isJumping = false;

				if (Mathf.Abs (jumpStartY - transform.position.y) >= jumpSuccessCheckDeltaY) {
					// Jump success
					ChangeState (lastState);
				} else {
					// Jump failed
					if (lastState == BobStates.WALK_BACKWARDS)
						ChangeState (BobStates.WALK_FORWARD);
					else
						ChangeState (BobStates.WALK_BACKWARDS);
				}
			}
		}else {
			Debug.DrawRay(transform.position+Vector3.up, transform.TransformDirection(Vector3.down) * groundCheckRayLength, Color.red);
		}
	}

	private void Respawn(){
		ChangeState(BobStates.IDLE);
		myAnim.SetBool ("isWalking", false);
		myAnim.SetTrigger ("reSpawn");
		transform.position = spawnPoint;
	}

	void ChangeState(BobStates newState){
		lastState = state;
		state = newState;

		myRigidBody.velocity = Vector3.zero;
		bobModel.transform.localScale = Vector3.one;
	}

	public void Clicked(){
		if (state == BobStates.IDLE) {
			myAnim.SetBool ("isWalking", true);
			ChangeState (BobStates.WALK_FORWARD);
		}
	}

	public void KillBob(){
		if (state != BobStates.DYING) {
			myAnim.SetTrigger ("killBob");
			ChangeState (BobStates.DYING);
			Invoke ("Respawn", 2.0f);
		}
	}

	enum BobStates {IDLE, WALK_FORWARD, WALK_BACKWARDS, JUMPING, DYING}
}
