using UnityEngine;

/*GameInstance non viene mai distrutta durante il runtime (eredita da Singleton)
 *e definisce una serie di proprietà generali a cui possono accedere tutte 
 *le altre classi
 */
public class GameInstance : Singleton<GameInstance>
{
    public static Texture2D[] CursorIcon = new Texture2D[3];
    public static bool isMusicPlaying;
    public static bool isSfxPlaying;

    void Start()
    {
        SetMouseCursor();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickCursor();

        if (Input.GetMouseButtonUp(0))
            HandCursor();
    }

    void SetMouseCursor()
    {
        var handCursor = (Texture2D)Resources.Load("MouseCursor/hand");
        var clickCursor = (Texture2D)Resources.Load("MouseCursor/click");
        var grabCursor = (Texture2D)Resources.Load("MouseCursor/grab");

        if (handCursor && clickCursor && grabCursor)
        {
            CursorIcon[0] = Resize(handCursor, 45, 45);
            CursorIcon[1] = Resize(clickCursor, 45, 45);
            CursorIcon[2] = Resize(grabCursor, 45, 45);
            Cursor.SetCursor(CursorIcon[0], Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public static void HandCursor()
    {
        if (CursorIcon[0])
        {
            Cursor.SetCursor(CursorIcon[0], Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public static void ClickCursor()
    {
        if (CursorIcon[1])
        {
            Cursor.SetCursor(CursorIcon[1], Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    public static void GrabCursor()
    {
        if (CursorIcon[2])
        {
            Cursor.SetCursor(CursorIcon[2], Vector2.zero, CursorMode.ForceSoftware);
        }
    }

    Texture2D Resize(Texture2D texture2D, int targetX, int targetY)
    {
        RenderTexture rt = new RenderTexture(targetX, targetY, 24);
        RenderTexture.active = rt;
        Graphics.Blit(texture2D, rt);
        Texture2D result = new Texture2D(targetX, targetY);
        result.ReadPixels(new Rect(0, 0, targetX, targetY), 0, 0);
        result.Apply();
        return result;
    }
}
