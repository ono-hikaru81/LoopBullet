using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component {
	static T instance;
	public static T Instance {
		get {
			if (instance == null) {
				// 同じタイプのオブジェクトを探す
				instance = FindObjectOfType<T> ();

				if (instance == null) {
					// 同じタイプのオブジェクトが見つからなければ作成する
					GameObject o = new GameObject ();
					o.name = typeof ( T ).Name;
					instance = o.AddComponent<T> ();
				}
			}

			return instance;
		}
	}

	public virtual void Awake () {
		if (instance == null) {
			instance = this as T;
			DontDestroyOnLoad ( gameObject );
		}
		else {
			// 複数ある場合は消す
			Destroy ( gameObject );
		}
	}
}