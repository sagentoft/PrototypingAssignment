using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public enum CameraState {
        Gameplay,
        ScriptedCinematic,
        Death
    }

    [Header("References and Misc")]
    public GameObject player;
    public CameraState currentState = new CameraState();

    [Header("Gameplay State following Parameters")]
    public bool shouldAlwaysFollowPlayerInGameplay = false;

    public Vector2 screenEdgeForFollow;

    public float followDamping;

    public AnimationCurve dampingScreenEdgeDistance;

    private Camera cam;

    private Vector3 initialCameraOffset;


    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        initialCameraOffset = transform.position - player.transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           StartCoroutine(Shake(10f, 0.4f));
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        switch (currentState)
        {
            case CameraState.Gameplay:
                UpdateOnGameplay();
                break;

            case CameraState.Death:
                UpdateOnDeath();
                break;

            case CameraState.ScriptedCinematic:
                UpdateOnScriptedCinematic();
                break;

            default:
                break;
        }

    }

    private void UpdateOnGameplay()
    {
        if (shouldAlwaysFollowPlayerInGameplay)
        {
            transform.position = Vector3.Lerp(transform.position, player.transform.position + initialCameraOffset, followDamping * Time.deltaTime);
            return;
        }

        Vector3 playerRelativeScreenPosition = cam.WorldToViewportPoint(player.transform.position);
        float adjustedDamping = followDamping;

        if(playerRelativeScreenPosition.x <= screenEdgeForFollow.x)
        {
            adjustedDamping *= dampingScreenEdgeDistance.Evaluate(1-(playerRelativeScreenPosition.x / screenEdgeForFollow.x));
            transform.Translate(transform.right * Time.deltaTime * adjustedDamping * -1);
        }
        if(playerRelativeScreenPosition.x > (1 - screenEdgeForFollow.x))
        {
            adjustedDamping *= dampingScreenEdgeDistance.Evaluate(1-((1-playerRelativeScreenPosition.x) / screenEdgeForFollow.x));
            transform.Translate(transform.right * Time.deltaTime * adjustedDamping);
        }
        if(playerRelativeScreenPosition.y <= screenEdgeForFollow.y)
        {
            adjustedDamping *= dampingScreenEdgeDistance.Evaluate(1-(playerRelativeScreenPosition.y / screenEdgeForFollow.y));
            transform.position -= Vector3.forward * Time.deltaTime * adjustedDamping;
        }
        if(playerRelativeScreenPosition.y > (1 - screenEdgeForFollow.y))
        {
            adjustedDamping *= dampingScreenEdgeDistance.Evaluate(1 - ((1-playerRelativeScreenPosition.y) / screenEdgeForFollow.y));
            transform.position += Vector3.forward * Time.deltaTime * adjustedDamping;
        }

    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.position = new Vector3(x, y, 0);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        transform.position = initialCameraOffset;
    }

    private void UpdateOnScriptedCinematic()
    {

    }

    private void UpdateOnDeath()
    {

    }
}
