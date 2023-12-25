using DG.Tweening;
using System;
using UnityEngine;

public class PistonArm : MonoBehaviour
{
    private Vector2 startPos;
    private bool isBeingHeld = false;
    public bool isRectracted;
    public bool isExtracted;
    public bool isGrabbed;
    public bool isReleased;
    public bool isDeclared;
    public Transform arm;
    public Transform gripper;
    public Transform grabbingPoint;
    public Ball grabbedBall;
    public CommandLines commandLines;
    public CommandLine commandLine;
    public TileManager tileManager;
    public AudioClip audioClip;
    public SoundManager soundManager;
    public GameManager gameManager;
   
    private void Awake()
    {
        startPos = transform.position;
        commandLines = FindObjectOfType<CommandLines>();
        tileManager = FindObjectOfType<TileManager>();
        soundManager = FindObjectOfType<SoundManager>();
        gameManager = FindObjectOfType<GameManager>();
    }
    private void OnMouseDown()
    {
        if (!gameManager.canPlay) return;

        if(Input.GetMouseButtonDown(0))
        {
            isBeingHeld = true;
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.GetComponent<Tile>())
                {
                    if (hit.transform.GetComponent<Tile>().isOccupied)
                    {
                        
                        hit.transform.GetComponent<Tile>().isOccupied = false;
                    }

                    
                }
            }
        }
    }
    private void Update()
    {
        if (!gameManager.canPlay) return;

        if (isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(mousePos.x, mousePos.y, 0);
        }
    }
    private void OnMouseUp()
    {
        if (!gameManager.canPlay) return;

        isBeingHeld = false;
        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if(hit.transform.GetComponent<Tile>())
            {
                if (!hit.transform.GetComponent<Tile>().isOccupied)
                {
                    transform.position = hit.transform.position;
                    hit.transform.GetComponent<Tile>().isOccupied = true;
                    isDeclared = true;
                }
                else
                {
                    transform.position = startPos;
                    isDeclared = false;
                }
                    
            }
            else
            {
                transform.position = startPos;
                isDeclared = false;
            }
        }
        else
        {
            transform.position = startPos;
            isDeclared = false;
        }  
    }

    public void Clockwise()
    {
        Debug.Log("Clockwise");
        Vector3 armRot = transform.rotation.eulerAngles;
        Vector3 rot = new Vector3(0, 0, 90);
        transform.DORotate(armRot + rot, 1, RotateMode.Fast).OnComplete(() =>
        { if (grabbedBall != null) { grabbedBall.CheckConverter(); grabbedBall.CheckStar(); }; });
        commandLines.totalNumberOfMoves++;
        soundManager.PlayAudioClip(audioClip);
    }

    

    public void CounterClockwise()
    {
        Debug.Log("CounterClockwise");
        Vector3 armRot = transform.rotation.eulerAngles;
        Vector3 rot = new Vector3(0, 0, -90);
        transform.DORotate(armRot + rot, 1, RotateMode.Fast).OnComplete(() =>
        { if (grabbedBall != null) { grabbedBall.CheckConverter(); grabbedBall.CheckStar(); }; });
        commandLines.totalNumberOfMoves++;
        soundManager.PlayAudioClip(audioClip);
    }


    public void Rectract()
    {
        Debug.Log("Rectract");
        if (!isRectracted)
        {
            arm.DOScaleY(3, 1);
            gripper.DOLocalMoveY(3.5f, 1);
            grabbingPoint.DOLocalMoveY(4, 1).OnComplete(() =>
            { if (grabbedBall != null) { grabbedBall.CheckConverter(); grabbedBall.CheckStar(); }; });
            commandLines.totalNumberOfMoves++;
            soundManager.PlayAudioClip(audioClip);
        }
    }

    public void Extract()
    {
        Debug.Log("Extract");
        if (!isExtracted)
        {
            arm.DOScaleY(7, 1);
            gripper.DOLocalMoveY(7.5f, 1);
            grabbingPoint.DOLocalMoveY(8, 1).OnComplete(() =>
            { if (grabbedBall != null) { grabbedBall.CheckConverter(); grabbedBall.CheckStar(); }; });
            commandLines.totalNumberOfMoves++;
            soundManager.PlayAudioClip(audioClip);
        }
    }

    public void Grab()
    {
        Debug.Log("Grab");

        gripper.DOScaleX(0.5f, 1);
        soundManager.PlayAudioClip(audioClip);

        for (int i = 0; i < commandLines.balls.Count; i++)
        {
            if (grabbingPoint.transform.position == commandLines.balls[i].transform.position)
            {
                Ball ball = commandLines.balls[i];
                grabbedBall = ball;

                if (ball.isGrabbed)
                {
                    Debug.Log("BALL IS GRABBED BY ANOTHER ARM");

                    commandLine.nextCommandIndex++;

                    if (commandLine.nextCommandIndex >= 10) commandLine.nextCommandIndex = 0;


                }
                else
                {
                    ball.isGrabbed = true;
                    ball.grabbingArm = this;
                    ball.transform.SetParent(grabbingPoint.transform);
                }

            }

        }
    }

    public void Release()
    {
        Debug.Log("Release");
        soundManager.PlayAudioClip(audioClip);
        if (!isReleased)
        {
            if (grabbedBall != null)
            {
                grabbedBall.transform.SetParent(null);
                grabbedBall.isGrabbed = false;
                grabbedBall.grabbingArm = null;
            }

            gripper.DOScaleX(0.3f, 1);
        }
    }

    

    public void None()
    {

        Debug.Log("None");
    }

}
