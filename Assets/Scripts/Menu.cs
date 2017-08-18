using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public float MaxTime = 5f;
	public float ActiveTime = 0f;
	public Image slowDownFillImg;
	bool startLoad = false;
	void Start () {

	}
	public void startLoadGameScene(){
		ActiveTime = 0f;
		startLoad = true;
	}
	public void stopLoadScene (){
		startLoad = false;
	}
	// Update isp called once per frame
	void Update () {
		if (startLoad) {
			ActiveTime += Time.deltaTime;
			var percent = ActiveTime / MaxTime;
			slowDownFillImg.fillAmount = Mathf.Lerp (0, 1, percent);
			if (slowDownFillImg.fillAmount == 1) {
				SceneBusses.sceneBusses.LoadScene = GameStatics.GameScene;
			}
		}
	}
}
