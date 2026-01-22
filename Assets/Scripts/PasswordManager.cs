using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PasswordManager : MonoBehaviour
{
    private string PlayerPass;
    [SerializeField] private string sceneToLoad;
    [SerializeField] private GameObject passPanel;
    [SerializeField] private TMP_InputField passText;
    [SerializeField] private TMP_InputField inputText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // clearPass(); //<--for testing purposes
        checkPass();
    }

    void checkPass()
    {
        if(PlayerPrefs.HasKey(PlayerPass)){
            passPanel.SetActive(false);
        }
        else{
            passPanel.SetActive(true);
        }
    }

    public void savePass()
    {
        if (!string.IsNullOrEmpty(passText.text))
        {
            PlayerPrefs.SetString(PlayerPass,passText.text);
            PlayerPrefs.Save();
            passPanel.SetActive(false);
        }
    }

    public void login()
    {
        if(inputText.text==PlayerPrefs.GetString(PlayerPass)){
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    public void clearPass()
    {
        PlayerPrefs.DeleteKey(PlayerPass);
    }
}
