// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;

public class TouchCamera : MonoBehaviour {


	private float topIzquierda = -15f;
	private float topDerecha = 15f;
	private float topArriba = 15f;
	private float topAbajo = -10f;

	public float mitadAlto;
	public float mitalLargo;
	public float posicionX;
	public float posicionY;


	public Camera m_FirstCamera;

	Vector2?[] oldTouchPositions = {
		null,
		null
	};
	Vector2 oldTouchVector;
	float oldTouchDistance;

	void start(){
		posicionX = transform.position.x;
		posicionY = transform.position.y;
	}

	void Update() {

		var halfHeight = Camera.main.orthographicSize;
		var halfWidth = halfHeight *Camera.main.aspect;

		mitadAlto = halfHeight;
		mitalLargo = halfWidth;



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
				
				switch (codOutTop)
				{
				case "INSIDE":

					break;
				case "OUT_UP_RIGHT":

						float posX = topDerecha - (Mathf.Floor(halfWidth) + 1f) ;
						float posY = topArriba - (Mathf.Floor(halfHeight) + 1f) ;
						transform.position = new Vector3 (posX , posY, transform.position.z);

					break;
				case "OUT_DOWN_RIGHT":
						 posX = topDerecha - (Mathf.Floor(halfWidth) + 1f) ;
						 posY = topAbajo + (Mathf.Floor(halfHeight) + 1f) ;
						transform.position = new Vector3 (posX , posY, transform.position.z);

					break;
				case "OUT_DOWN_LEFT":

						 posX = topIzquierda + (Mathf.Floor(halfWidth) + 1f) ;
						 posY = topAbajo + (Mathf.Floor(halfHeight) + 1f) ;
						transform.position = new Vector3 (posX , posY, transform.position.z);

					break;
				case "OUT_UP_LEFT":

						 posX = topIzquierda + (Mathf.Floor(halfWidth) + 1f) ;
						 posY = topArriba - (Mathf.Floor(halfHeight) + 1f) ;
						transform.position = new Vector3 (posX , posY, transform.position.z);

					break;
				case "OUT_UP":

						 posY = topArriba - (Mathf.Floor(halfHeight) + 1f) ;
						transform.position = new Vector3 (transform.position.x , posY, transform.position.z);

					break;
				case "OUT_RIGHT":

						 posX = topDerecha - (Mathf.Floor(halfWidth) + 1f) ;
						transform.position = new Vector3 (posX , transform.position.y, transform.position.z);

					break;
				case "OUT_DOWN":
						posY = topAbajo + (Mathf.Floor (halfHeight) + 1f);
						transform.position = new Vector3 (transform.position.x, posY, transform.position.z);
					break;
				case "OUT_LEFT":

						 posX = topIzquierda + (Mathf.Floor(halfWidth) + 1f) ;
						transform.position = new Vector3 (posX , transform.position.y, transform.position.z);

					break;
				default:
					break;
				}


				oldTouchPositions[0] = newTouchPosition;
			}
		}
	}
}
