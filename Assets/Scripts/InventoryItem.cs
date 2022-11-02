using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _textMesh;
    int _currentInventory = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddOneToGui()
    {
        _currentInventory++;
        _textMesh.text = $"{_currentInventory}/10";
        if(_currentInventory == 10)
        {
            SceneManager.LoadScene("High Seas");
        }
    }
}
