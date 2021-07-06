using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ARRaycastManager))]
public class StairsBuilder : MonoBehaviour
{
    
    
    private Vector2 touchPosition = default;
    public Generator generator;
    private ARRaycastManager arRaycastManager;

    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    [SerializeField]
    private Button linearStairs;
    [SerializeField]
    private Button elequiStairs;
    [SerializeField]
    private Button back;

    private bool isLinear;
    public ElequiStairs ElequiStairs;

    public GameObject linear;
    public GameObject elequi;
    public GameObject welcomePanel;


    

    void Start()
    {
        linearStairs.onClick.AddListener(IsLinear);
        elequiStairs.onClick.AddListener(IsNotLinear);

    }
    void Awake()
    {
        arRaycastManager = GetComponent<ARRaycastManager>();
        linear.SetActive(false);
        elequi.SetActive(false);
        
    }
    void IsLinear()
    {
        isLinear = true;
        linear.SetActive(true);
        elequi.SetActive(false);
        welcomePanel.SetActive(false);
    }
    void IsNotLinear()
    {
        isLinear = false;
        linear.SetActive(false);
        elequi.SetActive(true);
        welcomePanel.SetActive(false);
    }

    void Update()
    {      
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                touchPosition = touch.position;

                if (arRaycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
                {
                    
                    if(isLinear == true)
                    {
                        Pose hitPose = hits[0].pose;
                        Instantiate(generator.generatedStairs, generator.generatedStairs.position, generator.generatedStairs.rotation);
                    }
                    else if (isLinear==false)
                    {
                        Pose hitPose = hits[0].pose;
                        Instantiate(ElequiStairs.elequiStairs, ElequiStairs.elequiStairs.position, ElequiStairs.elequiStairs.rotation);
                    }
                    
                }
            }
        }

    }
    
    
}


