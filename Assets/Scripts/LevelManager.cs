using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	[HideInInspector]
	public static LevelManager singelton;

	private Animator myAnim;
	private float transitionDuration = 1.1f;
	private int sceneToLoad;
	private string sceneToLoadString;

	// Use this for initialization
	void Start () {
		if (singelton == null) {
			myAnim = GetComponent<Animator> ();
			singelton = this;
		}
		else
			Destroy (this);
	}
	
	public void LoadNextLevel(){
		sceneToLoad = SceneManager.GetActiveScene ().buildIndex + 1;
		Invoke ("StartSceneTransition", 3.5f);
	}

	public void LoadLevelByName(string nextLevel){
		sceneToLoad = -1;
		sceneToLoadString = nextLevel;
		StartSceneTransition ();
	}

	private void StartSceneTransition(){
		myAnim.SetTrigger ("StartTransition");
		Invoke ("LoadScene", transitionDuration);
	}

	private void LoadScene(){
		if (sceneToLoad >= 0)
			SceneManager.LoadScene (sceneToLoad);
		else
			SceneManager.LoadScene (sceneToLoadString);
	}
}
