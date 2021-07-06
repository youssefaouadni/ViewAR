
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UImanager : MonoBehaviour
{
    [SerializeField]
    private Button liveView;
    [SerializeField]
    private Button measurement;
    [SerializeField]
    private Button Stairsbuilder;
    // Start is called before the first frame update
    void Awake()
    {
        liveView.onClick.AddListener(sceneLiveView);
        measurement.onClick.AddListener(sceneMeasurement);
        Stairsbuilder.onClick.AddListener(Strairsbuilder);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void sceneLiveView()
    {
        SceneManager.LoadScene("Home");
    }
    void sceneMeasurement()
    {
        SceneManager.LoadScene("Measurement");
    }
    void Strairsbuilder()
    {
        SceneManager.LoadScene("StairsBuilder");
    }
}

