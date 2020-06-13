﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitSelection : MonoBehaviour
{
    public static UnityAction StartBuilders;

    [SerializeField] private RectTransform SelectionImage;
    private Vector2 startPos;
    private List<GameObject> objects;
    private Stack<GameObject> selectedUnits;
    private PlaceUnitController placeUnitController;


    // Start is called before the first frame update
    void Start()
    {
        
        placeUnitController = GameObject.FindWithTag("Placer").GetComponent<PlaceUnitController>();
        selectedUnits = new Stack<GameObject>();
        objects = GameObject.FindWithTag("Placer").GetComponent<PlaceUnitController>().objectList;
        SelectionImage.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            StartSelection(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0))
        {
            ReleaseSelectionBox();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            PoolingManager.Instance.RequestBlock();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            PoolingManager.Instance.PutBactToPull(5);
        }
    }

    private void StartSelection(Vector2 currentMousePos)
    {
        for(int i=0 ; i < selectedUnits.Count; i++)
        {
            selectedUnits.Pop().GetComponent<SpriteRenderer>().color = Color.white;
        }

        if (!SelectionImage.gameObject.activeInHierarchy)
            SelectionImage.gameObject.SetActive(true);

        float widgt = currentMousePos.x - startPos.x;
        float heigth = currentMousePos.y - startPos.y;

        SelectionImage.anchoredPosition = startPos + new Vector2(widgt / 2, heigth / 2);

        SelectionImage.sizeDelta = new Vector2(Mathf.Abs(widgt), Mathf.Abs(heigth));
    }

    private void ReleaseSelectionBox()
    {
        SelectionImage.gameObject.SetActive(false);
        Vector2 min = SelectionImage.anchoredPosition - (SelectionImage.sizeDelta / 2);
        Vector2 max = SelectionImage.anchoredPosition + (SelectionImage.sizeDelta / 2);

        if (Input.GetKey(KeyCode.S))
        {
            List<Vector2> locs = Grid.Instance.CheckSelectionArea(Camera.main.ScreenToWorldPoint(min), Camera.main.ScreenToWorldPoint(max));
            PlaceUnitController.PassToBuilders(locs);
            
        }
        else
        {
            foreach (var ob in objects)
            {
                Vector3 unitPos = Camera.main.WorldToScreenPoint(ob.transform.position);

                if (unitPos.x > min.x && unitPos.x < max.x && unitPos.y > min.y && unitPos.y < max.y)
                {
                    selectedUnits.Push(ob);
                    ob.GetComponent<SpriteRenderer>().color = new Color(0, 250, 248);
                }
            }
        }
    }

}
