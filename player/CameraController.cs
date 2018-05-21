using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour {
	private GameObject ball; // ボール取得

	private InputField inputField; // ボールの名前の取得

	private Vector3 offset; // カメラとボールのoffset

	void Start () {

	}

	void Update () {
		inputField = GameObject.Find("NameInput").GetComponent<InputField>();
		ball = GameObject.Find(inputField.text);
		offset = Camera.main.transform.position - ball.transform.position;

		Camera.main.transform.position = ball.transform.position + offset;
	}
}
