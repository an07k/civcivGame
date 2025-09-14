using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        CursorUnvisible();
    }

    public void CursorVisible()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void CursorUnvisible()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
