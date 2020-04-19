using UnityEngine;
using UnityEngine.Events;

public class BobTriggeredEvent : MonoBehaviour {

	public UnityEvent eventToTrigger;
	public bool killBob;

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Bob")) {
			if (eventToTrigger != null) {
				eventToTrigger.Invoke ();
			}

			if (killBob) {
				BobController bob = other.GetComponentInParent<BobController> ();
				bob.KillBob ();
			}
		}
	}
}
