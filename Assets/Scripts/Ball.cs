using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool isGrabbed;
    public PistonArm grabbingArm;
    public enum BallType {Red, Green, Blue, Black, White, Yellow, Magenta, Cyan}
    public BallType ballType;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;

    private TileManager tileManager;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileManager = FindObjectOfType<TileManager>();
    }

    public void CheckConverter()
    {
        for (int i = 0; i < tileManager.tiles.Count; i++)
        {
            if (transform.position == tileManager.tiles[i].transform.position)
            {
                if (tileManager.tiles[i].isOccupied)
                {
                    if (tileManager.tiles[i].occupyingConverter != null)
                    {
                        switch (tileManager.tiles[i].occupyingConverter.converterType)
                        {
                            case Converter.ConverterType.YingYang:
                                switch (ballType)
                                {
                                    case BallType.Black:
                                        ChangeBallColorTo(Color.white);
                                        break;
                                    case BallType.White:
                                        ChangeBallColorTo(Color.black);
                                        break;
                                }
                                break;
                            case Converter.ConverterType.RGB:
                                switch (ballType)
                                {
                                    case BallType.Red:
                                        ChangeBallColorTo(Color.blue);
                                        break;
                                    case BallType.Green:
                                        ChangeBallColorTo(Color.red);
                                        break;
                                    case BallType.Blue:
                                        ChangeBallColorTo(Color.green);
                                        break;
                                }
                                break;
                            case Converter.ConverterType.ColorMerger:
                                if (!tileManager.tiles[i].occupyingConverter.isOccupyingBall)
                                {
                                    tileManager.tiles[i].occupyingConverter.occupyingBall = this;
                                    tileManager.tiles[i].occupyingConverter.isOccupyingBall = true;
                                }
                                else if (tileManager.tiles[i].occupyingConverter.isOccupyingBall)
                                {
                                    Ball occupyingBall = tileManager.tiles[i].occupyingConverter.occupyingBall;

                                    switch (ballType)
                                    {
                                        case BallType.Red:
                                            switch (tileManager.tiles[i].occupyingConverter.occupyingBall.ballType)
                                            {
                                                case BallType.Green:
                                                    occupyingBall.ChangeBallColorTo(Color.yellow);
                                                    grabbingArm.grabbedBall = null;
                                                    gameObject.SetActive(false);
                                                    break;
                                                case BallType.Blue:
                                                    occupyingBall.ChangeBallColorTo(Color.magenta);
                                                    grabbingArm.grabbedBall = null;
                                                    gameObject.SetActive(false);
                                                    break;

                                            }
                                            break;

                                        case BallType.Green:
                                            switch (tileManager.tiles[i].occupyingConverter.occupyingBall.ballType)
                                            {
                                                case BallType.Red:
                                                    occupyingBall.ChangeBallColorTo(Color.yellow);
                                                    grabbingArm.grabbedBall = null;
                                                    gameObject.SetActive(false);
                                                    break;

                                                case BallType.Blue:
                                                    occupyingBall.ChangeBallColorTo(Color.cyan);
                                                    grabbingArm.grabbedBall = null;
                                                    gameObject.SetActive(false);
                                                    break;

                                            }
                                            break;

                                        case BallType.Blue:
                                            switch (tileManager.tiles[i].occupyingConverter.occupyingBall.ballType)
                                            {
                                                case BallType.Red:
                                                    occupyingBall.ChangeBallColorTo(Color.magenta);
                                                    grabbingArm.grabbedBall = null;
                                                    gameObject.SetActive(false);
                                                    break;
                                                case BallType.Green:
                                                    occupyingBall.ChangeBallColorTo(Color.cyan);
                                                    grabbingArm.grabbedBall = null;
                                                    gameObject.SetActive(false);
                                                    break;
                                            }
                                            break;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

    }

    public void ChangeBallColorTo(Color color)
    {
        spriteRenderer.color = color;

        if (color == Color.red)
        {
            ballType = BallType.Red;
        }
        if (color == Color.green)
        {
            ballType = BallType.Green;
        }
        if (color == Color.blue)
        {
            ballType = BallType.Blue;
        }
        if (color == Color.cyan)
        {
            ballType = BallType.Cyan;
        }
        if (color == Color.magenta)
        {
            ballType = BallType.Magenta;
        }
        if (color == Color.yellow)
        {
            ballType = BallType.Yellow;
        }
        if (color == Color.black)
        {
            ballType = BallType.Black;
        }
        if (color == Color.white)
        {
            ballType = BallType.White;
        }

    }
}
