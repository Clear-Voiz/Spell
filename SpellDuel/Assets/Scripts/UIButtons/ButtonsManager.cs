using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsManager : MonoBehaviour
{
    public GameObject startButt;
    public GameObject bookButt;
    public GameObject shopButt;
    public GameObject configButt;
    public GameObject exitButt;
    public GameObject creditsButt;


    public void onBookClick()
    {
        SceneManager.LoadScene("Grimoire");
    }

    public void onStartButt()
    {
        SceneManager.LoadScene("Abyss");
    }
}
