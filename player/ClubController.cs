using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClubController : MonoBehaviour {
	private GameObject club; // クラブの取得
	private GameObject clubStick; //クラブの棒

	private InputField inputField; // ボールの名前取得

	private Rigidbody clubRb; // クラブのRigidbody
	private Rigidbody rbBall; // ボールのRigidbody

	private Vector3 ball; // ボールの座標取得
	private Vector3 offset; // クラブとボールのoffset
	private Vector3 dirShoot; // ボールの飛ぶ方向

	// 打つ時用
	private float firstPosition, slidePosition; // タッチした時の画面の座標，タッチしてからスライドした時の移動量
	private float x, y, z; // x,zはタッチした瞬間の座標，yは0
	private float xPosition, zPosition; // x,zの計算後の位置

	private float clubSpeed; // 打つ時のクラブの速さ

	void Start () {
		club = GameObject.Find("Club");
		clubStick = GameObject.Find("ClubStick");
		clubRb = club.GetComponent<Rigidbody>();
	}

	void Update () {
		inputField = GameObject.Find("NameInput").GetComponent<InputField>();
		/*if(Input.GetMouseButton(0) && HitButton.shootMode){
			clubRb.velocity = new Vector3(0, 0, 10);
		}else{
			clubRb.velocity = Vector3.zero;
		}*/

		// 画面に触れてる指の本数が1本 かつ 撃つ気満々のとき
		if(Input.touchCount == 1 && HitButton.shootMode){
			Touch touch = Input.GetTouch(0); // タッチした指の一本目の取得
			ball = GameObject.Find(inputField.text).transform.position;

			// タッチした瞬間
			if(touch.phase == TouchPhase.Began){
				offset = club.transform.position - ball;
				firstPosition = touch.position.y;
				x = club.transform.position.x;
				z = club.transform.position.z;
			}
			// タッチして動かしてる時 か タッチして止まってる時
			else if(touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary){
				slidePosition = (firstPosition - touch.position.y)/10;
				firstPosition = touch.position.y;
				xPosition = (slidePosition*offset.x);
				y = 0;
				zPosition = (slidePosition*offset.z);
				clubRb.velocity = new Vector3(xPosition, y, zPosition);
			}
		}else{
			clubRb.velocity = Vector3.zero;
		}
	}

	void OnTriggerEnter(Collider other) {
		rbBall = GameObject.Find(inputField.text).GetComponent<Rigidbody>();
		dirShoot = this.transform.position - HitButton.initVect3;

		// ボールにクラブが当たったら
		if(other.gameObject.name == inputField.text){
			clubSpeed = club.GetComponent<Rigidbody>().velocity.magnitude;
			rbBall.AddForce(dirShoot*clubSpeed*3, ForceMode.Impulse);
			club.GetComponent<MeshRenderer>().enabled = false;
			club.GetComponent<BoxCollider>().enabled = false;
			clubStick.GetComponent<MeshRenderer>().enabled = false;
			BallController.firstStop = true;
		}
	}
}
