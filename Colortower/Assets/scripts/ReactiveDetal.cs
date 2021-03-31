using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveDetal : MonoBehaviour
{
    [SerializeField] private float duration = 0.3f;
    [SerializeField] private Transform checkerUpTransform;
    [SerializeField] private Transform checkerDownTransform;
    private ChastController allLine;
    private bool inAnimation = false;
    private int _id;
    private Vector2 deltaPos = new Vector2(0, 0);
    // Start is called before the first frame update
    void Start()
    {
        allLine = transform.parent.GetComponent<ChastController>();
    }
    public int id { get { return _id; } }
    public bool inAnim
    {
        set
        {
            inAnimation = value;
        }
    }
    public void setId(int id, Material mat)
    {
        _id = id;
        GetComponent<MeshRenderer>().material = mat;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        if(!inAnimation)
            if(!Physics.Linecast(transform.position,checkerUpTransform.position))
            {
                ChastController chast = allLine.GetUpperParent().GetComponent<ChastController>();
                if (!chast.inAnim)
                {
                    StartCoroutine(Move(0.6f));
                    transform.parent = chast.transform;
                    allLine = chast;
                }
                
            }else if(!Physics.Linecast(transform.position, checkerDownTransform.position))
            {
                ChastController chast = allLine.GetLowerParent().GetComponent<ChastController>();
                if (!chast.inAnim)
                {
                    StartCoroutine(Move(-0.6f));
                    transform.parent = chast.transform;
                    allLine = chast;
                }
            }
    }
    private void OnMouseDrag()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
            deltaPos += Input.GetTouch(0).deltaPosition;
    }
    private void OnMouseUp()
    {
        if(Mathf.Abs(deltaPos.x) / 3 > Mathf.Abs(deltaPos.y))
        {
            if (deltaPos.x < 0)
                allLine.RotateLeft();
            else allLine.RotateRight();
        }
        deltaPos.x = 0;
        deltaPos.y = 0;
    }
    private IEnumerator Move(float endposDelta)
    {
        inAnimation = true;
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position;
        endPos.y += endposDelta;
        float progress = 0f;
        float elapsedTime = 0f;
        while (progress <= 1)
        {
            transform.position = Vector3.Lerp(startPos, endPos, progress);
            elapsedTime += Time.unscaledDeltaTime;
            progress = elapsedTime / duration;
            yield return null;
        }
        transform.position = endPos;
        inAnimation = false;
    }
}
