using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewportHandler : MonoBehaviour
{
    public SpriteRenderer cameraSightArea;

    // Use this for initialization
    void Start()
    {
        float screenRatio = (float)Screen.width / (float)Screen.height;
        float targetRatio = cameraSightArea.bounds.size.x / cameraSightArea.bounds.size.y;

        if (screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = cameraSightArea.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = cameraSightArea.bounds.size.y / 2 * differenceInSize;
        }
    }
}