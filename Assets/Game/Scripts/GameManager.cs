using UnityEngine;



public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _ball;
    [SerializeField] private GameObject _cue;
    [SerializeField] private Camera _camera;
    private Rigidbody _rigidBodyBall;

    [SerializeField] private float _cueForce;
             
    
    private void Start()
    {
        _rigidBodyBall = GetComponent<Rigidbody>();
        
    }

    private void FixedUpdate()
    {
        HitTheBalls();
        CueActivatedDeactivate();
        CueRotation();        

    }

    private void HitTheBalls()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))        
            _rigidBodyBall.AddRelativeForce(_cueForce, 0, 0);            
        
    }

            
    private void CueActivatedDeactivate()
    {        
        Vector3 ballPos = _ball.transform.position;

        if (_rigidBodyBall.velocity.magnitude < 0.15f)
        {
            _cue.transform.position = ballPos;       
            _cue.SetActive(true);
        }
        else
            _cue.SetActive(false);

    }

    private void CueRotation()
    {
        var mousePos2D = Input.mousePosition;
        var screenToCameraDistance = _camera.nearClipPlane;

        var mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, screenToCameraDistance);
        var worldPointPos = _camera.ScreenToWorldPoint(mousePosNearClipPlane);

        _ball.transform.rotation = Quaternion.LookRotation(worldPointPos, Vector3.up);
        var tempCueRotation = _ball.transform.rotation;
        _cue.transform.rotation = tempCueRotation;        

    }

}
