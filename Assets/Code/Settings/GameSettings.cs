using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class GameSettings
{

    public static GameSettings gs = new GameSettings();

    //GAMEMOUSE
    public int fire = 0;

    //GAMEKEYS
    public KeyCode run = KeyCode.LeftShift;
    //public KeyCode sneak = KeyCode.LeftShift;

    public KeyCode jump = KeyCode.Space;

    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;

    //SYSTEMKEYS
    public KeyCode resetpos = KeyCode.X;
    public KeyCode perspective = KeyCode.R;

    //VALUES
    public float mouseS = 200f;
    

}
