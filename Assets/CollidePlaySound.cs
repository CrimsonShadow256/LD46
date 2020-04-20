using UnityEngine;

public class CollidePlaySound : MonoBehaviour {

	public AudioClip soundToPlay;
	public float yVelocityThreshold;

	void OnCollisionEnter(Collision other){
		if(Mathf.Abs(other.relativeVelocity.y) > yVelocityThreshold)
			AudioSource.PlayClipAtPoint (soundToPlay, transform.position);
	}
}
