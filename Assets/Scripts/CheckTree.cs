using UnityEngine;
using System.Collections;

public class CheckTree : MonoBehaviour {

	void OnMouseOver () {
		gameObject.guiText.material.color = Color.gray;
		
		//Check Tree
		if(Input.GetMouseButtonDown(0)){
			ClearTree.ClearedTree = true;
		}
	}
	
	void OnMouseExit () {
		gameObject.guiText.material.color = Color.white;
		ClearTree.ClearedTree = false;
	}

}
