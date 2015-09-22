using UnityEngine;
using System.Collections;

public class CubertMoveAgain : MonoBehaviour {

	public float speed = 3.0f;
	public float jump = 3.0f;
	public float dblJump = 6.0f;
	public int jumpNbr = 2;
	public float smooth = 2.0f;
	public Transform crchParent;

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

	// Use this for initialization
	void Start () {
		rgdCubert = GetComponent<Rigidbody>();
		jumpY = new Vector3 (0, 1, 0);
		move = new Vector3 (1, 0, 0);
		abtFace = Quaternion.Euler (0, -180, 0);
		rightFace = Quaternion.Euler (0, 0, 0);
		crouch = new Vector3 (1, 0.5f, 1);
		regScale = new Vector3 (1, 1, 1);
	}

	void Update () {
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
			if (Input.GetAxis("Vertical") < 0) {
				crchParent.localScale = crouch;
			}
			if (Input.GetAxis("Vertical") >= 0) {
				crchParent.localScale = regScale;
			}
		}
		if (!isGrounded) {
			rgdCubert.MovePosition (transform.position + hover * Time.deltaTime);
			if (Input.GetButtonDown ("Jump") && jumpCnt < jumpNbr) {
				rgdCubert.AddForce(jumpY * dblJump + hover * Time.deltaTime, ForceMode.Impulse);
				jumpCnt++;
			}
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
