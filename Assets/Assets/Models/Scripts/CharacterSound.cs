using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSound : MonoBehaviour
{
    private AudioSource soundFX;

    [SerializeField]
    private AudioClip attack_sound1,attack_sound2,die_sound;
    // Start is called before the first frame update
    void Awake()
    {
        soundFX=GetComponent<AudioSource>();
    }

    public void Attack_1()
    {
        soundFX.clip = attack_sound1;
        soundFX.Play();
    }
    public void Attack_2()
    {
        soundFX.clip = attack_sound2;
        soundFX.Play();
    }
    public void Die()
    {
        soundFX.clip = die_sound;
        soundFX.Play();
    }
}
