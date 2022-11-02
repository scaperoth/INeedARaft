using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DrowningBehavior : MonoBehaviour
{
    [SerializeField]
    Transform boat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.y < boat.localPosition.y - 3)
        {
            SceneManager.LoadScene("Main Island");
        }   
    }
}
