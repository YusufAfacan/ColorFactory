using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converter : MonoBehaviour
{
    public enum ConverterType { RGB, YingYang, ColorMerger };
    public ConverterType converterType;

    private Vector2 startPos;
    private bool isBeingHeld = false;
    private bool isDeclared = false;

    public GameObject prefabOfItself;
    public Ball occupyingBall;
    public bool isOccupyingBall;
    public bool isMoveble;
    public GameManager gameManager;

    private void Awake()
    {
        startPos = transform.position;
        gameManager = FindObjectOfType<GameManager>();

    }

    private void OnMouseDown()
    {
        if (!gameManager.canPlay) return;
        if (!isMoveble) return;

        if (Input.GetMouseButtonDown(0))
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
                        Tile tile = hit.transform.GetComponent<Tile>();

                        tile.isOccupied = false;
                        
                        tile.occupyingConverter = null;


                    }


                }
            }

            if (!isDeclared)
                Instantiate(prefabOfItself, transform.position, Quaternion.identity);


        }

    }

    private void Update()
    {
        if (!gameManager.canPlay) return;
        if (!isMoveble) return;

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
        if (!isMoveble) return;
        isBeingHeld = false;

        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<Tile>())
            {
                Tile tile = hit.transform.GetComponent<Tile>();

                if (!tile.isOccupied)
                {
                    transform.position = hit.transform.position;
                    tile.isOccupied = true;
                    isDeclared = true;
                    tile.occupyingConverter = this;
                }
                else
                {
                    transform.position = startPos;
                }

            }
            else
            {
                transform.position = startPos;
            }
        }
        else
        {
            transform.position = startPos;
        }

    }

}
