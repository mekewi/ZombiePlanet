using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneBuses : MonoBehaviour {

	public FirstPersonController Player;
	public Image Blood;
	public int move =0;
	public GameObject UFOGroundHighlight;
	public GameObject UFO;
	public ObjectPooler objectPooler;
	public Transform ZombiesHome; 
	public ParticleSystem MuzzleFlash;
	public Animator PlayerAnimation;
	public Animator UFOAnimation;
	public bool inHelpZone = false;
	public AudioSource GameSceneAudio;
	public AudioSource backGroundMusic;
	public AudioClip ZombiesRun;

	public AudioClip Scream;
	public AudioClip ComeHere;
	public GameObject TowerLight;

	// Use this for initialization
	void Start () {
		GameSceneAudio.PlayOneShot (ComeHere);
		InvokeRepeating ("AddZombie",3,1);
	}
	public void AddZombie(){
		GameObject newZombie = objectPooler.addNewobjectFromPool ("Zombie",0);
		if (newZombie!=null) {
			newZombie.transform.SetParent (ZombiesHome.transform);
		}
	}
	public void ZombieKilled(){
		MuzzleFlash.gameObject.SetActive (true);
		MuzzleFlash.Stop ();
		MuzzleFlash.Play ();
		Player.GunShot ();
		PlayerAnimation.SetTrigger ("Shot");
	}
	public void StartHelpRound(){
		if (inHelpZone == false) {
			GameSceneAudio.PlayOneShot (Scream);
			backGroundMusic.clip = ZombiesRun;
			backGroundMusic.Play ();
			backGroundMusic.loop = true;
			TowerLight.SetActive (true);
			UFOAnimation.SetBool ("AttackTime", true);
			inHelpZone = true;
		}
	}
	public void PlayerAttacked(){
		Blood.gameObject.SetActive (true);
		Player.Attacked ();
	}
	public void finishAttackEffect(){
		Blood.gameObject.SetActive (false);
		Player.StopAttacked ();
	}
	// Update is called once per frame
	void Update () {
		UFOGroundHighlight.transform.position = new Vector3 (UFO.transform.position.x, UFOGroundHighlight.transform.position.y, UFO.transform.position.z);
	}
}
