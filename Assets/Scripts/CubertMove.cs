using UnityEngine;
using System.Collections;

public class CubertMove : MonoBehaviour {

	public float speed = 3.0f;
	public float jump = 3.0f;
	public float dblJump = 6.0f;
	public int jumpNbr = 2;
	public float smooth = 2.0f;
	public float bounce = 0.75f;
	public PhysicMaterial powerPhys;
	public PhysicMaterial regPhys;

	private Vector3 jumpY;
	private Vector3 jumpDir;
	private Vector3 crouch;
	private Vector3 move;
	private Vector3 hover;
	private Vector3 regScale;
	private Rigidbody rgdCubert;
	private bool isGrounded;
	private int jumpCnt = 0;
	private Quaternion abtFace;
	private Quaternion rightFace;
	private Collider cubertPhys;

	// Use this for initialization
	void Start () {
		rgdCubert = GetComponent<Rigidbody>();
		jumpY = new Vector3 (0, 1, 0);
		move = new Vector3 (1, 0, 0);
		abtFace = Quaternion.Euler (0, -180, 0);
		rightFace = Quaternion.Euler (0, 0, 0);
		crouch = new Vector3 (1, 0.5f, 1);
		regScale = new Vector3 (1, 1, 1);
		cubertPhys = GetComponent<Collider> ();

	}

	void Update () {
		if (isGrounded) {
			if (Input.GetAxis("Vertical") < 0) {
				transform.localScale = crouch;
			}
			if (Input.GetAxis("Vertical") >= 0) {
				transform.localScale = regScale;
			}
		}
	}

	void FixedUpdate () {
		move = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
		jumpDir = move * speed;
		hover = move * speed / jumpNbr;

		if (isGrounded) {
			rgdCubert.MovePosition (transform.position + move * speed * Time.deltaTime);
			if (Input.GetButtonDown ("Jump")) {
				rgdCubert.AddForce(jumpY * jump + jumpDir, ForceMode.Impulse);
			}
			if (Input.GetAxis ("Horizontal") < 0){
				transform.rotation = Quaternion.Slerp(transform.rotation, abtFace, Time.deltaTime * smooth);
			}
			if (Input.GetAxis ("Horizontal") > 0){
				transform.rotation = Quaternion.Slerp(transform.rotation, rightFace, Time.deltaTime * smooth);
			}
		}

		if (!isGrounded) {
			rgdCubert.MovePosition (transform.position + hover * Time.deltaTime);
			if (Input.GetButtonDown ("Jump") && jumpCnt < jumpNbr) {
				rgdCubert.AddForce(jumpY * dblJump + hover * Time.deltaTime, ForceMode.Impulse);
				jumpCnt++;
			}
		}

		if (Input.GetButton ("Bounce")) {
			cubertPhys.material = powerPhys;

		}

		if (!Input.GetButton ("Bounce")){
			cubertPhys.material = regPhys;
		}
	}

	void OnCollisionExit (){
		isGrounded = false;
	}

	void OnCollisionEnter (){
		isGrounded = true;
		jumpCnt = 0;
		//Debug.Log ("The jump count is " + jumpCnt);
	}
	
}
