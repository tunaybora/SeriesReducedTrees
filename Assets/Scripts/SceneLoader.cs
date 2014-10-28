using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {

	public int LevelCount;

	void Update () {
		if(Input.GetMouseButtonDown(0)){
			if(LevelCount!=2){
				Application.LoadLevel(LevelCount);
				LevelCount++;
			}
		}
	}

	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
}
