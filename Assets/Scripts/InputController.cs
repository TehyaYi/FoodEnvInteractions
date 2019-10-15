using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
    [SerializeField]
    private TileMapsController _tileMapsController;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                // TODO: Send to UIController
                return;
            }
            else
            {
                // TODO: Send to EditModeController
                _tileMapsController.LeftMousePressed();
            }
           
        }
    }
}
