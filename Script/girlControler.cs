using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class girlControler : MonoBehaviour {

	[SerializeField]
	float moveSpeed =  -1f;

	[SerializeField]
	private bool alcanzado =  false;

	[SerializeField]
	private float posicionGirl;

	public float limiteGirl = 1.5f;

	public static girlControler instance = null;

	void Start () {
		posicionGirl = transform.position.x;
		if (instance == null) {
			instance = this;
		}else if(instance != this){
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(PlayerPrefs.GetInt ("tipoJuego") == 1){
			if(JuegoOnlineScript.instance.getIniciarJuegoOnline()){
				if(transform.position.x < limiteGirl){
					transform.position = new Vector2 (transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
					posicionGirl = transform.position.x;
				}
			}
		}else{
			if(transform.position.x < limiteGirl){
				transform.position = new Vector2 (transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
				posicionGirl = transform.position.x;
			}
		}


	}

	void OnTriggerEnter2D(Collider2D col){
		alcanzado = true;
	}

	public bool getAlcanzado(){
		return alcanzado;
	}

	public float getLimiteGirl(){
		return limiteGirl;
	}

	public float grtPosicionXGirl(){
		return posicionGirl;
	}




}
