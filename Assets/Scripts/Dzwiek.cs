using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Dzwiek 
{
	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = 1;

	[Range(0f, 3f)]
	public float pitch = 1;

	public bool loop = false;

	[HideInInspector]
	public AudioSource source;
}
