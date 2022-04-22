using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class btnObjectController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);
			Vector2 touchPosWorld2D = Camera.main.ScreenToWorldPoint (touch.position);

			if (GetComponent<Collider2D> () == Physics2D.OverlapPoint (touchPosWorld2D)) {
				SceneManager.LoadScene ("MenuScene");
			}
		}
	}
}
