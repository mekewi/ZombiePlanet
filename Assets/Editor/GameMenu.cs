using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CanEditMultipleObjects]

public class GameMenu : Editor
{
	public override void OnInspectorGUI()
	{
	}

	[MenuItem ("Scenes/Splash")]
	static void OpenMainScene ()
	{
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
		EditorSceneManager.OpenScene ("Assets/Scenes/Splash.unity");
	}
	[MenuItem ("Scenes/Menu")]
	static void OpenSplashScene ()
	{
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
		EditorSceneManager.OpenScene ("Assets/Scenes/Menu.unity");
	}
	[MenuItem ("Scenes/GameScene")]
	static void OpenGameScene ()
	{
		EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ();
		EditorSceneManager.OpenScene ("Assets/Scenes/GameScene.unity");
	}

}
