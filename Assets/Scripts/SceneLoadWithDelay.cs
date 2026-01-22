using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadWithDelay : MonoBehaviour
{
    [SerializeField] private float timeToSwitch=10f;
    [SerializeField] private string sceneName;
    private float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        timeElapsed+=Time.deltaTime;

        if (timeElapsed > timeToSwitch)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
