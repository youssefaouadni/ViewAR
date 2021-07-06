using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Generator : MonoBehaviour
{
    public Transform Stairs;

    public InputField inputField;

    public GameObject cube;
    public Transform generatedStairs;
    public int nbrStrairs;

    [SerializeField]
    private Button back;
     void Awake()
    {
        back.onClick.AddListener(Back);
    }


    public void Generate()
    {
        nbrStrairs = int.Parse(inputField.text);

        for (int i = 0; i < nbrStrairs; i++)
        {
            
            generatedStairs = Instantiate(Stairs, new Vector3(Stairs.position.x - i * 0.2f, Stairs.position.y + i*0.116f, Stairs.position.z), Quaternion.identity);

        }
    }
    void Back()
    {
        SceneManager.LoadScene("Welcome");
    }

}
