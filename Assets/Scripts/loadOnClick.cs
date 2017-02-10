using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadOnClick : MonoBehaviour {

	public void LoadScene(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }
}
