using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private GameObject previewBuilding;

    [Header("Building Costs")]
    [SerializeField] private int requiredMason;
    [SerializeField] private int woodCost;
    [SerializeField] private int rockCost;

    private GameObject building;
    private Animator animator;
    private RectTransform rectTransform;
    private Image image;
    private Button button;
    private Vector2 defaultPosition;
    private Vector3 buildPosition;

    public void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        animator = GetComponentInParent<Animator>();
        button = GetComponent<Button>();
    }

    public void OnEnable()
    {
        button.interactable = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!button.interactable)
            return;
        defaultPosition = rectTransform.position;
        image.color = new Color(1, 1, 1, 0.6f);
        building = Instantiate(previewBuilding, Vector3.zero, Quaternion.identity);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!button.interactable)
            return;
        rectTransform.position = Input.mousePosition;

        RaycastHit hit;

        if(!Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100f, LayerMask.GetMask("Ground")))
            return;

        buildPosition = new Vector3(
            Mathf.Round(hit.point.x),
            0,
            Mathf.Round(hit.point.z)
        );

        building.transform.position = buildPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!button.interactable)
            return;
        image.color = new Color(1, 1, 1, 1);
        rectTransform.position = defaultPosition;
        Destroy(building);
        animator.Play("IdleBuild");
    }
}
