using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(InteractableObject))]
public class ClickableObject : MonoBehaviour {

	public UnityEvent clickEvent;

	private InteractableObject	myInteraction;

	// Use this for initialization
	void Start () {
		myInteraction = GetComponent<InteractableObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			if (myInteraction.isSelected == true)
				clickEvent.Invoke ();
		}
	}
}
