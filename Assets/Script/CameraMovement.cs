using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float speed;
    public float scrollSpeed;

    private Camera cam;
    
    PlayerInput playerInput;
    InputAction move;
    // Start is called before the first frame update
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        move = playerInput.actions.FindAction("Move");
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = move.ReadValue<Vector2>();
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += Vector3.back * speed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += Vector3.forward * speed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.unscaledDeltaTime;
        }

        if (cam.fieldOfView > 20f)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                cam.fieldOfView -= scrollSpeed * Time.unscaledDeltaTime;
                //transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
            }
        }
        if (cam.fieldOfView < 60f)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                cam.fieldOfView += scrollSpeed * Time.unscaledDeltaTime;
                //transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isOverUi())
        {
            //Clique avec la souris pour faire un debug.log du nom du hit
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("House"))
                {
                    hit.collider.GetComponentInParent<House>().Touched();
                }
                if (hit.collider.CompareTag("Villager"))
                {
                    hit.collider.GetComponent<Villager>().Touched();
                }
            }
            
        }
        
    }

    private bool isOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
