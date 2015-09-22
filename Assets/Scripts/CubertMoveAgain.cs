using UnityEngine;
using System.Collections;

public class CubertMoveAgain : MonoBehaviour {

	public float speed = 3.0f;
	public float jump = 3.0f;
	public float dblJump = 6.0f;
	public int jumpNbr = 2;
	public float smooth = 2.0f;
	public static PhysicMaterial powerPhys;
	public PhysicMaterial regPhys;
	private Collider cubertPhys;
	public static bool isIce;

	private Vector3 jumpY;
	private Vector3 jumpDir;
	private Vector3 crouch;
	private Vector3 move;
	//private Vector3 velLimit;
	private Vector3 hover;
	private Vector3 regScale;
	private Rigidbody rgdCubert;
	private bool isGrounded;
	private int jumpCnt = 0;
	private Quaternion turnSpeed;
	private Quaternion airRot;


	// Use this for initialization
	void Start () {
		rgdCubert = GetComponent<Rigidbody>();
		jumpY = new Vector3 (0, 1, 0);
		move = new Vector3 (1, 0, 0);
		//velLimit = Vector3.one * speed;
		crouch = new Vector3 (1, 0.5f, 1);
		regScale = new Vector3 (1, 1, 1);
		cubertPhys = GetComponent<Collider> ();
		isIce = false;
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
		jumpDir = transform.right;
		hover = transform.right * speed / jumpNbr;
		turnSpeed = Quaternion.Euler (0, Input.GetAxis("Horizontal") * speed, 0);

		//rgdCubert.MovePosition (transform.position + transform.forward * speed * Time.deltaTime);
		rgdCubert.MoveRotation(rgdCubert.rotation * turnSpeed);

		if (isGrounded) {
			CubertsMoving();
			if (isIce) {
				CubertIcy();
			}
		}

		if (!isGrounded) {
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
	}

	void CubertIcy () {
		if (Input.GetAxis ("Vertical") > 0){
			rgdCubert.AddRelativeForce (speed, 0, 0, ForceMode.Acceleration);
		}
		if (Input.GetAxis ("Vertical") <= 0) {
			rgdCubert.velocity = Vector3.Lerp(rgdCubert.velocity, Vector3.zero, smooth);
		}
	}

	void CubertsMoving () {
		if (Input.GetButtonDown ("Jump")) {
			rgdCubert.AddForce(jumpY * jump + jumpDir, ForceMode.VelocityChange);
		}
		if (!isIce) {
			if (Input.GetAxis ("Vertical") > 0 && rgdCubert.velocity.x < speed){
				//rgdCubert.AddRelativeForce(Vector3.forward * speed);
				//rgdCubert.MovePosition (transform.position + transform.right * speed * Time.deltaTime);
				rgdCubert.AddForce (transform.right * speed);
				rgdCubert.AddRelativeForce(Input.GetAxis("Vertical") * speed, 0, 0);
			}
		}
		if (Input.GetAxis ("Vertical") <= 0) {
			rgdCubert.velocity = Vector3.Lerp(rgdCubert.velocity, Vector3.zero, smooth);
		}
	}
}
