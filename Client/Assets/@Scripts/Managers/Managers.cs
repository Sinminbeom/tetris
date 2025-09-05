using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    public static bool Initialized { get; set; }
    private static Managers s_instance; // 유일성이 보장된다
    public static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니저를 갖고온다

    #region Contents

    //private BoardManager _board = new BoardManager();
    private SingleBoardManager _singleBoard = new SingleBoardManager();
    private MyBoardManager _myBoard = new MyBoardManager();
    private EnemyBoardManager _enemyBoard = new EnemyBoardManager();

    //private BackgroundManager _background = new BackgroundManager();
    private SingleBackgroundManager _singleBackground = new SingleBackgroundManager();
    private MultiBackgroundManager _multiBackground = new MultiBackgroundManager();

    //private ObjectManager _object = new ObjectManager();
    private SingleObjectManager _singleObject = new SingleObjectManager();
    private MultiObjectManager _multiObject = new MultiObjectManager();

    //public static BoardManager Board { get { return Instance?._board; } }
    public static SingleBoardManager SingleBoard { get { return Instance?._singleBoard; } }
    public static MyBoardManager MyBoard { get { return Instance?._myBoard; } }
    public static EnemyBoardManager EnemyBoard { get { return Instance?._enemyBoard; } }


    //public static BackgroundManager Background { get { return Instance?._background; } }
    public static SingleBackgroundManager SingleBackground { get { return Instance?._singleBackground; } }
    public static MultiBackgroundManager MultiBackground { get { return Instance?._multiBackground; } }

    //public static ObjectManager Object { get { return Instance?._object; } }
    public static SingleObjectManager SingleObject { get { return Instance?._singleObject; } }
    public static MultiObjectManager MultiObject { get { return Instance?._multiObject; } }

    #endregion

    #region Core

    private DataManager _data = new DataManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private UIManager _ui = new UIManager();

    public static DataManager Data { get { return Instance?._data; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static SceneManagerEx Scene { get { return Instance?._scene; } }
    public static UIManager UI { get { return Instance?._ui; } }

    #endregion

    public static void Init()
    {
        if (s_instance == null && Initialized == false)
        {
            Initialized = true;

            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
    // Update is called once per frame
    public void Update()
    {
        
    }

    public static void Clear()
    {
        UI.Clear();
    }
}
