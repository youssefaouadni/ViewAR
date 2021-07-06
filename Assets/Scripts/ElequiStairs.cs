using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElequiStairs : MonoBehaviour

{
    public GameObject inputField;
    public int numberOfObjects;
    public float radius = 5f;
    public Transform prefab;
    private float rotate = 0;
    public Transform elequiStairs;

    public void Generate()
    {     
        numberOfObjects = int.Parse(inputField.GetComponent<InputField>().text);
        for (int i = 0; i < numberOfObjects; i++)
        {
  
            Quaternion rot = Quaternion.Euler(0, rotate, 0);
            elequiStairs=Instantiate(prefab, new Vector3(prefab.position.x, prefab.position.y + i  ,prefab.position.z), rot);
            rotate = rotate - 14;
            if (rotate== -180)
            {
                rotate = 0;
            }

        }
    }
    void Back()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Welcome");
    }
    [SerializeField]
    private Button back;
    void Awake()
    {
        back.onClick.AddListener(Back);
    }
}
