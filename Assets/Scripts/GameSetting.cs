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

    public struct PlayerData {
        bool join;
        public bool Join {
            get { return join; }
            set { join = value; }
        }
        string horizontal;
        public string Horizontal {
            get { return horizontal; }
        }
        string vertical;
        public string Vertical {
            get { return vertical; }
        }
        string shot;
        public string Shot {
            get { return shot; }
        }
        string item;
        public string Item {
            get { return item; }
        }

        public PlayerData( bool join_, string horizontal_, string vertical_, string shot_, string item_ ) {
            join = join_;
            horizontal = horizontal_;
            vertical = vertical_;
            shot = shot_;
            item = item_;
        }
    }
    static PlayerData[] buttonBinds = new PlayerData[4];
    public PlayerData[] ButtonBinds {
        get { return buttonBinds; }
        set { buttonBinds = value; }
    }

    // Start is called before the first frame update
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    public int GetJoinedPlayers () {
        int t = 0;
        foreach(PlayerData temp in buttonBinds ) {
            if(temp.Join == true ) {
                t++;
            }
        }

        return t;
    }
}
