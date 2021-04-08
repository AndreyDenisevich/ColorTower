using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform Tower;
    [SerializeField] private float sens=1f;
    private bool inAnimation=false;
    private float duration = 0.2f;
    private float deltaAngle;
    void Start()
    {
        
    }
    public Transform tower { set { Tower = value; } }
    public float angle { set { deltaAngle = value; } }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDrag()
    {
        if (Input.GetTouch(0).phase == TouchPhase.Moved && !inAnimation)
        {
            Vector2 delta = Input.GetTouch(0).deltaPosition;
            float deltatime = Input.GetTouch(0).deltaTime;
            if (delta.x > 1 || delta.x < -1)
                Tower.Rotate(0, sens * delta.x * deltatime, 0);
        }
    }
    private void OnMouseUp()
    {
        if (!inAnimation)
        {
            float rotation = Mathf.RoundToInt(Tower.rotation.eulerAngles.y / deltaAngle) * deltaAngle;
            StartCoroutine(RotateAnim(rotation-Tower.rotation.eulerAngles.y));
        }
    }
    private IEnumerator RotateAnim(float deltaAngle)
    {
        inAnimation = true;
        Vector3 startPos = Tower.rotation.eulerAngles;
        Vector3 endPos = Tower.rotation.eulerAngles;
        endPos.y += deltaAngle;
        Quaternion rot = Tower.rotation;
        float progress = 0f;
        float elapsedTime = 0f;
        while (progress <= 1)
        {
            rot.eulerAngles = Vector3.Lerp(startPos, endPos, progress);
            Tower.rotation = rot;
            elapsedTime += Time.unscaledDeltaTime;
            progress = elapsedTime / duration;
            yield return null;
        }
        rot.eulerAngles = endPos;
        Tower.rotation = rot;
        inAnimation = false;
    }
}
