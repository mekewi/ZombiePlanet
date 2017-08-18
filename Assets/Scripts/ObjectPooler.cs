using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

public class ObjectPoolItem
{
	public GameObject objectToPool;
	public int amountToPool;
	public bool shouldExpand;
	public List<GameObject> pooledParent;
	public List<GameObject> pooledObjects;

}

public class ObjectPooler : MonoBehaviour
{

	public static ObjectPooler SharedInstance;
	public List<ObjectPoolItem> itemsToPool;
	public List<GameObject> pooledObjects;

	void Awake ()
	{
		SharedInstance = this;
	}

	// Use this for initialization
	void Start ()
	{
	}
	public GameObject addNewobjectFromPool(string tag,int parentIndex){
		foreach (ObjectPoolItem item in itemsToPool) {
			if (item.objectToPool.tag == tag) {
				if (item.pooledObjects.Count >= item.amountToPool) {
					foreach (GameObject pooledObject in item.pooledObjects) {
						if (!pooledObject.activeSelf) {
							pooledObject.transform.SetParent (item.pooledParent[parentIndex].transform);
							pooledObject.transform.localPosition = new Vector3 (0, 0, 0);
							pooledObject.SetActive (true);
							return pooledObject;
						}
					}
				} else {
					GameObject obj = (GameObject)Instantiate (item.objectToPool);
					obj.transform.SetParent (item.pooledParent[parentIndex].transform);
					obj.transform.localPosition = new Vector3 (0, 0, 0);
					obj.SetActive (true);
					item.pooledObjects.Add (obj);
					return obj;
				}
			}
		}
		return null;
	}
	public void deactiveObjectFromPool(string tag){
		foreach (ObjectPoolItem item in itemsToPool) {
			if (item.objectToPool.tag == tag) {
				foreach (GameObject pooledObject in item.pooledObjects) {
					pooledObject.SetActive (false);
				}
			}
		}
	}
	public GameObject GetPooledObject (string tag)
	{
		for (int i = 0; i < pooledObjects.Count; i++) {
			if (!pooledObjects [i].activeInHierarchy && pooledObjects [i].tag == tag) {
				return pooledObjects [i];
			}
		}
		foreach (ObjectPoolItem item in itemsToPool) {
			if (item.objectToPool.tag == tag) {
				if (item.shouldExpand) {
					GameObject obj = (GameObject)Instantiate (item.objectToPool);
					obj.SetActive (false);
					pooledObjects.Add (obj);
					return obj;
				}
			}
		}
		return null;
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
