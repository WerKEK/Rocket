using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadFirstLvl();
        }
    }
    private static void LoadFirstLvl()
    {
        SceneManager.LoadScene(1); 
    }
}
