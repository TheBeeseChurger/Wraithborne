using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldManager : MonoBehaviour
{
    public static OverworldManager Instance;

    public Vector3 overworldPlayerPosition;
    public Quaternion overworldPlayerRotation;
    public int overworldPlayerScene;
    public bool hasSavedPostion = false;

    [Header("Inspector Variables")]
    public DeckData playerDeck;
    public FrameMaker frameMaker;

    public const int TABLE_SCENE_INDEX = 1;

    private PlayerController _playerController;
    private PlayerCameraController _playerCameraController;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ReturnToOverworld()
    {
        if (hasSavedPostion) SceneManager.LoadScene(overworldPlayerScene);
        else Debug.LogError("ERROR! No saved overworld to return to!");
    }

    public void StartCardBattle(DeckData enemyDeck, MapData mapData)
    {
        Save();

        if (MatchSession.CurrentMatch == null)
        {
            MatchSession.StartMatch(mapData, playerDeck, enemyDeck, frameMaker);
        }

        SceneManager.LoadScene(TABLE_SCENE_INDEX);
    }

    public void Save()
    {
        var position = _playerController.TriggerSave();
        var rotation = _playerCameraController.TriggerSave();
        var scene = SceneManager.GetActiveScene().buildIndex;

        overworldPlayerPosition = position;
        overworldPlayerRotation = rotation;
        overworldPlayerScene = scene;

        hasSavedPostion = true;
    }

    public void ClearSave()
    {
        overworldPlayerPosition = Vector3.zero;
        overworldPlayerRotation = Quaternion.identity;
        overworldPlayerScene = 0;

        hasSavedPostion = false;
    }

    public void Register(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Register(PlayerCameraController playerCameraController)
    {
        _playerCameraController = playerCameraController;
    }
}
