using UnityEngine;
using System.Collections.Generic;

public class WindowManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> allWindows;

    public void openWindow(GameObject winToOpen)
    {
        foreach(GameObject win in allWindows)
        {
            if (win == winToOpen)
            {
                win.SetActive(true);
            }
            else
            {
                win.SetActive(false);
            }
        }
    }

    public void closeWindows()
    {
        foreach(GameObject win in allWindows)
        {
            win.SetActive(false);
        }
    }
}
