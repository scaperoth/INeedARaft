using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickupBehavior : MonoBehaviour
{
    [SerializeField]
    Animator _MovementAnimator;
    [SerializeField]
    UnityEvent<GameObject> _pickedSomethingUp = new UnityEvent<GameObject>();
    [SerializeField]
    UnityEvent<GameObject> _interactibleEnter = new UnityEvent<GameObject>();
    [SerializeField]
    UnityEvent<GameObject> _interactibleExit = new UnityEvent<GameObject>();
    bool _canPickUp = false;
    bool _shouldAnimate = false;
    GameObject currentItem;

    List<GameObject> _inventory = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Jump") && currentItem != null && _canPickUp)
        {
            if (_shouldAnimate)
            {
                _MovementAnimator.SetTrigger("Pick Up");
                _shouldAnimate = false;
                StartCoroutine("PickUpCurrentItem", 1.5f);
            }
            else
            {
                StartCoroutine("PickUpCurrentItem", 0f);
            }
        }
    }

    IEnumerator PickUpCurrentItem(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(currentItem);
        _pickedSomethingUp.Invoke(currentItem);
        _interactibleExit.Invoke(currentItem);
        currentItem = null;
        _canPickUp = false;
        _inventory.Add(currentItem);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {

            EmitOnEnter emitter = other.gameObject.GetComponent<EmitOnEnter>();
            if(_inventory.Count == 0)
            {
                _shouldAnimate = true;
            }
            _interactibleEnter.Invoke(other.gameObject);
            _canPickUp = true;
            currentItem = other.gameObject;
            emitter.Emit(1);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Collectible"))
        {
            EmitOnEnter emitter = other.gameObject.GetComponent<EmitOnEnter>();
            emitter.Emit(0);
            _interactibleExit.Invoke(other.gameObject);
        }
    }
}
