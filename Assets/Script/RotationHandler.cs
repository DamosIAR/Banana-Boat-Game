using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationHandler : MonoBehaviour
{
    public static RotationHandler Instance { get; private set; }

    [SerializeField] private AnimationCurve rotationCurve;

    private float hold = 0f;
    private float force;

    bool left;

    private void Awake()
    {
        Instance = this;
        transform.eulerAngles = Vector3.zero;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            if (!GameManager.Instance.isPlaying()) return;
            hold += Time.deltaTime;
            force = Mathf.RoundToInt(rotationCurve.Evaluate(hold));
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "LeftSide")
                {
                    transform.Rotate(Vector3.forward * force * Time.deltaTime);
                    Debug.Log("kiri");
                    left = true;
                }
                else if (hit.collider.tag == "RightSide")
                {
                    transform.Rotate(Vector3.back * force * Time.deltaTime);
                    Debug.Log("kanan");
                    left = false;
                }
            }
        }
        else
        {
            if (hold > 0f)
            {
                if (!GameManager.Instance.isPlaying()) return;
                hold -= Time.deltaTime * 2;
                force = Mathf.RoundToInt(rotationCurve.Evaluate(hold));
                if (left)
                {
                    transform.Rotate(Vector3.forward * force * Time.deltaTime);

                }
                else
                {
                    transform.Rotate(Vector3.back * force * Time.deltaTime);

                }
            }
        }
        //Debug.Log(GetRotation());
    }

    public float getForce()
    {
        return force;
    }

    public Vector3 GetRotation()
    {
        return transform.eulerAngles;
    }

    public void RotateToLeft()
    {
        if (!GameManager.Instance.isPlaying()) return;
        hold += Time.deltaTime;
        force = Mathf.RoundToInt(rotationCurve.Evaluate(hold));
        transform.Rotate(Vector3.up * force * Time.deltaTime);
        left = true;
    }

    public void RotateToRight()
    {
        if (!GameManager.Instance.isPlaying()) return;
        hold += Time.deltaTime;
        force = Mathf.RoundToInt(rotationCurve.Evaluate(hold));
        transform.Rotate(Vector3.down * force * Time.deltaTime);
        left = false;
    }
}
