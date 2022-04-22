using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDiagonal : MonoBehaviour {

	[SerializeField]
	float moveSpeed =  1f;
	[SerializeField]
	float leftWayPointX = -11f;
	[SerializeField]
	float rigthWayPointX = 12f;



	void Update () {

		transform.position = new Vector2 (transform.position.x, transform.position.y + (-1f * moveSpeed * Time.deltaTime));
		if(transform.position.y < -10f ){
			transform.position = new Vector2 (transform.position.x , 10f);
		}
	}
}
