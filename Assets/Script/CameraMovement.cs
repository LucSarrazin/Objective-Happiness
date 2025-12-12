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
        float edgeSize = 20f; 
        Vector3 moveDir = Vector3.zero;
        Vector3 mousePos = Input.mousePosition;

        // Gauche
        if (mousePos.x <= edgeSize)
            moveDir += Vector3.left;

        // Droite
        if (mousePos.x >= Screen.width - edgeSize)
            moveDir += Vector3.right;

        // Bas
        if (mousePos.y <= edgeSize)
            moveDir += Vector3.back;

        // Haut
        if (mousePos.y >= Screen.height - edgeSize)
            moveDir += Vector3.forward;

        transform.position += moveDir * speed * Time.unscaledDeltaTime;
        
        float clampedX = Mathf.Clamp(transform.position.x, -30f, 30f);
        float clampedY = transform.position.y;
        float clampedZ = Mathf.Clamp(transform.position.z, -21f, 27f);
        transform.position = new Vector3(clampedX, clampedY, clampedZ);

        if (cam.fieldOfView > 20f)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                cam.fieldOfView -= scrollSpeed * Time.unscaledDeltaTime;
            }
        }
        if (cam.fieldOfView < 60f)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                cam.fieldOfView += scrollSpeed * Time.unscaledDeltaTime;
            }
        }
        if (!isOverUi())
        {
            transform.position += moveDir * speed * Time.unscaledDeltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && !isOverUi())
        {
            //Clique avec la souris pour faire un debug.log du nom du hit
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100f, LayerMask.GetMask("Villagers")) && hit.collider.CompareTag("Villager"))
                hit.collider.GetComponent<Villager>().Touched();
            
        }
        
    }

    private bool isOverUi()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
