using UnityEngine;

public class Quit : MonoBehaviour
{
    [SerializeField] private float timeToQuit=10f;
    private float timeElapsed;

    // Update is called once per frame
    void Update()
    {
        timeElapsed+=Time.deltaTime;

        if (timeElapsed > timeToQuit)
        {
            Application.Quit();
            Debug.Log("App has been quit");
        }
    }
}
