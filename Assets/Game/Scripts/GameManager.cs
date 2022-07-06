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

    private void Update()
    {       

        if (_rigidBodyBall.velocity.magnitude < 0.15f)
        {
            Vector3 ballPos = _ball.transform.position;
            _cue.transform.position = ballPos;
            _cue.SetActive(true);
            CueRotation();
            HitTheBalls();
        }
        else
            _cue.SetActive(false);

    }
       


    private void HitTheBalls()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))        
            _rigidBodyBall.AddRelativeForce(_cueForce, 0, 0);            
        
    }            
   

    private void CueRotation()
    {
        var mousePos2D = Input.mousePosition;
        var screenToCameraDistance = _camera.nearClipPlane;

        var mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, screenToCameraDistance);
        var worldPointPos = _camera.ScreenToWorldPoint(mousePosNearClipPlane);

        var ballPos = _ball.transform.position;
        var vectorForRotation = new Vector3(worldPointPos.x - ballPos.x, worldPointPos.y - ballPos.y, worldPointPos.z - ballPos.z);
        _ball.transform.rotation = Quaternion.LookRotation(vectorForRotation, Vector3.up);
        var tempCueRotation = _ball.transform.rotation;
        _cue.transform.rotation = tempCueRotation;        

    }

}
