using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitButton : MonoBehaviour {
	private GameObject club; // クラブ

	public static Vector3 initVect3; // クラブの最初の位置

	public static bool shootMode; // true: 打つモード，false: 視点操作モード

	public static int counter; // ボタンを押した回数

	void Start () {
		club = GameObject.Find("Club");
		shootMode = false;
		counter = 0;
	}

	void Update () {
		
	}

	public void OnClick(){
		if(counter%2 == 0){
			shootMode = true;
			initVect3 = club.transform.position;
		}else{
			shootMode = false;
			club.transform.position = initVect3;
		}
		counter += 1;
	}
}
