using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageAppearScript : MonoBehaviour {



	private bool destruir = false;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if(TimerScript.instance.getTiempoAgotadoReloj()){
			Destroy (gameObject);
		}
	}

	public void setDestruit(bool permitirDestruir){
		destruir = permitirDestruir;
	}

	public bool getDestruir(){
		return destruir;
	}

}
