using UnityEngine;

public class PocketController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }

        
}
 
    

