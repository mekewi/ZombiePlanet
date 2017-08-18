using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameStatics{
	
	public static SceneState sceneState;
	public static string MenuScene = "Menu";
	public static string GameScene = "GameScene";

	public enum SceneState {
		Reset, 
		WaitReset, 
		Preload,
		Load,
		Unload,
		Postload,
		Ready,
		Run,
		Count 
	};
}
