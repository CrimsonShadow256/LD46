using UnityEngine;
using UnityEngine.Events;

public class BobTriggeredEvent : MonoBehaviour {

	public BobTriggeredEventType type;
	public UnityEvent eventToTrigger;

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Bob")) {
			switch(type){
			case BobTriggeredEventType.UNITY_EVENT:
				if (eventToTrigger != null) {
					eventToTrigger.Invoke ();
				}
				break;
			case BobTriggeredEventType.KILL_BOB:
				BobController bob = other.GetComponentInParent<BobController> ();
				bob.KillBob ();
				break;
			case BobTriggeredEventType.RESPAWN_POINT:
				break;
			}
		}
	}


	public enum BobTriggeredEventType {KILL_BOB, RESPAWN_POINT, UNITY_EVENT}
}
