using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    private Vector2 startPos;
    private bool isBeingHeld = false;


    private void Awake()
    {
        startPos = transform.position;

    }

    private void OnMouseDown()
    {
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

                        hit.transform.GetComponent<Tile>().isOccupied = false;
                    }


                }
            }
        }

    }

    private void Update()
    {
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
        isBeingHeld = false;

        Ray ray;
        RaycastHit hit;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.GetComponent<Tile>())
            {
                if (!hit.transform.GetComponent<Tile>().isOccupied)
                {
                    transform.position = hit.transform.position;
                    hit.transform.GetComponent<Tile>().isOccupied = true;
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
