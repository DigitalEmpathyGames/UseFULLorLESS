using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imegenFondoScript : MonoBehaviour {

	float moveSpeed = -5f;
	public Sprite[] sprites;
	private int numeroSprite;
	private SpriteRenderer spriteR;
	private Transform spriteT;
	private bool cambiarSprite = false;


	// Use this for initialization
	void Start () {
		numeroSprite = 0;
		spriteR = gameObject.GetComponent<SpriteRenderer>();
		spriteT = gameObject.GetComponent<Transform>();
		spriteR.sprite = sprites[elegirSprite ()];
		if (PlayerPrefs.GetInt ("numeroNivel") != 3) {
			spriteT.localScale = new Vector3 (0.45f, 0.45f, 1);
		} else {
			spriteT.localScale = new Vector3 (1f, 1f, 1f);
		}

	}
	
	// Update is called once per frame
	void Update () {
		
		if (PlayerPrefs.GetInt ("tipoJuego") == 1) {
			if(GameController.instance.cambiarNivel){
				if(cambiarSprite){
					spriteR.sprite = sprites[elegirSprite ()];
					cambiarSprite = false;
				}
			}
		}
		transform.position = new Vector2 (transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
		if (transform.position.x < -5f){
			transform.position = new Vector2 (5f, transform.position.y);
			numeroSprite = numeroSprite + 1;
			if(numeroSprite > sprites.Length){
				numeroSprite = 0;
			}
			spriteR.sprite = sprites[elegirSprite ()];
		}
		

	}

	public int elegirSprite (){
		int nivelActual = PlayerPrefs.GetInt ("numeroNivel");
		if (PlayerPrefs.GetInt ("tipoJuego") == 1) {
			//Juego onLine
			nivelActual = GameController.instance.nroNivelActual;
			if(nivelActual != 3){
				spriteT.localScale = new Vector3 (0.45f, 0.45f, 1);
			}else{
				spriteT.localScale = new Vector3 (1f, 1f, 1f);
			}
		}


		int primerElemento = (nivelActual - 1) * 5;
		int ultimoElemento = nivelActual * 5;
		return Random.Range (primerElemento, ultimoElemento);
	}
}

