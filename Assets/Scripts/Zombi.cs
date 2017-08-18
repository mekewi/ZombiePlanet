using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombi : MonoBehaviour {
	public Transform[] HitPositions;
	public NavMeshAgent navMesh;
	public GameObject HitPositiongameObject;
	public Transform Target;
	public Animator zombieAnimations;
	public GameSceneBuses gameBuses;
	public CapsuleCollider ZombieCollider;
	public Rigidbody ZombieRigidbody;
	public AudioClip ZombieAttack;
	public AudioClip ZombieKilled;
	public AudioSource ZombieAudioSource;
	bool isAlive = false;
	// Use this for initialization
	void Start () {

	}
	void OnEnable()
	{
		navMesh.enabled = false;
		isAlive = false;
		ZombieRigidbody.isKinematic = false;
		int randomTargetHiter = Random.Range(0,2);
		HitPositiongameObject.transform.SetParent (HitPositions [randomTargetHiter]);
		HitPositiongameObject.transform.localPosition = new Vector3 (0, 0, 0);
		float Speed = Random.Range(3.0f,5.0f);
		navMesh.speed  = Speed;
	}
	public void deActiveMe(){
		gameObject.SetActive (false);
	}
	public void Kill(){
		if (isAlive) {
			zombieAnimations.SetBool ("Dead", true);
			navMesh.enabled = false;
			isAlive = false;
			ZombieCollider.enabled = false;
			attackFinished ();
			ZombieAudioSource.PlayOneShot (ZombieKilled);
			gameBuses.ZombieKilled ();
			Invoke ("deActiveMe", 1);
			Debug.Log ("Kill");
		}
	}
	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag.Equals ("Human")) {
			zombieAnimations.SetBool ("Attack", false);
			attackFinished ();
		}
	}
	void OnTriggerStay(Collider other) {
		if (isAlive) {
			if (other.gameObject.tag.Equals ("Human")) {
				zombieAnimations.SetBool ("Attack", true);
				attack ();
			}

		}
	}
	void  OnCollisionEnter(Collision collision){
		if (collision.gameObject.tag.Equals ("Ground")) {
			ZombieRigidbody.isKinematic = true;
			navMesh.enabled = true;
			isAlive = true;
		}

	}
	public void attack(){
		gameBuses.PlayerAttacked ();
	}
	public void attackFinished(){
		gameBuses.finishAttackEffect ();
	}
	// Update is called once per frame
	void Update () {
		if (isAlive) {
			navMesh.SetDestination (Target.position);
		}
	}
}
