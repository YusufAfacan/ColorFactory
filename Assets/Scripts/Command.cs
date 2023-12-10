using UnityEngine;

public class Command : MonoBehaviour
{
    public enum CommandType { Extract, Rectract, Clockwise, CounterClockwise, Grab, Release }
    public CommandType commandType;

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
                if (hit.transform.GetComponent<CommandTile>())
                {
                    if (hit.transform.GetComponent<CommandTile>().isOccupied)
                    {
                        CommandTile tile = hit.transform.GetComponent<CommandTile>();

                        tile.isOccupied = false;
                        tile.declaredCommand = CommandTile.DeclaredCommand.None;
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
            if (hit.transform.GetComponent<CommandTile>())
            {
                CommandTile tile = hit.transform.GetComponent<CommandTile>();

                if (!tile.isOccupied)
                {
                    transform.position = hit.transform.position;
                    tile.isOccupied = true;
                    DeclareCommand(tile);
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

    private void DeclareCommand(CommandTile tile)
    {
        if (commandType == CommandType.Extract)
        {
            tile.declaredCommand = CommandTile.DeclaredCommand.Extract;
        }
        if (commandType == CommandType.Rectract)
        {
            tile.declaredCommand = CommandTile.DeclaredCommand.Rectract;
        }
        if (commandType == CommandType.Clockwise)
        {
            tile.declaredCommand = CommandTile.DeclaredCommand.Clockwise;
        }
        if (commandType == CommandType.CounterClockwise)
        {
            tile.declaredCommand = CommandTile.DeclaredCommand.CounterClockwise;
        }
        if (commandType == CommandType.Grab)
        {
            tile.declaredCommand = CommandTile.DeclaredCommand.Grab;
        }
        if (commandType == CommandType.Release)
        {
            tile.declaredCommand = CommandTile.DeclaredCommand.Release;
        }
    }
}
