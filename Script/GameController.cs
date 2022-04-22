using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController instance = null;

	public Canvas canvasLoading;
	private AsyncOperation operacion;
	[SerializeField]
	Text textoLoad;

	public bool gameOver = false;



	[SerializeField]
	public GameObject[] imageOne;

	[SerializeField]
	public GameObject CanvasFinNivel;

	[SerializeField]
	public GameObject CanvasFinNivelOnline;

	[SerializeField]
	public GameObject CanvasBotones;

	[SerializeField]
	public GameObject[] imageTwo;

	[SerializeField]
	public GameObject[] icon;

	[SerializeField]
	public GameObject usefull;
	[SerializeField]
	public GameObject useless;
	[SerializeField]
	public GameObject usefullless;

	public string[] nombresNivel;

	[SerializeField]
	Transform spawnPoint;

	[SerializeField]
	Transform spawnIcon;


	[SerializeField]
	Transform spawnImageOne;

	[SerializeField]
	Transform spawnImageTwo;

	[SerializeField]
	public Text scoreNivel;

	[SerializeField]
	Text scoreNivelOnline;

	public Text timerPartida;
	public Text txtNombreNivel;
	public Text txtResultado;

	[SerializeField]
	float spawnRate = 2f;
	float nextSpawn = 2f;

	public int velocidadDePuntaje = 10; 

	private int puntajeASumar;
	public int puntajeNivel;

	private int imagenCorrecta;

	string nombrescoreNivel;
	public string nombreEstrellaNivel;

	public bool permitirJugar = false;

	public bool refrescarImagen = false;

	public bool jugadaPerdida = true;
	private int numeroJugada = 0;
	public int cantMaxJugadas = 20;
	public int puntajeMaximo;
	public bool mpstrarPuntajeFinal;
	public int estrellaGanada1 = 0;
	public int estrellaGanada2 = 0;
	public int estrellaGanada3 = 0;
	public bool finalizar;
	public int niveles;
	public int nivelesCreados = 10;
	public int imagenesNivel;
	public int modojuego;
	public int nroNivelActual;
	public float tiempoCambioMapa = 10f;
	public bool cambiarNivel = false;
	public bool noJugo = true;
	public int minutoActual;
	public int segundoActual;
	private float segundero = 1f;
	public Image timeLevelBar;
	public Image timeLevelBarFondo;

	private int multiplicador = 1;
	private string actualMultiplicador = "";
	private string anteMultiplicador = "";
	private string actualTipo = "";
	private string anteriorTipo = "";

	public string getAntrTipo(){
		return anteriorTipo;
	}

	public void setAntrTipo(string nvoAntrTipo){
		anteriorTipo = nvoAntrTipo;
	}

	public string getActlTipo(){
		return actualTipo;
	}

	public void setActlTipo(string nvoActlTipo){
		actualTipo = nvoActlTipo;
	}

	public string getAntMltplcdr(){
		return anteMultiplicador;
	}

	public void setAntMltplcdr(string nuevoAntMltplcdr){
		anteMultiplicador = nuevoAntMltplcdr;
	}

	public void setActualMultiplicador(string nvoActualMultiplicador){
		actualMultiplicador = nvoActualMultiplicador;
	}

	public string getActualMultiplicadorr(){
		return actualMultiplicador;
	}

	public void setMultiplicador(int nvoMltplcdr){
		multiplicador = nvoMltplcdr;
	}

	public int getMultiplicador(){
		return multiplicador;
	}

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		}else if(instance != this){
			Destroy (gameObject);
		}


		imageTwo = imageOne;
		CanvasFinNivel.SetActive (false);


		puntajeASumar = 0;
		puntajeNivel = 0;
		permitirJugar = true;
		jugadaPerdida = true;
		puntajeMaximo = (cantMaxJugadas + 1) * 300; 
		finalizar = false;

		if (PlayerPrefs.GetInt ("tipoJuego") == 1) {
			timeLevelBar.gameObject.SetActive (true);
			timeLevelBarFondo.gameObject.SetActive (true);
			timerPartida.text = "PARTIDA = " + minutoActual + ":" + segundoActual;
			TUTORIALController.instance.tutorialActivado = false;
			int ultimoNivel = nivelesCreados;
			nroNivelActual = Random.Range (1, ultimoNivel);
			txtNombreNivel.text = nombresNivel[nroNivelActual - 1];
			modojuego = 3;
			SpawnImage (nroNivelActual);
			operacion = null;
			CanvasBotones.SetActive (false);
		} else {
			timeLevelBar.gameObject.SetActive (false);
			timeLevelBarFondo.gameObject.SetActive (false);
			nroNivelActual = PlayerPrefs.GetInt ("numeroNivel");
			nombrescoreNivel = "score" + nroNivelActual;
			nombreEstrellaNivel = "estrella" + nroNivelActual;
			modojuego = PlayerPrefs.GetInt ("modoJuego");
			SpawnImage (nroNivelActual);
			operacion = null;
			canvasLoading.gameObject.SetActive (false);
			if(PlayerPrefs.GetInt (nombreEstrellaNivel) > 0){
				CanvasBotones.SetActive (true);
			}else{
				CanvasBotones.SetActive (false);
			}
			if (nroNivelActual < 4) {
				TUTORIALController.instance.tutorialActivado = true;
			} else {
				TUTORIALController.instance.tutorialActivado = false;
			}
		}

		switch(modojuego){
		case 1:
			usefull.SetActive (true);
			useless.SetActive (false);
			usefullless.SetActive (false);
			break;
		case 2:
			usefull.SetActive (false);
			useless.SetActive (true);
			usefullless.SetActive (false);
			break;
		case 3:
			usefull.SetActive (false);
			useless.SetActive (false);
			usefullless.SetActive (true);
			break;
		default:
			usefull.SetActive (true);
			useless.SetActive (false);
			usefullless.SetActive (false);
			break;
		}

	}



	// Update is called once per frame
	void Update () {
		//SpawnObstacle (PlayerPrefs.GetInt ("numeroNivel"));	
		//Debug.Log(PlayerPrefs.GetInt ("tipoJuego"));
		if (PlayerPrefs.GetInt ("tipoJuego") == 1 && JuegoOnlineScript.instance.getIniciarJuegoOnline()) {
			
			segundero = segundero - Time.deltaTime;
			if(segundero < 0f){
				segundero = 1f;
				segundoActual = segundoActual - 1;
				if(segundoActual < 0){
					minutoActual = minutoActual - 1;
					segundoActual = 59;
				}
				if (!(minutoActual < 0)) {
					string separador = ":";
					if (segundoActual < 10){
						separador = ":0";
					}
					timerPartida.text = "PARTIDA = " + minutoActual + separador + segundoActual;
				} else {
					gameOver = true;
				}
			}

			if(cambiarNivel){
				tiempoCambioMapa = 10f;
				cambiarNivel = false;
			}
			tiempoCambioMapa = tiempoCambioMapa - Time.deltaTime;


			if(tiempoCambioMapa < 0f){
				nroNivelActual = Random.Range (1, nivelesCreados);
				txtNombreNivel.text = nombresNivel[nroNivelActual - 1];
				cambiarNivel = true;
			}
			if(girlControler.instance.getAlcanzado ()){
				gameOver = true;
			}

			if (!gameOver) {
				timeLevelBar.fillAmount = tiempoCambioMapa / 10f;
				//showing score
				actualizarPuntaje ();

				//waiting girl to start Clock
				float xpositionGirl = girlControler.instance.grtPosicionXGirl ();
				float limiteGirl = girlControler.instance.getLimiteGirl ();
				if (xpositionGirl > limiteGirl) {
					TimerScript.instance.setAnimar (true);
					clockScript.instance.setAnimar (true);
				}


				// use clock to move dog

				if (TimerScript.instance.getTiempoAgotadoReloj () && permitirJugar) {
					noJugo = true;
					string jugadaNoJugada = "";
					//0
					jugadaNoJugada = jugadaNoJugada + "jugada";
					//1
					jugadaNoJugada = jugadaNoJugada + "_numero:" + JuegoOnlineScript.instance.getNumeroJugada ();
					//2
					jugadaNoJugada = jugadaNoJugada + "_jugador:" + JuegoOnlineScript.instance.gettipoJugador ();
					//3
					jugadaNoJugada = jugadaNoJugada + "_tipo:perdida";
					//4
					jugadaNoJugada = jugadaNoJugada + "_reaccion:" + TimerScript.instance.maxTime;
					//5
					jugadaNoJugada = jugadaNoJugada + "_puntaje:0";
					//6
					jugadaNoJugada = jugadaNoJugada + "_tipoMuliplicador:GOOD";
					//7
					jugadaNoJugada = jugadaNoJugada + "_cantMuliplicador:" + 1;
					//8
					jugadaNoJugada = jugadaNoJugada + "_factorJugada:" + -100f;
					if (JuegoOnlineScript.instance.gettipoJugador () == 1) {
						JuegoOnlineScript.instance.agregarJugadasJugador (jugadaNoJugada);
					} else {
						JuegoOnlineScript.instance.enviarMensaje (jugadaNoJugada);
					}
					JuegoOnlineScript.instance.setPermitirJugarOnline (true);	
				} else {
					noJugo = false;
				}


				//respawn image
				if (TimerScript.instance.getTiempoAgotadoReloj ()) {
					numeroJugada = numeroJugada + 1;
					permitirJugar = true;
					SpawnImage (nroNivelActual);

				}

				finalizar = true;

			} else if (finalizar) {
				finalizar = false;
				CanvasBotones.SetActive (false);
				puntajeNivel = puntajeNivel + puntajeASumar;
				puntajeASumar = 0;
				scoreNivelOnline.text = "PUNTAJE : " + puntajeNivel;
				int nvoPnjTotal = puntajeNivel + PlayerPrefs.GetInt ("puntajeTotal");
				PlayerPrefs.SetInt ("puntajeTotal", nvoPnjTotal);
				if (JuegoOnlineScript.instance.gettipoJugador () == 1) {
					if (girlControler.instance.getAlcanzado ()) {
						txtResultado.text = "Has Ganado";
					} else {
						txtResultado.text = "Has Perdido";
					}
				} else {
					if (girlControler.instance.getAlcanzado ()) {
						txtResultado.text = "Has Perdido";
					} else {
						txtResultado.text = "Has Ganado";
					}
				}

				scoreNivel.text = "PUNTAJE : " + puntajeNivel;
				CanvasFinNivelOnline.SetActive (true);

			} else {
				//scoreNivel.text = "";
			}
		} else {
			if (TUTORIALController.instance.tutorialActivado) {
				if (nroNivelActual == 1) {

				}
			} else {
				if(girlControler.instance.getAlcanzado () || numeroJugada > cantMaxJugadas){
					gameOver = true;
				}

				if (!gameOver) {

					//showing score
					actualizarPuntaje();

					//waiting girl to start Clock
					float xpositionGirl = girlControler.instance.grtPosicionXGirl();
					float limiteGirl = girlControler.instance.getLimiteGirl ();
					if( xpositionGirl > limiteGirl){
						TimerScript.instance.setAnimar (true);
						clockScript.instance.setAnimar (true);
					}


					// use clock to move dog
					if(TimerScript.instance.getTiempoAgotadoReloj() && jugadaPerdida){
						perroControler.instance.setAvance (true);
					}

					//respawn image
					if (TimerScript.instance.getTiempoAgotadoReloj () ) {
						numeroJugada = numeroJugada + 1;
						permitirJugar = true;
						SpawnImage (nroNivelActual);

					}

					finalizar = true;

				} else if(finalizar){
					finalizar = false;
					CanvasBotones.SetActive (false);
					CanvasFinNivel.SetActive (true);
					int puntajeregistrado = PlayerPrefs.GetInt (nombrescoreNivel);

					puntajeNivel = puntajeNivel + puntajeASumar;
					puntajeASumar = 0;
					scoreNivel.text = "PUNTAJE : " + puntajeNivel;
					if(puntajeregistrado < puntajeNivel){
						PlayerPrefs.SetInt (nombrescoreNivel, puntajeNivel);
					}

				}
			}

		}



	}

	public void actualizarPuntaje(){
		
		int puntajeTope = puntajeNivel + puntajeASumar;

			if (puntajeNivel < puntajeTope) {
				puntajeNivel = puntajeNivel + velocidadDePuntaje;
				puntajeASumar = puntajeASumar - velocidadDePuntaje;

			}else if(puntajeTope < puntajeNivel){
				puntajeNivel = puntajeNivel - velocidadDePuntaje;
				puntajeASumar = puntajeASumar + velocidadDePuntaje;


			} else {
				puntajeNivel = puntajeTope;
			}

		if (puntajeNivel < 0){
			puntajeNivel = 0;
		}

		scoreNivel.text = "PUNTAJE : " + puntajeNivel;
	}

	/*
	void SpawnObstacle(int numeroNivel){
		
		if(Time.time > nextSpawn){
			int primerElemento = (numeroNivel - 1) * imagenesNivel;
			int ultimoElemento = numeroNivel * imagenesNivel;
			nextSpawn = Time.time + spawnRate;
			int randomObstacle = Random.Range (primerElemento, ultimoElemento);
			Instantiate (obstacles[randomObstacle], spawnPoint.position, Quaternion.identity);
		}
	}
	*/

	void SpawnImage(int numeroNivel){
		int ultimoNivel;
		if(PlayerPrefs.GetInt ("tipoJuego") == 1){
			ultimoNivel = nivelesCreados;
		}else{
			ultimoNivel = PlayerPrefs.GetInt ("nivelesCreados");
		}


		int cantImagenPorNivel = imagenesNivel;
		imagenCorrecta = Random.Range (1, 3);
		int primerElemento = (numeroNivel - 1) * cantImagenPorNivel;
		int ultimoElemento = numeroNivel * cantImagenPorNivel;
		int randomImagenCorrecta = Random.Range (primerElemento, ultimoElemento);



		int randomImageIncorrecta;

		switch(imagenCorrecta){
		case 1:
			if (numeroNivel < 2) {
				randomImageIncorrecta = Random.Range (ultimoElemento, imageOne.Length);
			} else {
				randomImageIncorrecta = Random.Range (0, (numeroNivel - 1) * cantImagenPorNivel);
			}
			Instantiate (imageOne [randomImagenCorrecta], spawnImageOne.position, Quaternion.identity);
			Instantiate (imageTwo [randomImageIncorrecta], spawnImageTwo.position, Quaternion.identity);
			break;
		case 2:
			if (numeroNivel > (ultimoNivel - 1)) {
				randomImageIncorrecta = Random.Range (0, (numeroNivel - 1) * cantImagenPorNivel);
			} else {
				randomImageIncorrecta = Random.Range (ultimoElemento, imageOne.Length);
			}
			Instantiate (imageOne [randomImageIncorrecta], spawnImageOne.position, Quaternion.identity);
			Instantiate (imageTwo [randomImagenCorrecta], spawnImageTwo.position, Quaternion.identity);
			break;
		default:
			break;
		}
			
		switch(modojuego){
			case 1:
				break;
			case 2:
				if (imagenCorrecta == 1) {
					imagenCorrecta = 2;
				} else {
					imagenCorrecta = 1;
				}
				break;
			case 3:
			int fulorLess = Random.Range (0, 2);
			Instantiate (icon [fulorLess], spawnIcon.position, Quaternion.identity);
			if(fulorLess == 1){
				if (imagenCorrecta == 1) {
					imagenCorrecta = 2;
				} else {
					imagenCorrecta = 1;
				}
			}

				break;
			default:
				break;
		}

	}

	public int getImagenCorrecta(){
		return imagenCorrecta;
	}

	public int getPuntajeASumar(){
		return puntajeASumar;
	}

	public void setPuntajeASumar(int nuevoPuntajeASumar){
		puntajeASumar = nuevoPuntajeASumar;
	}

	public void setPermitirJugar(bool nuevoPermitirJugar){
		permitirJugar = nuevoPermitirJugar;
	}

	public void volverAjugar(){
		canvasLoading.gameObject.SetActive (true);
		actualizarProgreso (0f);
		StartCoroutine (empezarLoad("escenaJuego"));
	}

	public void volverAlMapa(){
		canvasLoading.gameObject.SetActive (true);
		actualizarProgreso (0f);
		StartCoroutine (empezarLoad("EscenaMundo"));
	}

	public void volverAlMenu(){
		canvasLoading.gameObject.SetActive (true);
		actualizarProgreso (0f);
		StartCoroutine (empezarLoad("MenuScene"));
	}

	private void actualizarProgreso(float progreso){
		textoLoad.text = "Cargando " + ((int)(progreso * 100f)) + " %";
	}

	private IEnumerator empezarLoad(string nombreEscena){
		operacion = SceneManager.LoadSceneAsync (nombreEscena);
		while(!operacion.isDone){
			actualizarProgreso (operacion.progress);
			yield return null;
		}
		actualizarProgreso (operacion.progress);
		operacion = null;
		canvasLoading.gameObject.SetActive (false);

	}

}
