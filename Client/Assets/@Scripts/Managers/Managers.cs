using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    public static bool Initialized { get; set; }
    private static Managers s_instance; // ���ϼ��� ����ȴ�
    public static Managers Instance { get { Init(); return s_instance; } } // ������ �Ŵ����� ����´�

    #region Contents

    private BoardManager _board = new BoardManager();
    private BackgroundManager _background = new BackgroundManager();
    private ObjectManager _object = new ObjectManager();

    public static BoardManager Board { get { return Instance?._board; } }
    public static BackgroundManager Background { get { return Instance?._background; } }
    public static ObjectManager Object { get { return Instance?._object; } }

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
