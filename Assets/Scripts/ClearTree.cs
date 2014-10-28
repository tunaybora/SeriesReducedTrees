using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;

public class ClearTree : MonoBehaviour {

	public GUIText NCount;
	public static bool ClearedTree;

	void OnMouseOver () {
		gameObject.guiText.material.color = Color.gray;

		//Clear Tree
		if(Input.GetMouseButtonDown(0)){
			ClearedTree = true;
			Application.LoadLevel("Game");
		}
	}

	void OnMouseExit () {
		gameObject.guiText.material.color = Color.white;
		ClearedTree = false;
	}

}