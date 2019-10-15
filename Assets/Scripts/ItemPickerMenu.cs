using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemPickerMenu : MonoBehaviour
{
    private bool ItemPickerMenuIsOpen = false;

    [SerializeField]
    private GameObject _itemPickerMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (ItemPickerMenuIsOpen)
            {
                CloseItemPickerMenu();
            }
            else
            {
                OpenItemPickerMenu();
            }
        }
    }

    public void OpenItemPickerMenu()
    {
        Time.timeScale = 0f;
        _itemPickerMenu.SetActive(true);
        ItemPickerMenuIsOpen = !ItemPickerMenuIsOpen;
    }

    public void CloseItemPickerMenu()
    {
        Time.timeScale = 1f;
        _itemPickerMenu.SetActive(false);
        ItemPickerMenuIsOpen = !ItemPickerMenuIsOpen;
    }

    public void SelectItem(Tile selectedTile)
    {
        TileMapsController tileMapsController = FindObjectOfType<TileMapsController>();
        tileMapsController.SelectedTile = selectedTile;
    }
}
