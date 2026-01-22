using UnityEngine;

public class OurCursor : MonoBehaviour
{
    [SerializeField] private string[] ourCode={"o","u","r","c","u","r","s","o","r"};
    
    [SerializeField] private Texture2D cursorTex;
    Vector2 hit;
    CursorMode mode=CursorMode.Auto;
    private int index=0;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if(index < ourCode.Length)
            {
                if (Input.GetKeyDown(ourCode[index]))
                {
                    index++;
                }
                else
                {
                    index=0;
                }
            }
        }

        if (index == ourCode.Length)
        {
            Cursor.SetCursor(cursorTex,hit,mode);
        }
    }
}
