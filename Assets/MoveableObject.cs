using UnityEngine;

[RequireComponent(typeof(InteractableObject))]
[RequireComponent(typeof(Rigidbody))]

public class MoveableObject : MonoBehaviour {

	public 	float				maxMoveSpeed;

	private InteractableObject	myInteraction;
	private Rigidbody			myRigidBody;
	private bool 				playerControlled;

	// Use this for initialization
	void Start () {
		myInteraction = GetComponent<InteractableObject> ();
		myRigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (myInteraction.isSelected == true)
				SetPlayerControl ();
		}

		if (playerControlled) {
			if (Input.GetMouseButtonUp (0))
				ReleasePlayerControl ();

			Vector3 newPosition = transform.position;
			newPosition.z += Time.deltaTime * maxMoveSpeed * Input.GetAxis ("Mouse X");
			newPosition.y += Time.deltaTime * maxMoveSpeed * Input.GetAxis ("Mouse Y");
			transform.position = newPosition;

		}
	}

	void SetPlayerControl(){
		playerControlled = true;
		myRigidBody.isKinematic = true;
	}

	void ReleasePlayerControl(){
		playerControlled = false;
		myRigidBody.isKinematic = false;
	}

}
