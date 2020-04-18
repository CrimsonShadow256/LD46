using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobController : MonoBehaviour {

	public float wallCheckRayLength;
	public float maxWalkSpeed;


	private BobStates state;
	private Rigidbody myRigidBody;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody> ();
		state = BobStates.IDLE;
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
		}
	}

	void WalkForward(){
		Vector3 newVelocity = myRigidBody.velocity;
		if (newVelocity.z < maxWalkSpeed)
			newVelocity.z = maxWalkSpeed;
		myRigidBody.velocity = newVelocity;

		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast (transform.position + Vector3.up, transform.TransformDirection (Vector3.forward), out hit, wallCheckRayLength)) {
			Debug.DrawRay (transform.position + Vector3.up, transform.TransformDirection (Vector3.forward) * hit.distance, Color.red);
			state = BobStates.WALK_BACKWARDS;
		} else {
			Debug.DrawRay(transform.position+Vector3.up, transform.TransformDirection(Vector3.forward) * wallCheckRayLength, Color.green);
		}
	}

	void WalkBackwards(){
		Vector3 newVelocity = myRigidBody.velocity;
		if (newVelocity.z > -maxWalkSpeed)
			newVelocity.z = -maxWalkSpeed;
		myRigidBody.velocity = newVelocity;

		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(transform.position+Vector3.up, transform.TransformDirection(Vector3.back), out hit, wallCheckRayLength))
		{
			Debug.DrawRay(transform.position+Vector3.up, transform.TransformDirection(Vector3.back) * hit.distance, Color.red);
			state = BobStates.WALK_FORWARD;
		}else {
			Debug.DrawRay(transform.position+Vector3.up, transform.TransformDirection(Vector3.back) * wallCheckRayLength, Color.green);
		}
	}

	public void Clicked(){
		if (state == BobStates.IDLE)
			state = BobStates.WALK_FORWARD;
	}

	enum BobStates {IDLE, WALK_FORWARD, WALK_BACKWARDS, JUMPING, DIEING}
}
