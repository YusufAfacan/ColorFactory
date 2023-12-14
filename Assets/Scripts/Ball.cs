using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //public Transform targetCircle;
    public bool isGrabbed;
    public enum BallType {Red, Green, Blue, Black, White, Yellow, Magenta, Cyan}
    public BallType ballType;
    public SpriteRenderer spriteRenderer;

    public Sprite redBall;
    public Sprite greenBall;
    public Sprite blueBall;
    public Sprite blackBall;
    public Sprite whiteBall;
    public Sprite yellowBall;
    public Sprite magentaBall;
    public Sprite cyanBall;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


}
