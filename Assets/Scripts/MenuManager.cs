using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject photon;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartLocalGame()
    {
        SceneManager.LoadScene("BouncingDot");
        menu.SetActive(false);
    }

    public void StartOnlineGame()
    {

    }
}
