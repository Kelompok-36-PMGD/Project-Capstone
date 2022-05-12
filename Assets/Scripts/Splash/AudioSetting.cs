using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioSetting : MonoBehaviour
{
	public AudioSource Audiosource;
	private float musicVolume = 1f;

	void Start()
	{
		Audiosource.Play();
	}

	void Update()
    {
		Audiosource.volume = musicVolume;
    }
	
	public void updateVolume(float volume)
    {
		musicVolume = volume;

    } 

}
