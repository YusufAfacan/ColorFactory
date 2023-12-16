using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public List<Tile> tiles;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeInHierarchy)
            {
                tiles.Add(transform.GetChild(i).GetComponent<Tile>());
            }
            
            
        }
    }
}
