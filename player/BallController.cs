using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BallController : NetworkBehaviour {
	private GameObject club; // クラブ
	private GameObject clubStick; //クラブの棒

	private InputField inputField; // ボールの名前取得

	private Rigidbody rbBall; // ボールのRigidbody

	private Vector3 offset; // クラブの位置のリセット，カメラとボールの間
	private Vector3 stayBallPosition; // ボールが打たれる前の位置

	private float magTimer; // ボールの速さが0.1以下になった時に用いるタイマー
	private float onStageTimer, lagTimer; // ボールがステージから離れた時間，処理を行う際のラグの考慮用

	public static bool firstStop; // ボールを打ったらtrue，再度打てる状態はfalse
	private bool onStage;

	public override void OnStartLocalPlayer () {
		inputField = GameObject.Find("NameInput").GetComponent<InputField>();
        name = inputField.text;
		this.GetComponent<Renderer>().material.color = Color.black;
    }

	void Start () {
		firstStop = false;
		onStage = false;
		rbBall = this.GetComponent<Rigidbody>();
		club = GameObject.Find("Club");
		clubStick = GameObject.Find("ClubStick");
	}

	void Update () {
		if(!isLocalPlayer){
			return;
		}

		// ボールを打ったらtrue
		if(firstStop){
			if(onStage){
				onStageTimer += Time.deltaTime;
			}else{
				onStageTimer = 0;
			}

			if(rbBall.velocity.magnitude <= 0.1){
				magTimer += Time.deltaTime;
			}else{
				magTimer = 0f;
			}

			// ボールの速度が0.1以下になってから1秒後に再度打てるようにする
			if(1 <= magTimer){
				Vector3 ball = this.gameObject.transform.position;
				Vector3 camera = Camera.main.transform.position;
				HitButton.shootMode = false;
				HitButton.counter = 0;
				offset = (ball - camera)/3;
				offset.y = 0;
				club.transform.position = ball - offset;
				club.GetComponent<MeshRenderer>().enabled = true;
				club.GetComponent<BoxCollider>().enabled = true;
				clubStick.GetComponent<MeshRenderer>().enabled = true;

				magTimer = 0f;
				firstStop = false;
			}

			if(6 < onStageTimer){
				this.gameObject.transform.position = stayBallPosition;
			}
		}else{
			stayBallPosition = this.gameObject.transform.position;
		}
	}

	void OnCollisionEnter(Collision other) {
		if(other.gameObject.tag == "stage"){
			onStage = false;
		}
	}

	void OnCollisionExit(Collision other) {
		if(other.gameObject.tag == "stage"){
			onStage = true;
		}
	}
}
