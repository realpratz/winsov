using UnityEngine;

public class MusicFolderManager : MonoBehaviour
{
    [SerializeField] private GameObject mainWin;
    [SerializeField] private GameObject ampWin;

    public void close()
    {
        mainWin.SetActive(false);
        ampWin.SetActive(false);
    }

    public void winAmp()
    {
        ampWin.SetActive(true);
    }
}
