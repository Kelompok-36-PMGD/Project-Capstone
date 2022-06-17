using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    public static PlayerSound instance;
    private float musicVolume = 1f;
    public AudioSource source;

    [Header("Sound")]
    public AudioSource walk;
    public AudioSource run;
    [Header("SFX")]
    public AudioClip jump;
    public AudioClip skill1;
    public AudioClip skill2;
    public AudioClip skill3;
    public AudioClip drinkPotion;
    public AudioClip getKey;
    public AudioClip deathSound;
    public AudioClip enemyHit;
    public AudioClip openChest;

    [Header("MenuSFX")]
    public AudioClip clickButton;
    public AudioClip clickPause;
    public AudioClip buySkill;

    [Header("ButoIjo")]
    public AudioClip butoIjoAttack;

    [Header("Banaspati")]
    public AudioClip banaspatiAttack;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
    void Update()
    {
        source.volume = musicVolume;
    }

    public void updateVolume(float volume)
    {
        musicVolume = volume;
    }
    public void PlayWalk(bool status)
    {
        if (status && !walk.isPlaying) walk.Play();
        else if(!status)walk.Stop();
    }
    public void PlayRun(bool status)
    {
        if (status && !run.isPlaying) run.Play();
        else if(!status)run.Stop();
    }

    public void JumpSound()
    {
        source.PlayOneShot(jump);
    }

    public void Skill1Sound()
    {
        source.PlayOneShot(skill1);
    }
    public void Skill2Sound()
    {
        source.PlayOneShot(skill2);
    }
    public void Skill3Sound()
    {
        source.PlayOneShot(skill3);
    }

    public void DrinkSound()
    {
        source.PlayOneShot(drinkPotion);
    }

    public void PickKeySound()
    {
        source.PlayOneShot(getKey);
    }

    public void DeathSound()
    {
        source.PlayOneShot(deathSound);
    }

    public void OpenChestSound()
    {
        source.PlayOneShot(openChest);
    }

    public void ClickButtonSound()
    {
        source.PlayOneShot(clickButton);
    }

    public void pauseButtonSound()
    {
        source.PlayOneShot(clickPause);
    }

    public void BuySkillSound()
    {
        source.PlayOneShot(buySkill);
    }

    public void ButoIjoAttack()
    {
        source.PlayOneShot(butoIjoAttack);
    }

    public void BanaspatiBullet()
    {
        source.PlayOneShot(banaspatiAttack);
    }
}
