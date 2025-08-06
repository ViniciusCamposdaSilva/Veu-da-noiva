using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalizarDemo : MonoBehaviour
{
    [SerializeField] private GameObject endScreen;

    public void ShowEndScreen()
    {
        endScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 0f; 
    }

    public void BackHomeScreen()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu Principal");
    }

    public void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
