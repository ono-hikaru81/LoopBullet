using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour {
    public enum Stars {
        Jimejime,
        Moon,
        Magmag,
    };
    static Stars star;
    public Stars Star {
        get { return star; }
        set { star = value; }
    }

    public enum Scenes {
        Title,
        Menu,
        Option,
        Connect,
        Stage,
    };
    static Scenes scene = Scenes.Title;
    public Scenes Scene {
        get { return scene; }
        set { scene = value; }
    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }
}
