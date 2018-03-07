using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static bool GamePause;

    public void NewGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Continue()
    {

        string Json = File.ReadAllText(string.Format("{0}{1}", FSaveData.FullPath, FSaveData.FileName));
        FSaveData datos = JsonUtility.FromJson<FSaveData>(Json);

        SimonBelmont.bRestoreLocation = true;
        SimonBelmont.RestoreLocation = datos.Position;

        SceneManager.LoadScene(datos.SceneIndex, LoadSceneMode.Single);

    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
