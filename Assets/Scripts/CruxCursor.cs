using UnityEngine;

public class CruxCursor : MonoBehaviour
{
    [SerializeField] private string[] cruxCode={"z","e","e","s","h","a","n","m","y","g","o","a","t"};
    
    [SerializeField] private Texture2D cursorTex;
    Vector2 hit;
    CursorMode mode=CursorMode.Auto;
    private int index=0;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if(index < cruxCode.Length)
            {
                if (Input.GetKeyDown(cruxCode[index]))
                {
                    index++;
                }
                else
                {
                    index=0;
                }
            }
        }

        if (index == cruxCode.Length)
        {
            Cursor.SetCursor(cursorTex,hit,mode);
        }
    }
}

