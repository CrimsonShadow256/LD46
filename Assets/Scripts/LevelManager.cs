using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	[HideInInspector]
	public static LevelManager singelton;

	private int sceneToLoad;
	private string sceneToLoadString;

	// Use this for initialization
	void Start () {
		if (singelton == null)
			singelton = this;
		else
			Destroy (this);
	}
	
	public void LoadNextLevel(){
		sceneToLoad = SceneManager.GetActiveScene ().buildIndex + 1;
		StartSceneTransition ();
	}

	public void LoadLevelByName(string nextLevel){
		sceneToLoad = -1;
		sceneToLoadString = nextLevel;
		StartSceneTransition ();
	}

	private void StartSceneTransition(){
		Invoke ("LoadScene", 2.0f);
	}

	private void LoadScene(){
		if (sceneToLoad >= 0)
			SceneManager.LoadScene (sceneToLoad);
		else
			SceneManager.LoadScene (sceneToLoadString);
	}
}
