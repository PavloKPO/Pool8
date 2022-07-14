using UnityEngine;

public class CueController : MonoBehaviour
{
    [SerializeField] private Rigidbody _ball;
    [SerializeField] private Transform _cue;
    [SerializeField] private GameObject _cueSprite;
    [SerializeField] private Camera _camera;
        
    [SerializeField] private float _force;
    
           
    private void Update()
    {       
        if (_ball.velocity.magnitude < 0.1f)
        {
            var ballPos = _ball.transform.position;
            _cue.position = ballPos;
            
            _cueSprite.SetActive(true);
            CueRotationAndHitTheBalls();          
        }
        else
            _cueSprite.SetActive(false);       
        
    }

    

    private void CueRotationAndHitTheBalls()
    {
        var mousePos2D = Input.mousePosition;
        var screenToCameraDistance = _camera.nearClipPlane;

        var mousePosNearClipPlane = new Vector3(mousePos2D.x, mousePos2D.y, screenToCameraDistance);
        var worldPointPos = _camera.ScreenToWorldPoint(mousePosNearClipPlane);

        var ballPos = _ball.transform.position;
        var vectorForRotation = new Vector3(worldPointPos.x - ballPos.x, worldPointPos.y - ballPos.y, worldPointPos.z - ballPos.z);
        _cue.rotation = Quaternion.LookRotation(vectorForRotation, Vector3.up);
       
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var vectorForForce = new Vector3(worldPointPos.x - ballPos.x, 0, worldPointPos.z - ballPos.z);
            _ball.AddForce(vectorForForce.normalized * _force, ForceMode.Impulse);
        }
    }
}
