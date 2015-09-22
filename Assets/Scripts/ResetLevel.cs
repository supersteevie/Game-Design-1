using UnityEngine;
using System.Collections;

public class ResetLevel : MonoBehaviour {

	public GameObject cubert;
	private Rigidbody rgdbdyTarget;

	void Start () {
		rgdbdyTarget = cubert.GetComponent<Rigidbody>();
		
	}
	
	void OnTriggerEnter (Collider character) {
		if (character.attachedRigidbody == rgdbdyTarget) {
			Application.LoadLevel ("TopDownLevel");
		}
	}
	
}