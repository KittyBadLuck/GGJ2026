using MyBox;
using UnityEngine;

public class CursorSwitcher : MonoBehaviour
{
    public MyCursor cursor;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        cursor.ApplyAsFreeCursor();
    }
}
