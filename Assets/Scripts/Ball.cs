using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Transform targetCircle;
    public bool isGrabbed;
    public enum BallType {Red, Green, Blue, Black, White}
    public BallType ballType;
    public SpriteRenderer spriteRenderer;

    public Sprite redBall;
    public Sprite greenBall;
    public Sprite blueBall;
    public Sprite blackBall;
    public Sprite whiteBall;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


}
