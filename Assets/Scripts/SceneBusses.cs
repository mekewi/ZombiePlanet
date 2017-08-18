using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class SceneBusses : MonoBehaviour {

	public static SceneBusses sceneBusses;
	private string currentSceneName;
	private string loadScene;

	private AsyncOperation resourceUnloadTask;
	private AsyncOperation sceneLoadTask;
	private delegate void UpdateDelegate();
	private UpdateDelegate[] updateDelegates;
	public Image FadeImage;



	public string LoadScene {
		get {
			return loadScene;
		}
		set {
			if (loadScene != value) {
				loadScene = value;
			}
		}
	}



	protected void Awake()
	{
		//Setup the singleton instance
		if (sceneBusses != null) {
			Destroy (gameObject);
		} else {
			sceneBusses = this;
		}
		DontDestroyOnLoad (gameObject);
		//Setup the array of updateDelegates
		updateDelegates = new UpdateDelegate[(int)GameStatics.SceneState.Count];
		loadScene = GameStatics.MenuScene;
		GameStatics.sceneState = GameStatics.SceneState.Reset;
		//Set each updateDelegate
		updateDelegates [(int)GameStatics.SceneState.Reset] = UpdateSceneReset;
		updateDelegates [(int)GameStatics.SceneState.WaitReset] = waitReset;
		updateDelegates [(int)GameStatics.SceneState.Preload] = UpdateScenePreload;
		updateDelegates [(int)GameStatics.SceneState.Load] = UpdateSceneLoad;
		updateDelegates [(int)GameStatics.SceneState.Unload] = UpdateSceneUnload;
		updateDelegates [(int)GameStatics.SceneState.Postload] = UpdateScenePostload;
		updateDelegates [(int)GameStatics.SceneState.Ready] = UpdateSceneReady;
		updateDelegates [(int)GameStatics.SceneState.Run] = UpdateSceneRun;
	}

	protected void OnDestroy()
	{
		Debug.Log ("OnDestroy");
		//Clean up all the updateDelegates
		if (updateDelegates != null) {
			for (int i = 0; i < (int)GameStatics.SceneState.Count; i++) {
				updateDelegates [i] = null;
			}
			updateDelegates = null;
		}
		//Clean up the singleton instance
		if (sceneBusses != null) {
			Debug.Log ("Removing instance of scenebusses ... !");
			sceneBusses = null;
		}
	}
	protected void Update()
	{
		if (updateDelegates != null) {
			if (updateDelegates [(int)GameStatics.sceneState] != null) {
				updateDelegates [(int)GameStatics.sceneState] ();
			}
		} else {
			Debug.Log ("updateDelegates = "+updateDelegates);
		}
		Debug.Log ("Current Scene State : " + currentSceneName);

	}
	private void waitReset(){
	
	}
	private void UpdateSceneReset()
	{
		Debug.Log ("SceneBusses > UpdateSceneReste");
		GameStatics.sceneState = GameStatics.SceneState.WaitReset;
		StartCoroutine (SceneReset());
	} 

	public IEnumerator SceneReset(){
		Debug.Log ("SceneBusses >  SceneReste");
		FadeImage.gameObject.SetActive (true);
		FadeImage.CrossFadeAlpha (1, 0.11f, false);
		yield return new WaitForSeconds (0.07f);
		System.GC.Collect();
		GameStatics.sceneState = GameStatics.SceneState.Preload;
	}

	private void UpdateScenePreload()
	{
		GameStatics.sceneState = GameStatics.SceneState.Load;
		Debug.Log ("SceneBusses >  UpdateScenePreLoad");
		if (sceneLoadTask == null) {
			sceneLoadTask = new AsyncOperation ();
		}
		sceneLoadTask = SceneManager.LoadSceneAsync (loadScene);
	} 

	private void UpdateSceneLoad ()
	{
		Debug.Log ("SceneBusses > UpdateSceneLoad");
		if (sceneLoadTask.isDone == true) {
			GameStatics.sceneState = GameStatics.SceneState.Unload;
		}
	}


	private void UpdateSceneUnload()
	{
		Debug.Log ("SceneBusses > UpdateSceneUnLoad");
		if (resourceUnloadTask == null) {
			resourceUnloadTask = Resources.UnloadUnusedAssets ();
		} else {
			if (resourceUnloadTask.isDone == true) {
				resourceUnloadTask = null;
				GameStatics.sceneState = GameStatics.SceneState.Postload;
			}
		}
	} 

	private void UpdateScenePostload()
	{
		Debug.Log ("SceneBusses > UpdateScenePostLoad");
		currentSceneName = loadScene;
		GameStatics.sceneState = GameStatics.SceneState.Ready;
	}//PostLoad

	private void UpdateSceneReady()
	{
		Debug.Log ("SceneBusses > UpdateSceneReady");
		System.GC.Collect ();
		GameStatics.sceneState = GameStatics.SceneState.Run;
	}//Ready

	private void UpdateSceneRun()
	{
		Debug.Log ("SceneBusses > UpdateSceneRun");
		FadeImage.CrossFadeAlpha (0,0.11f, false);
		FadeImage.gameObject.SetActive (false);
		if (currentSceneName != loadScene) {
			GameStatics.sceneState = GameStatics.SceneState.Reset;
		}
	}

	public static void RestartGame()
	{
		SceneManager.LoadSceneAsync (SceneManager.GetActiveScene().name);
	}
}
