using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
	Rigidbody2D rigid2D;
	Animator animator;
	float jumpForce = 680.0f;
	float walkForce = 30.0f;
	float maxWalkSpeed = 2.0f;
	float threshold = 0.2f;

	// Use this for initialization
	void Start () {
		this.rigid2D = GetComponent<Rigidbody2D> ();
		this.animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// space key
		if (Input.GetKeyDown (KeyCode.Space) && this.rigid2D.velocity.y == 0) {
			this.rigid2D.AddForce (transform.up * this.jumpForce);
		}
		// LeftClick or Tap
		if (Input.GetMouseButtonDown (0) && this.rigid2D.velocity.y == 0) {
			this.rigid2D.AddForce (transform.up * this.jumpForce);
		}

		// 左右移動
		int key = 0;
		if (Input.GetKey (KeyCode.RightArrow))
			key = 1;
		if (Input.GetKey (KeyCode.LeftArrow))
			key = -1;
		// 左右移動(デバイス傾き)
		if (Input.acceleration.x > this.threshold)
			key = 1;
		if (Input.acceleration.x < -this.threshold)
			key = -1;

		// プレイヤの速度
		float speedx = Mathf.Abs(this.rigid2D.velocity.x);

		// スピード制限
		if (speedx < this.maxWalkSpeed) {
			this.rigid2D.AddForce (transform.right * key * this.walkForce);
		}

		// 反転
		if (key != 0) {
			transform.localScale = new Vector3 (key, 1, 1); // 拡大率
		}

		// アニメーション
		this.animator.speed = speedx / 2.0f;

		// 画面外にでたら
		if (transform.position.y < -10) {
			SceneManager.LoadScene ("GameScene");
		}
	}
}
