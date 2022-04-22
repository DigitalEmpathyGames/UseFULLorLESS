// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;


public class CamaraScript : MonoBehaviour {

	public Camera m_FirstCamera;


	Vector2?[] oldTouchPositions = {
		null,
		null
	};
	Vector2 oldTouchVector;
	float oldTouchDistance;

	private float topIzquierda = -15f;
	private float topDerecha = 15f;
	private float topArriba = 10f;
	private float topAbajo = -10f;

	void Update() {

		transform.position = transform.TransformDirection (-18f,9f,-10f);

		var halfHeight = Camera.main.orthographicSize;
		var halfWidth = halfHeight *Camera.main.aspect;

		string codOutTop = "INSIDE";

		if(transform.position.y + halfHeight > topArriba && transform.position.x + halfWidth > topDerecha){
			codOutTop = "OUT_UP_RIGHT";
		}else if(transform.position.y - halfHeight < topAbajo && transform.position.x + halfWidth > topDerecha){
			codOutTop = "OUT_DOWN_RIGHT";
		}else if(transform.position.y - halfHeight < topAbajo && transform.position.x - halfWidth < topIzquierda){
			codOutTop = "OUT_DOWN_LEFT";
		}else if(transform.position.y + halfHeight > topArriba && transform.position.x - halfWidth < topIzquierda){
			codOutTop = "OUT_UP_LEFT";
		}else if(transform.position.y + halfHeight > topArriba){
			codOutTop = "OUT_UP";
		}else if(transform.position.x + halfWidth > topDerecha){
			codOutTop = "OUT_RIGHT";
		}else if(transform.position.y - halfHeight < topAbajo){
			codOutTop = "OUT_DOWN";
		}else if(transform.position.x - halfWidth < topIzquierda){
			codOutTop = "OUT_LEFT";
		}


		/*

		switch (codOutTop)
		{
		case "INSIDE":

			break;
		case "OUT_UP_RIGHT":
			if (Input.touchCount < 1) {
				float posX = topDerecha - (Mathf.Floor(halfWidth) + 1f) ;
				float posY = topArriba - (Mathf.Floor(halfHeight) + 1f) ;
				transform.position = new Vector3 (posX , posY, transform.position.z);
			}
			break;
		case "OUT_DOWN_RIGHT":
			if (Input.touchCount < 1) {
				float posX = topDerecha - (Mathf.Floor(halfWidth) + 1f) ;
				float posY = topAbajo + (Mathf.Floor(halfHeight) + 1f) ;
				transform.position = new Vector3 (posX , posY, transform.position.z);
			}
			break;
		case "OUT_DOWN_LEFT":
			if (Input.touchCount < 1) {
				float posX = topIzquierda + (Mathf.Floor(halfWidth) + 1f) ;
				float posY = topAbajo + (Mathf.Floor(halfHeight) + 1f) ;
				transform.position = new Vector3 (posX , posY, transform.position.z);
			}
			break;
		case "OUT_UP_LEFT":
			if (Input.touchCount < 1) {
				float posX = topIzquierda + (Mathf.Floor(halfWidth) + 1f) ;
				float posY = topArriba - (Mathf.Floor(halfHeight) + 1f) ;
				transform.position = new Vector3 (posX , posY, transform.position.z);
			}
			break;
		case "OUT_UP":
			if (Input.touchCount < 1) {
				float posY = topArriba - (Mathf.Floor(halfHeight) + 1f) ;
				transform.position = new Vector3 (transform.position.x , posY, transform.position.z);
			}
			break;
		case "OUT_RIGHT":
			if (Input.touchCount < 1) {
				float posX = topDerecha - (Mathf.Floor(halfWidth) + 1f) ;
				transform.position = new Vector3 (posX , transform.position.y, transform.position.z);
			}
			break;
		case "OUT_DOWN":
			if (Input.touchCount < 1) {
				float posY = topAbajo + (Mathf.Floor(halfHeight) + 1f) ;
				transform.position = new Vector3 (transform.position.x , posY, transform.position.z);
			}
			break;
		case "OUT_LEFT":
			if (Input.touchCount < 1) {
				float posX = topIzquierda + (Mathf.Floor(halfWidth) + 1f) ;
				transform.position = new Vector3 (posX , transform.position.y, transform.position.z);
			}
			break;
		default:
			break;
		}
		
		
		
		
		
		
		*/



		if (Input.touchCount == 0) {
			oldTouchPositions[0] = null;
			oldTouchPositions[1] = null;
		}
		else if (Input.touchCount == 1) {
			if (oldTouchPositions[0] == null || oldTouchPositions[1] != null) {
				oldTouchPositions[0] = Input.GetTouch(0).position;
				oldTouchPositions[1] = null;
			}
			else {
				Vector2 newTouchPosition = Input.GetTouch(0).position;

				transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * m_FirstCamera.orthographicSize / m_FirstCamera.pixelHeight * 2f));

				oldTouchPositions[0] = newTouchPosition;
			}
		}
		else {
			if (oldTouchPositions[1] == null) {
				oldTouchPositions[0] = Input.GetTouch(0).position;
				oldTouchPositions[1] = Input.GetTouch(1).position;
				oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
				oldTouchDistance = oldTouchVector.magnitude;
			}
			else {
				Vector2 screen = new Vector2(m_FirstCamera.pixelWidth, m_FirstCamera.pixelHeight);

				Vector2[] newTouchPositions = {
					Input.GetTouch(0).position,
					Input.GetTouch(1).position
				};
				Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
				float newTouchDistance = newTouchVector.magnitude;

				transform.position += transform.TransformDirection((Vector3)((oldTouchPositions[0] + oldTouchPositions[1] - screen) * m_FirstCamera.orthographicSize / screen.y));
				transform.localRotation *= Quaternion.Euler(new Vector3(0, 0, Mathf.Asin(Mathf.Clamp((oldTouchVector.y * newTouchVector.x - oldTouchVector.x * newTouchVector.y) / oldTouchDistance / newTouchDistance, -1f, 1f)) / 0.0174532924f));
				GetComponent<Camera>().orthographicSize *= oldTouchDistance / newTouchDistance;
				transform.position -= transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * m_FirstCamera.orthographicSize / screen.y);

				oldTouchPositions[0] = newTouchPositions[0];
				oldTouchPositions[1] = newTouchPositions[1];
				oldTouchVector = newTouchVector;
				oldTouchDistance = newTouchDistance;
			}
		}












	}
}
