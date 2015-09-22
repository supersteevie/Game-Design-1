using UnityEngine;
using System.Collections;

public class GivePower : MonoBehaviour {

	public GameObject player;
	public GameObject plyrNose;
	public PhysicMaterial power;
	public Material pwrSkin;

	private MeshRenderer regSkin;
	private MeshRenderer noseSkin;
	
	// Use this for initialization
	void Start () {
		regSkin = player.GetComponent<MeshRenderer>();
		noseSkin = plyrNose.GetComponent<MeshRenderer>();
	
	}

	void OnTriggerEnter (Collider cubert) {
		regSkin.material = pwrSkin;
		noseSkin.material = pwrSkin;
		CubertMoveAgain.powerPhys = power;
		if (pwrSkin.name == "CubertIce") {
			CubertMoveAgain.isIce = true;
		}
	}
}
