using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private BuildingCosts buildingCosts;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI masonCounter;
    [SerializeField] private TextMeshProUGUI woodCounter;
    [SerializeField] private TextMeshProUGUI rockCounter;
    [SerializeField] private GameObject costPanel;

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

        masonCounter.text = buildingCosts.requiredMason.ToString();
        woodCounter.text = buildingCosts.woodCost.ToString();
        rockCounter.text = buildingCosts.rockCost.ToString();
    }

    public void Update()
    {
        masonCounter.color = (GameManager.numberMason < buildingCosts.requiredMason) ? Color.red : Color.black;
        woodCounter.color = (GameManager.totalWood < buildingCosts.woodCost) ? Color.red : Color.black;
        rockCounter.color = (GameManager.totalRock < buildingCosts.rockCost) ? Color.red : Color.black;


        button.interactable = GameManager.numberMason >= buildingCosts.requiredMason &&
                              GameManager.totalWood >= buildingCosts.woodCost &&
                              GameManager.totalRock >= buildingCosts.rockCost;

        if (button.interactable)
            costPanel.SetActive(false);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!button.interactable)
            return;
        defaultPosition = rectTransform.position;
        image.color = new Color(1, 1, 1, 0.6f);
        building = Instantiate(buildingCosts.builderPrefab, Vector3.zero, Quaternion.identity);
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
            0, // TODO : raycast for height map
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
