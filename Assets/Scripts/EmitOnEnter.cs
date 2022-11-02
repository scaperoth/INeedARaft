using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmitOnEnter : MonoBehaviour
{
    Material _emissionMaterial;
    float _lastEmissionTime = 0.0f;
    float _emissionRate = .2f;
    float _currentEmissionValue = 0f;
    float _emissionMax = 10f;
    float _emissionMin = 0f;
    float _emissionStepSize = 1f;
    float _emissionStepDirection = 1;

    // Start is called before the first frame update
    void Start()
    {
        _emissionMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Emit(int emissionValue)
    {
        _emissionMaterial.SetFloat("EmissionValue", emissionValue);
    }

    void PulseEmission()
    {
        if (Time.time > _lastEmissionTime + _emissionRate)
        {
            _emissionMaterial.SetFloat("EmissionValue", _currentEmissionValue);
            _lastEmissionTime = Time.time;
            if (_currentEmissionValue >= _emissionMax)
            {
                _emissionStepDirection = -1;
            }
            else if (_currentEmissionValue <= _emissionMin)
            {
                _emissionStepDirection = 1;
            }

            _currentEmissionValue += (_emissionStepSize * _emissionStepDirection);
        }
    }
}
