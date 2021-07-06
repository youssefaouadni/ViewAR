using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CanvasBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject firstCanvas;
    [SerializeField]
    private GameObject secondCanvas;
    [SerializeField]
    private Button btn;
    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(SwitchCanvas);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void Awake()
    {
        secondCanvas.SetActive(false);
    }
    void SwitchCanvas ()
    {
        firstCanvas.SetActive(false);
        secondCanvas.SetActive(true);
    }
 
    
}
