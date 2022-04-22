using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundControl : MonoBehaviour {

	public static AudioClip getPoint;
	public static AudioClip getStar;
	public static AudioClip correct;
	public static AudioClip wrong;
	public static AudioSource audioSrc;
	// Use this for initialization
	void Start () {
		getPoint = Resources.Load<AudioClip> ("getCoin");
		getStar = Resources.Load<AudioClip> ("win");
		correct = Resources.Load<AudioClip> ("correct");
		wrong = Resources.Load<AudioClip> ("wrong");
		audioSrc = GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void playSoudPoint(){
		audioSrc.PlayOneShot (getPoint,0.15f);
	}
	public static void playSoundStar(){
		audioSrc.PlayOneShot (getStar,0.15f);
	}

	public static void playSoundWrong(){
		audioSrc.PlayOneShot (wrong);
	}

	public static void playSoundCorrect(){
		audioSrc.PlayOneShot (correct);
	}

}
