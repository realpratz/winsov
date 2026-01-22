using UnityEngine;
using UnityEngine.SceneManagement;

public class OnKeyPressLoad : MonoBehaviour
{
    [SerializeField] private string sceneName;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) SceneManager.LoadScene(sceneName);
    }
}
