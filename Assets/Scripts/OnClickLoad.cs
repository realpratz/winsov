using UnityEngine;
using UnityEngine.SceneManagement;

public class OnClickLoad : MonoBehaviour
{
    [SerializeField] private string sceneName;

    public void Load()
    {
        SceneManager.LoadScene(sceneName);
    }
}
