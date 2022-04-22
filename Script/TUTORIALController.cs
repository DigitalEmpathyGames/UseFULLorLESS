using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TUTORIALController : MonoBehaviour {

	public static TUTORIALController instance = null;

	public bool tutorialActivado = false;
	public bool textoMostrado;
	public int tutorialAMostrar = 1;
	public string[] textoTutroial;
	public int textoAMostrar;

	[SerializeField]
	public GameObject flecha;

	[SerializeField]
	public GameObject flechatutorial;

	[SerializeField]
	public GameObject flechaModoDeJuego;

	[SerializeField]
	public GameObject flechaImagen2;

	[SerializeField]
	public GameObject flechaImagen1;

	[SerializeField]
	public GameObject flechaIzquierda;

	[SerializeField]
	public GameObject flechaDerecha;

	[SerializeField]
	public GameObject perro;
	[SerializeField]
	public GameObject cuadroTexto;
	[SerializeField]
	public GameObject canvasTuorial;
	[SerializeField]
	public GameObject flechaIcono;

	public Text textoTutorial;
	public int largoTutorial;


	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}else if(instance != this){
			Destroy (gameObject);
		}
		flechaIcono.SetActive (false);
		flecha.SetActive (false);
		flechatutorial.SetActive (false);
		flechaModoDeJuego.SetActive (false);
		flechaImagen2.SetActive (false);
		flechaImagen1.SetActive (false);
		flechaIzquierda.SetActive (false);
		flechaDerecha.SetActive (false);
		if (PlayerPrefs.GetInt ("tipoJuego") == 0) {
			//juegoOfline

			//tutorialActivado = true;

			textoAMostrar = 0;
			textoMostrado = false;
			if(PlayerPrefs.GetInt ("numeroNivel") == 1){
				largoTutorial = 15;
				textoTutroial [0] = "Bienvenido a UseFULLorLESS\nEl juego que te ayudará a\nmejorar tu concentración y reflejos";
				textoTutroial [1] = "El reloj indica el tiempo\nque te queda para responder\n";
				textoTutroial [2] = "Si respondes BIEN antes\nde este punto, ganarás\n300 puntos";
				textoTutroial [3] = "Si respondes BIEN antes\nde este, ganarás 200\nsino, ganarás 100";
				textoTutroial [4] = "Si respondes MAL antes\nde este punto, perderás\n50 puntos";
				textoTutroial [5] = "Si respondes MAL antes\nde este, perderás 100\nsino, perderás 150";
				textoTutroial [6] = "el puntaje que vas ganando\nse aculuma en este lugar\n";
				textoTutroial [7] = "El modo de juego aparece\nacá.\n";
				textoTutroial [8] = "USEFULL:\nDebes marcar la imagen más\nUTIL";
				textoTutroial [9] = "Como estamos en PLAYA,\nésta es la imagen más\nUTIL";
				textoTutroial [10] = "\n\n";
				textoTutroial [11] = "Las imagenes que aparecen\npor la derecha te darán\n una pista de lo UTIL";
				textoTutroial [12] = "Yo apareceré por la\nizquierda,si te atrapo\n pierdes";
				textoTutroial [13] = "Si respondes BIEN\nno me acercaré\n";
				textoTutroial [14] = "Si respondes MAL\nentonces lo haré\n";
				textoTutroial [15] = "Ya puedes jugar\nUseFULLorLESS\nBuenaSuerte.";
			}else if(PlayerPrefs.GetInt ("numeroNivel") == 2){
				largoTutorial = 3;
				flechaModoDeJuego.SetActive (true);
				textoTutroial [0] = "USELESS:\nDebes marcar la imagen más\nINUTIL";
				textoTutroial [1] = "Como estamos en COCINA,\nésta es la imagen más\nINUTIL";
				textoTutroial [2] = "\n\n";
				textoTutroial [3] = "Ya puedes jugar\nUseFULLorLESS\nBuenaSuerte.";
			}else if(PlayerPrefs.GetInt ("numeroNivel") == 3){
				largoTutorial = 4;
				flechaModoDeJuego.SetActive (true);
				textoTutroial [0] = "UseFULLorLESS:\nDebes marcar la imagen más\nUTIL o INUTIL";
				textoTutroial [1] = "Según el ícono que\naparece acá.\n";
				textoTutroial [2] = "Si el ícono es un CORRECTO\n debes marcar lo UTIL\n";
				textoTutroial [3] = "Si el ícono es un INCORRECTO\ndebes marcar lo INUTIL\n";
				textoTutroial [4] = "Ya puedes jugar\nUseFULLorLESS\nBuenaSuerte.";
			}

		}



	}
	
	// Update is called once per frame
	void Update () {
		if (tutorialActivado) {
			perro.SetActive (true);
				if (!textoMostrado) {
					textoTutorial.text = textoTutroial [textoAMostrar];
					textoMostrado = true;
				}
		} else {
			perro.SetActive (false);
			canvasTuorial.SetActive (false);
		}

			
	}

	public void cambiarTexto(){
		if (textoAMostrar < largoTutorial) {
			if(PlayerPrefs.GetInt ("numeroNivel") == 1){
				textoMostrado = false;
				textoAMostrar = textoAMostrar + 1;
				Vector3 nuevaPosicion;

				switch (textoAMostrar) {
				case 1:
					flecha.SetActive (true);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 2:
					flecha.SetActive (true);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 3:
					flecha.SetActive (true);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 4:
					flecha.SetActive (true);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 5:
					flecha.SetActive (true);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 6:
					flecha.SetActive (false);
					flechatutorial.SetActive (true);
					flechaModoDeJuego.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 7:
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (true);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 8:
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (true);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 9:
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					if (GameController.instance.getImagenCorrecta () == 1) {
						flechaImagen2.SetActive (false);
						flechaImagen1.SetActive (true);
					} else {
						flechaImagen2.SetActive (true);
						flechaImagen1.SetActive (false);
					}

					break;
				case 10:
					cuadroTexto.SetActive (false);
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 11:
					cuadroTexto.SetActive (true);
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (true);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					break;
				case 12:
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (true);
					flechaDerecha.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					break;
				case 13:
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (true);
					flechaDerecha.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					break;
				case 14:
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (true);
					flechaDerecha.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					break;
				case 15:
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					break;
				default:
					break;
				}
			}else if(PlayerPrefs.GetInt ("numeroNivel") == 2){
				flechaModoDeJuego.SetActive (false);
				textoMostrado = false;
				textoAMostrar = textoAMostrar + 1;
				switch (textoAMostrar) {
				case 1:
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					if (GameController.instance.getImagenCorrecta () == 1) {
						flechaImagen2.SetActive (false);
						flechaImagen1.SetActive (true);
					} else {
						flechaImagen2.SetActive (true);
						flechaImagen1.SetActive (false);
					}

					break;
				case 2:
					cuadroTexto.SetActive (false);
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 3:
					cuadroTexto.SetActive (true);
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					break;
				default:
					break;
				}
			}else if(PlayerPrefs.GetInt ("numeroNivel") == 3){
				flechaModoDeJuego.SetActive (false);
				textoMostrado = false;
				textoAMostrar = textoAMostrar + 1;
				switch (textoAMostrar) {
				case 1:
					flechaIcono.SetActive (true);
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
						flechaImagen2.SetActive (false);
						flechaImagen1.SetActive (false);

					break;
				case 2:
					flechaIcono.SetActive (false);
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					break;
				case 3:
					flecha.SetActive (false);
					flechatutorial.SetActive (false);
					flechaModoDeJuego.SetActive (false);
					flechaIzquierda.SetActive (false);
					flechaDerecha.SetActive (false);
					flechaImagen2.SetActive (false);
					flechaImagen1.SetActive (false);
					break;
				default:
					break;
				}
			}



		} else {
			tutorialActivado = false;
		}

	}



}
