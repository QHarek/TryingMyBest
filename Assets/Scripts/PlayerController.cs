using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float _speed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Move();
        Rotate();
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Hit();
        }
    }

    private void Rotate()
    {
        transform.rotation *= Quaternion.Euler(new Vector3(0, Input.GetAxis("Mouse X"), 0));

        Camera.main.transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), 0, 0));
        Vector3 mainCameraRotationEulers = Camera.main.transform.eulerAngles;
        if(mainCameraRotationEulers.x > 300)
            mainCameraRotationEulers.x = Mathf.Clamp(mainCameraRotationEulers.x, 330, 360);
        else
            mainCameraRotationEulers.x = Mathf.Clamp(mainCameraRotationEulers.x, 0, 30);
        Camera.main.transform.eulerAngles = mainCameraRotationEulers;
    }

    private void Move()
    {
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * _speed, 0, Input.GetAxis("Vertical") * _speed));
    }

    private void Hit()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.forward, out hit, 10))
            if(hit.collider.GetComponent<OreClusterBehavior>() != null)
                hit.collider.GetComponent<OreClusterBehavior>().ApplyDamage(gameObject);
    }
}
