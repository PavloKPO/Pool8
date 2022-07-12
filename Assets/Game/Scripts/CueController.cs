using UnityEngine;

public class CueController : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _cue;
    [SerializeField] private GameObject _cueSprite;
    [SerializeField] private Camera _camera;
        
    private Rigidbody _rbBall;

    [SerializeField] private float _force;


    private void Start()
    {        
        _rbBall = _ball.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if(_rbBall.velocity.magnitude < 0.1f)
        {
            var ballPos = _ball.transform.position;
            _cue.transform.position = ballPos;
            
            _cueSprite.SetActive(true);
            CueRotation();
            HitTheBalls();
            
        }
        else
            _cueSprite.SetActive(false);
    }

    private void HitTheBalls()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var mousePos2D = Input.mousePosition;
            var screenToCameraDistance = _camera.nearClipPlane;

            var mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, screenToCameraDistance);
            var worldPointPos = _camera.ScreenToWorldPoint(mousePosNearClipPlane);

            var ballPos = _ball.transform.position;
            var vectorForForce = new Vector3(worldPointPos.x - ballPos.x, 0, worldPointPos.z - ballPos.z);
            
            _rbBall.AddForce(vectorForForce.normalized * _force, ForceMode.Impulse);
        }
                
    }

    private void CueRotation()
    {
        var mousePos2D = Input.mousePosition;
        var screenToCameraDistance = _camera.nearClipPlane;

        var mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, screenToCameraDistance);
        var worldPointPos = _camera.ScreenToWorldPoint(mousePosNearClipPlane);

        var ballPos = _ball.transform.position;
        var vectorForRotation = new Vector3(worldPointPos.x - ballPos.x, worldPointPos.y - ballPos.y, worldPointPos.z - ballPos.z);
        _cue.transform.rotation = Quaternion.LookRotation(vectorForRotation, Vector3.up);
        
    }
}
