using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	[HideInInspector]
	public static LevelManager singelton;

	private int sceneToLoad;

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
		sceneToLoad = SceneManager.GetSceneByName (nextLevel).buildIndex;
		StartSceneTransition ();
	}

	private void StartSceneTransition(){
		Invoke ("LoadScene", 2.0f);
	}

	private void LoadScene(){
		SceneManager.LoadScene (sceneToLoad);
	}
}
