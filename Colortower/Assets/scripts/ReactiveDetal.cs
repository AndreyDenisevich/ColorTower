using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveDetal : MonoBehaviour
{
    [SerializeField] private float duration = 0.3f;

    [SerializeField] private Transform checkerUpTransform;
    [SerializeField] private Transform checkerDownTransform;
    [SerializeField] private LayerMask pieceMask;

    private ChastController allLine;
    private GameController gameController;

    private bool inAnimation = false;
    private bool canMoveUp = false;
    private bool canMoveDown = false;
    private float deltaAngle;
    private float deltaPos;

    private int _id;

    private float sens = -3f;
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
    public void setId(int id, Material mat,GameController game,float deltaAngl,float dPos)
    {
        _id = id;
        GetComponent<MeshRenderer>().material = mat;
        gameController = game;
        deltaAngle = deltaAngl;
        deltaPos = dPos;
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        Vector3 pos = transform.position;
        if (!inAnimation)
        {
            pos.y += 0.6f;
            if (!Physics.Linecast(pos, checkerUpTransform.position,pieceMask) && pos.y <= (deltaPos+0.1f))
            {
                canMoveUp = true;
            }
            pos.y -= 1.2f;
            if (!Physics.Linecast(pos, checkerDownTransform.position,pieceMask) &&pos.y>=-(deltaPos+0.1f))
            {
                canMoveDown = true;
            }
        }

    }
    private void OnMouseDrag()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Moved && !inAnimation)
        {
            canMoveDown = canMoveUp = false;
            Vector2 delta = Input.GetTouch(0).deltaPosition;
            float deltatime = Input.GetTouch(0).deltaTime;
            if (delta.x > 2 || delta.x < -2)
                allLine.transform.Rotate(0, sens * delta.x * deltatime, 0);
        }
    }
    private void OnMouseUp()
    {
        if (!inAnimation)
        {
            if (!canMoveUp && !canMoveDown)
            {
                float rotation = Mathf.RoundToInt(allLine.transform.rotation.eulerAngles.y / deltaAngle) * deltaAngle;
                StartCoroutine(RotateAnim(rotation - allLine.transform.eulerAngles.y));
            }
            if (canMoveUp)
            {
                canMoveUp = canMoveDown = false;
                ChastController chast = allLine.GetUpperParent().GetComponent<ChastController>();
                if (!inAnimation&&!chast.inAnim)
                {
                    StartCoroutine(Move(0.6f));
                    transform.parent = chast.transform;
                    allLine = chast;
                }
            }
            else if (canMoveDown)
            {
                canMoveUp = canMoveDown = false;
                ChastController chast = allLine.GetLowerParent().GetComponent<ChastController>();
                if (!inAnimation&& !chast.inAnim)
                {
                    StartCoroutine(Move(-0.6f));
                    transform.parent = chast.transform;
                    allLine = chast;
                }
            }
        }
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
        gameController.CheckForWin();
    }

    private IEnumerator RotateAnim(float deltaAngle)
    {
        allLine.inAnim = true;
        Component[] compnts = allLine.transform.GetComponentsInChildren<ReactiveDetal>();
        foreach (ReactiveDetal piece in compnts)
        {
            piece.inAnim = true;
        }
        Vector3 startPos = allLine.transform.rotation.eulerAngles;
        Vector3 endPos = allLine.transform.rotation.eulerAngles;
        endPos.y += deltaAngle;
        Quaternion rot = allLine.transform.rotation;
        float progress = 0f;
        float elapsedTime = 0f;
        while (progress <= 1)
        {
            rot.eulerAngles = Vector3.Lerp(startPos, endPos, progress);
            allLine.transform.rotation = rot;
            elapsedTime += Time.unscaledDeltaTime;
            progress = elapsedTime / duration;
            yield return null;
        }
        rot.eulerAngles = endPos;
        allLine.transform.rotation = rot;
        foreach (ReactiveDetal piece in compnts)
        {
            piece.inAnim = false;
        }
        allLine.inAnim = false;
        gameController.CheckForWin();
    }
}
