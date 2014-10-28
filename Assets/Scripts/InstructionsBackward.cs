using UnityEngine;
using System.Collections;

public class InstructionsBackward : MonoBehaviour {

	void OnMouseOver () {
		if(Input.GetMouseButtonDown(0)){
			gameObject.guiText.material.color = Color.gray;
			StartCoroutine(Delay());
			
		}
	}
	
	IEnumerator Delay() {
		yield return new WaitForSeconds(0.3f);
		Application.LoadLevel(Application.loadedLevel-1);
	}
}