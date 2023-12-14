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
    private void Awake()
    {
        startPos = transform.position;

    }

    private void OnMouseDown()
    {
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


}
