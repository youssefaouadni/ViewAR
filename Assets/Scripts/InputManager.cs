using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Lean.Touch;
using UnityEngine.SceneManagement;

public class InputManager : ButtonManager
{

    [SerializeField]
    private Camera arCam;
    [SerializeField]
    private ARRaycastManager _raycastManager;
    [SerializeField]
    private GameObject crosshair;
    [SerializeField]
    private Button canvasBtn;
    [SerializeField]
    private Button btn;
    [SerializeField]
    private Button manipulate;
    [SerializeField]
    private Button disable;
    [SerializeField]
    private Button ChairsC;
    [SerializeField]
    private Button home;
    [SerializeField]
    private GameObject firstCanvas;
    [SerializeField]
    private GameObject secondCanvas;
    [SerializeField]
    private GameObject thirdCanvas;
    [SerializeField]
    private GameObject ChairsCanvas;

    private GameObject selectedPrefab;
    private Touch touch;

    [SerializeField]
    private Button changetexture;

    private Pose pose;
    private List<ARRaycastHit> _hits = new List<ARRaycastHit>();
    private List<GameObject> placedPrefabs = new List<GameObject>();

    private void Awake()
    {
        thirdCanvas.SetActive(false);
        secondCanvas.SetActive(false);
        ChairsCanvas.SetActive(false);
    }
    void Start()
    {
        canvasBtn.onClick.AddListener(UIBehavior);
        btn.onClick.AddListener(Remove);
        manipulate.onClick.AddListener(Manipulate);
        ChairsC.onClick.AddListener(ChairOpen);
        disable.onClick.AddListener(Disable);
        home.onClick.AddListener(Back);
        changetexture.onClick.AddListener(changeTexture);
    }
    void UIBehavior()
    {
        firstCanvas.SetActive(false);
        secondCanvas.SetActive(true);
    }

    public void BuildObject()
    {
        CrossHairCalculation();
        ChairsCanvas.SetActive(false);
        firstCanvas.SetActive(true);
        if (IsPointerOverUI(touch)) return;
        Instantiate(DataHandler.Instance.furniture, pose.position, Quaternion.Euler(-90,0,0));
    }
    bool IsPointerOverUI(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    void CrossHairCalculation()
    {
        Vector3 origin = arCam.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));

        Ray ray = arCam.ScreenPointToRay(origin);
        if (_raycastManager.Raycast(ray, _hits))
        {
            pose = _hits[0].pose;
            crosshair.transform.position = pose.position;
            crosshair.transform.eulerAngles = new Vector3(0, 0, 0);
        }

    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Physics.autoSimulation = false;
            Physics.Simulate(Time.fixedDeltaTime);
            Physics.autoSimulation = true;


            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(arCam.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hitInfo.collider)
            {

                if (hitInfo.collider.gameObject != null)
                {

                    selectedPrefab = hitInfo.collider.gameObject;

                    if (!selectedPrefab.name.ToLower().Contains("arplane"))

                        thirdCanvas.SetActive(true);

                }
            }
        }
    }
    void Remove()
    {
        Destroy(selectedPrefab);
        thirdCanvas.SetActive(false);
    }
    public void Manipulate()
    {
        selectedPrefab.GetComponent<LeanDragTranslate>().enabled = true;
        selectedPrefab.GetComponent<LeanPinchScale>().enabled = true;
        selectedPrefab.GetComponent<LeanTwistRotateAxis>().enabled = true;
        thirdCanvas.SetActive(false);
    }
    public void Disable()
    {
        selectedPrefab.GetComponent<LeanDragTranslate>().enabled = false;
        selectedPrefab.GetComponent<LeanPinchScale>().enabled = false;
        selectedPrefab.GetComponent<LeanTwistRotateAxis>().enabled = false;
        thirdCanvas.SetActive(false);
    }
    public void ChairOpen()
    {
        secondCanvas.SetActive(false);
        ChairsCanvas.SetActive(true);
    }
    public void Back()
    {
        SceneManager.LoadScene("Welcome");
    }
    [SerializeField]
    private Material[] materials;
    void changeTexture()
    {
        selectedPrefab.GetComponent<MeshRenderer>().materials = materials;
    }


}
