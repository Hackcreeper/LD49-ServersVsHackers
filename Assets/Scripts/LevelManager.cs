using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private void CleanSingletons()
    {
        FieldGenerator.Instance.Clean();
    }

    public void LoadLevel(int levelId)
    {
        SceneManager.LoadSceneAsync($"Level{levelId}", LoadSceneMode.Additive);
    }
}