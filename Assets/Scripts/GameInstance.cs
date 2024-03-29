﻿using System.Collections.Generic;
using UnityEngine;

/*GameInstance non viene mai distrutta durante il runtime (eredita da Singleton)
 *e definisce una serie di proprietà generali a cui possono accedere tutte 
 *le altre classi
 */
public class Move
{
    public Card card;
    public TableManager tableManager;
    public List<Card> cardMatrix; //se la carta è nella matrice la rimetto nel mazzetto
    public int deckIndex;
    public Vector3 oldPosition;
}
public class GameInstance : Singleton<GameInstance>
{
    public static Texture2D[] CursorIcon = new Texture2D[3];
    public static Card principalCard;
    public static Move previousMove = new Move();

    public static bool isMusicPlaying;
    public static bool isSfxPlaying;
    public static bool isHighQuality;
    public static bool isTutorialMode;
    
    public static bool isFirstRuleSeen = false;
    public static bool isSecondRuleSeen = false;
    public static bool isThirdRuleSeen = false;

    void Start()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = 30;
        SetMouseCursor();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            ClickCursor();

        if (Input.GetMouseButtonUp(0))
            HandCursor();

        //esci col tasto back da mobile
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
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

    public static void ToggleSfx()
    {
        isSfxPlaying = !isSfxPlaying;
        FindObjectOfType<ControlPanel>()?.ToggleSfxIcon(isSfxPlaying);
    }

    public static void ToggleMusic()
    {
        isMusicPlaying = !isMusicPlaying;
        FindObjectOfType<MusicSingleton>()?.PlayMusic(isMusicPlaying);
        
        FindObjectOfType<ControlPanel>()?.ToggleMusicIcon(isMusicPlaying);//btn musica menù principale
        FindObjectOfType<AudioButton>()?.ToggleMusicIcon(isMusicPlaying); //btn audio tavolo da gioco
    }
    public static void ToggleQuality()
    {
        isHighQuality = !isHighQuality;
        FindObjectOfType<ControlPanel>()?.ToggleLowHighIcon(isHighQuality);
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
