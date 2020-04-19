using UnityEngine;
using UnityEngine.Events;

public class BobTriggeredEvent : MonoBehaviour {

	public BobTriggeredEventType type;
	public UnityEvent eventToTrigger;

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Bob")) {
			BobController bob = other.GetComponentInParent<BobController> ();
			if (bob == null)
				return;
			
			switch(type){
			case BobTriggeredEventType.UNITY_EVENT:
				break;
			case BobTriggeredEventType.KILL_BOB:
				bob.KillBob ();
				break;
			case BobTriggeredEventType.RESPAWN_POINT:
				bob.SetSpawnPoint (transform.position);
				break;
			case BobTriggeredEventType.VICTORY:
				bob.Victory ();
				LevelManager.singelton.LoadNextLevel ();
				break;
			}

			if (eventToTrigger != null) {
				eventToTrigger.Invoke ();
			}
		}
	}


	public enum BobTriggeredEventType {UNITY_EVENT, KILL_BOB, RESPAWN_POINT, VICTORY}
}
