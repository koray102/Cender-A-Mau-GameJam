using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("FirstPlaceSpace");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
