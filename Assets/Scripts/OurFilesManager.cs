using UnityEngine;

public class OurFilesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] diaWin;
    [SerializeField] private GameObject mainWin;

    public void nextDialogue(int idx)
    {
        if(idx!=0) diaWin[idx-1].SetActive(false);
        diaWin[idx].SetActive(true);
    }

    public void close()
    {
        mainWin.SetActive(false);
        foreach(GameObject dia in diaWin)
        {
            if (dia.activeSelf)
            {
                dia.SetActive(false);
            }
        }
    }
}
