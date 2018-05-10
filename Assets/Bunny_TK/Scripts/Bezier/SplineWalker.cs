using UnityEngine;

[ExecuteInEditMode]
public class SplineWalker : MonoBehaviour
{

    public BezierSpline spline;
    public float duration;
    public bool lookForward;
    public SplineWalkerMode mode;
    public AnimationCurve speedCurve;

    [SerializeField]
    [Range(0f, 1f)]
    private float progress;
    private bool goingForward = true;

    private void Update()
    {
        if (goingForward)
        {
            if (Application.isPlaying)
            {
                float deltaTime = speedCurve.Evaluate(progress + Time.deltaTime / duration);
                progress += (Time.deltaTime * deltaTime) / duration;
            }
            if (progress > 1f)
            {
                if (mode == SplineWalkerMode.Once)
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop)
                {
                    progress -= 1f;
                }
                else
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else
        {
            if (Application.isPlaying)
            {
                float deltaTime = speedCurve.Evaluate(progress + Time.deltaTime / duration);

                progress -= (Time.deltaTime * deltaTime) / duration;

            }
            if (progress < 0f)
            {
                progress = -progress;
                goingForward = true;
            }
        }

        Vector3 position = spline.GetPoint(progress);
        transform.localPosition = position;
        if (lookForward)
        {
            transform.LookAt(position + spline.GetDirection(progress));
        }
    }
}