using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform ceilingCheck;
    private BoxCollider2D _collider;

    void Start()
    {
        objectHeight = ceilingCheck.transform.position.y- groundCheck.transform.position.y ;
        _collider = GetComponent<BoxCollider2D>();
        objectWidth = _collider.size.x / 2;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        Debug.Log(screenBounds);
       
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 , screenBounds.y);
        transform.position = viewPos;

    }
}
