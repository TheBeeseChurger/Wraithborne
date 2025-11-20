using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldManager : MonoBehaviour
{
    public static OverworldManager Instance;

    public Vector3 overworldPlayerPosition;
    public Quaternion overworldPlayerRotation;
    public Scene overworldPlayerScene;
    public bool hasSavedPostion = false;

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
        if (hasSavedPostion) SceneManager.LoadScene(overworldPlayerScene.buildIndex);
        else Debug.LogError("ERROR! No saved overworld to return to!");
    }
}
