using UnityEngine;
using System.Collections;

public class ResetLevel : MonoBehaviour {

	public GameObject cubert;
	private Rigidbody rgdbdyTarget;
	public PhysicMaterial none;

	void Start () {
		rgdbdyTarget = cubert.GetComponent<Rigidbody>();
		
	}
	
	void OnTriggerEnter (Collider character) {
		if (character.attachedRigidbody == rgdbdyTarget) {
			CubertMoveAgain.powerPhys = none;
			CubertMoveAgain.isIce = false;
			Application.LoadLevel ("Cubert_Choice");
		}
	}
	
}