using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour {
    public void LoadScene(string levelName)
    {
        Application.LoadLevel(levelName);
    }
}
