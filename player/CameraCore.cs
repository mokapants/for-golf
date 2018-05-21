using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraCore : MonoBehaviour {
	private GameObject ball; // ボールの取得

	private InputField inputField; // ボールの名前取得

	//回転用
    Vector2 slidePosition; // タッチした座標
    Quaternion slideRotate; // タッチしたときの回転
	
	float wid, hei, diag; // スクリーンサイズ
	float touchX, touchY; // それぞれ上と横にスライドした移動量

	void Start () {
		wid = Screen.width;
   		hei = Screen.height;
	}
	
	void Update () {
		inputField = GameObject.Find("NameInput").GetComponent<InputField>();

		// CameraCoreはボールと位置を同期する
		if(GameObject.Find(inputField.text)){
			ball = GameObject.Find(inputField.text);
			this.gameObject.transform.position = ball.transform.position;
		}

		// 画面に触れてる指の本数が1本 かつ 見渡すモードだった場合
		if(Input.touchCount == 1 && !HitButton.shootMode){
			Touch t1 = Input.GetTouch (0);
			if (t1.phase == TouchPhase.Began){
       			slidePosition = t1.position;
        		slideRotate = this.gameObject.transform.rotation;
    		}else if(t1.phase == TouchPhase.Moved||t1.phase == TouchPhase.Stationary){
        		touchX = (t1.position.x - slidePosition.x)/wid; //横のスライド移動量(-1<tx<1)
        		touchY = (t1.position.y - slidePosition.y)/hei; //縦のスライド移動量(-1<ty<1) (現在未使用)
        		this.gameObject.transform.rotation = slideRotate;
        		this.gameObject.transform.Rotate(new Vector3(0, 90*touchX, 0),Space.World);
    		}
		}
	}
}
