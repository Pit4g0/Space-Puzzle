using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float velocity;
    [SerializeField] private int leftBorder;
    [SerializeField] private int rightBorder;
    [SerializeField] private Vector3 direction;

    private Vector3 oldPosition;

    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        Debug.DrawRay(transform.position, new Vector3(direction.x * horizontalInput, direction.y, direction.z), Color.red, 0.1f, false);
        Ray ray = new Ray(transform.position, new Vector3(direction.x * horizontalInput, direction.y, direction.z));
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit) && horizontalInput != 0)
        {
            if (raycastHit.transform.gameObject.CompareTag("Piece") || raycastHit.transform.gameObject.CompareTag("Door") || raycastHit.transform.gameObject.CompareTag("Rocket"))
            {

                oldPosition = transform.position;
                transform.Translate(horizontalInput * velocity * Time.deltaTime, 0, 0);
                
                if (transform.position.x < leftBorder)
                    transform.position = new Vector3(leftBorder, transform.position.y, transform.position.z);

                if (transform.position.x > rightBorder)
                    transform.position = new Vector3(rightBorder, transform.position.y, transform.position.z);

                transform.position = new Vector3(Mathf.Clamp(transform.position.x, leftBorder, rightBorder), transform.position.y, transform.position.z);
                transform.localScale = new Vector3(3 * horizontalInput, 3, 1);

                if (!raycastHit.transform.gameObject.GetComponent<Puzzle>().isPlaced)
                    transform.position = oldPosition;
            }
        }

    }
}