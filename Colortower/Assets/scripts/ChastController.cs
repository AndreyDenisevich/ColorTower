using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChastController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool inAnimation = false;
    [SerializeField] private float duration = 0.4f;
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
    public void RotateLeft()
    {
        if (!inAnimation)
            StartCoroutine(RotateAnim(45f));
    }
    public void RotateRight()
    {
        if (!inAnimation)
            StartCoroutine(RotateAnim(-45f));
    }
    private IEnumerator RotateAnim(float deltaAngle)
    {
        inAnimation = true;
        Component[] compnts = transform.GetComponentsInChildren<ReactiveDetal>();
        foreach (ReactiveDetal piece in compnts)
        {
            piece.inAnim = true;
        }
        Vector3 startPos = transform.rotation.eulerAngles;
        Vector3 endPos = transform.rotation.eulerAngles;
        endPos.y += deltaAngle;
        Quaternion rot = transform.rotation;
        float progress = 0f;
        float elapsedTime = 0f;
        while (progress <= 1)
        {
            rot.eulerAngles = Vector3.Lerp(startPos, endPos, progress);
            transform.rotation = rot;
            elapsedTime += Time.unscaledDeltaTime;
            progress = elapsedTime / duration;
            yield return null;
        }
        rot.eulerAngles = endPos;
        transform.rotation = rot;
        foreach (ReactiveDetal piece in compnts)
        {
            piece.inAnim = false;
        }
        inAnimation = false;
    }
    public bool inAnim
    {
        get { return inAnimation; }
    }
}

