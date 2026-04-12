using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnPlayButtonClicked()
    {
        SceneManager.LoadScene(1);
    }
    public void OnMainmenuButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
