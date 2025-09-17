using UnityEngine;
using UnityEngine.SceneManagement;

public class Managers : MonoBehaviour
{
    public static bool Initialized { get; set; }
    private static Managers s_instance; // ���ϼ��� ����ȴ�
    public static Managers Instance { get { Init(); return s_instance; } } // ������ �Ŵ����� ����´�

    #region Contents

    private GameRoomManager _room = new GameRoomManager();

    public static GameRoomManager GameRoom { get { return Instance?._room; } }

    private ObjectManager _object = new ObjectManager();

    public static ObjectManager Object { get { return Instance?._object; } }

    #endregion

    #region Core

    private DataManager _data = new DataManager();
    private PoolManager _pool = new PoolManager();
    private ResourceManager _resource = new ResourceManager();
    private SceneManagerEx _scene = new SceneManagerEx();
    private UIManager _ui = new UIManager();
    private NetworkManager _network = new NetworkManager();

    public static DataManager Data { get { return Instance?._data; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static SceneManagerEx Scene { get { return Instance?._scene; } }
    public static UIManager UI { get { return Instance?._ui; } }
    public static NetworkManager Network { get { return Instance?._network; } }

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
        _network?.Update();
    }

    public static void Clear()
    {
        UI.Clear();
    }
}
