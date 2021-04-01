using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChastController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inAnimation = false;
    [SerializeField] private Transform CheckerUpperParent;
    [SerializeField] private Transform CheckerLowerParent;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public Transform GetUpperParent()
    {
        return Physics.OverlapSphere(CheckerUpperParent.position, 0.2f)[0].GetComponent<Transform>();
    }
    public Transform GetLowerParent()
    {
        return Physics.OverlapSphere(CheckerLowerParent.position, 0.2f)[0].GetComponent<Transform>();
    }
    public bool inAnim
    {
        get { return inAnimation; }
    }
}

