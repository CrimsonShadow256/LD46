using UnityEngine;

public class FireworksSound : MonoBehaviour {

	public AudioClip fireworksSound;

	private Vector2 timeBetweenExplosions = new Vector2(0.3f, 1.0f);
	private float elapsedTime = 0.0f;

	// Update is called once per frame
	void Update () {
		elapsedTime -= Time.deltaTime;
		if (elapsedTime < 0.0f) {
			elapsedTime = Random.Range (timeBetweenExplosions.x, timeBetweenExplosions.y);
			AudioSource.PlayClipAtPoint (fireworksSound, transform.position);
		}
	}
}
