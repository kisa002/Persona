using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour {

    public AudioClip[] sound = new AudioClip[15];
    public AudioSource[] source = new AudioSource[15];
    
	void Start ()
    {
        sound[0] = Resources.Load<AudioClip>("Sounds/BGM/2");
        sound[1] = Resources.Load<AudioClip>("Sounds/BGM/3");
        sound[2] = Resources.Load<AudioClip>("Sounds/Effects/03_result");
        sound[3] = Resources.Load<AudioClip>("Sounds/Effects/04_button");
        sound[4] = Resources.Load<AudioClip>("Sounds/Effects/05_brushswing");
        sound[5] = Resources.Load<AudioClip>("Sounds/Effects/06_throwpaint");
        sound[6] = Resources.Load<AudioClip>("Sounds/Effects/07_dash");
        sound[7] = Resources.Load<AudioClip>("Sounds/Effects/08_painted");
        sound[8] = Resources.Load<AudioClip>("Sounds/Effects/09_timeover");
        sound[9] = Resources.Load<AudioClip>("Sounds/Effects/10_knockback");
        sound[10] = Resources.Load<AudioClip>("Sounds/Effects/11_floorpaint");
        sound[11] = Resources.Load<AudioClip>("Sounds/Effects/12_getitem");
        sound[12] = Resources.Load<AudioClip>("Sounds/Effects/13_scream1");
        sound[13] = Resources.Load<AudioClip>("Sounds/Effects/14_scream2");

        sound[14] = Resources.Load<AudioClip>("Sounds/BGM/15");
        
        for (int i = 0; i < sound.Length; i++)
        {
            source[i] = gameObject.AddComponent<AudioSource>() as AudioSource;
            source[i].clip = sound[i];
        }

        source[0].loop = true;
        source[1].loop = true;

        switch (SceneManager.GetActiveScene().name)
        {
            case "Main":
                BgmTitle();
                break;

            case "GameScene":
                BgmIngame();
                break;
        }
    }
    
    public void BgmTitle()
    {
        source[0].Play();

        source[1].Stop();
        source[2].Stop();
        source[14].Stop();
    }

    public void BgmIngame()
    {
        source[1].Play();

        source[0].Stop();
        source[2].Stop();
        source[14].Stop();
    }

    public void BgmCredit()
    {
        source[14].Play();

        source[0].Stop();
        source[1].Stop();
        source[2].Stop();
    }

    public void BgmResult()
    {
        source[2].Play();

        source[0].Stop();
        source[1].Stop();
        source[14].Stop();
    }

    public void PlaySound(int code)
    {
        source[code - 1].Play();
    }
}
