using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideObscuringObjects : MonoBehaviour
{
    [SerializeField]
    Material _transparentMaterial;

    Ray _cameraRay;
    RaycastHit[] hits;
    Dictionary<Renderer, Material> _transparentObjects = new Dictionary<Renderer, Material>();
    List<Renderer> _objectsToRestore = new List<Renderer>();
    Material mat;

    Transform _transform;
    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        HandleTrasnparentObjects();
    }

    void HandleTrasnparentObjects()
    {
        _cameraRay = new Ray(Camera.main.transform.position, _transform.position - Camera.main.transform.position);

        hits = Physics.RaycastAll(_cameraRay,Mathf.Abs(Vector3.Magnitude(_transform.position - Camera.main.transform.position)));

        if (hits.Length > 0)
        {
            foreach (Renderer renderer in _transparentObjects.Keys)
            {
                _objectsToRestore.Add(renderer);
            }

            foreach (RaycastHit hit in hits)
            {
                Renderer rend = hit.collider.gameObject.GetComponent<Renderer>();
                if (rend == null)
                {
                    continue;
                }

                if (!_transparentObjects.TryGetValue(rend, out mat))
                {
                    _transparentObjects.Add(rend, rend.material);
                    rend.material = _transparentMaterial;
                }
                else
                {
                    _objectsToRestore.Remove(rend);
                }
            }

        }

        if (_objectsToRestore.Count > 0)
        {
            foreach (Renderer rend in _objectsToRestore)
            {
                if (_transparentObjects.TryGetValue(rend, out mat))
                {
                    rend.material = mat;
                    _transparentObjects.Remove(rend);
                }
            }

            _objectsToRestore.Clear();
        }
    }
}
