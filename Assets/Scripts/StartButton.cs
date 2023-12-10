using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public CommandLines CommandLines;
    public Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(()  => { CommandLines.StartCommanding();});
    }
}
