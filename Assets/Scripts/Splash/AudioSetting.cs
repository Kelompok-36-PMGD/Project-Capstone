using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioSetting : MonoBehaviour
{
	public AudioSource Audiosource;
	private float musicVolume = 1f;
	private float sfxVolume = 1f;

	void Start()
	{
		Audiosource.Play();
	}

	void Update()
    {
		Audiosource.volume = musicVolume;
		PlayerSound.instance.source.volume = sfxVolume;

	}
	
	public void updateVolume(float volume)
    {
		musicVolume = volume;

    } 

	public void updateSFX(float volume)
    {
		sfxVolume = volume;
    }
}
