using UnityEngine;
using System.Collections;

public class CameraFollows : MonoBehaviour {

	public float smoothAmnt;
	public Rigidbody target;
	public Vector3 posAmnt;

	private Vector3 posJump;
	private Vector3 newPos;

	// Use this for initialization
	void Start () {
		//posAmnt = new Vector3 (10, 10, -10);
		transform.LookAt(target.position);

	}
	
	// Update is called once per frame
	void Update () {
		newPos = target.position + posAmnt;
		transform.position = Vector3.Lerp (transform.position, newPos, smoothAmnt); 
	}
}
