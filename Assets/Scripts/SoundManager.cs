using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager> {
	public enum SE {
		Shot,
		Hit,
		OnBush,
		Bound,
		Mud,
		Count,
		Start,
		Finish,
		TitleStart,
		Next,
		Back,
		Cursor,
		Gauge,
		GetItem,
		HeavyBullet,
		SpeedBullet,
		UseItem,
		Connect,
		Disconnect,
		Ash,
	};

	public enum BGM {
		Title,
		MainGame,
		Result,
	};

	List<AudioClip> SEList;
	List<string> SENames;
	List<AudioClip> BGMList;
	List<string> BGMNames;

	[SerializeField] AudioMixerGroup seMixer;
	[SerializeField] AudioMixerGroup bgmMixer;

	AudioSource SEAudioSource;
	AudioSource BGMAudioSource;

	// Start is called before the first frame update
	void Start () {
		var c = GetComponents<AudioSource> ();
		foreach (var a in c) {
			if (a.outputAudioMixerGroup == seMixer) {
				SEAudioSource = a;
			}
			else if (a.outputAudioMixerGroup == bgmMixer) {
				BGMAudioSource = a;
			}
		}

		// SEの読み込み
		SENames = Enum.GetNames ( typeof ( SE ) ).ToList ();
		SEList = new List<AudioClip> ();
		foreach (var n in SENames) {
			SEList.Add ( (AudioClip)Resources.Load ( "Prefabs/SE/" + n ) );
		}

		// BGMの読み込み
		BGMNames = Enum.GetNames ( typeof ( BGM ) ).ToList ();
		BGMList = new List<AudioClip> ();
		foreach (var n in BGMNames) {
			BGMList.Add ( (AudioClip)Resources.Load ( "Prefabs/BGM/" + n ) );
		}
	}

	public void PlaySE ( SE se ) {
		var s = se.ToString ();
		var i = SENames.FindIndex ( x => x == s );
		SEAudioSource.PlayOneShot ( SEList[i] );
	}

	public void PlayBGM ( BGM bgm ) {
		var s = bgm.ToString ();
		var i = BGMNames.FindIndex ( x => x == s );
		BGMAudioSource.clip = BGMList[i];
		BGMAudioSource.Play ();
	}
}
