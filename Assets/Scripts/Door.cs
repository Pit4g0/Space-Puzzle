using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject connectedDoor;

    private SoundsManager soundManager;
    private GameObject player;
    private bool playerInRange;

    private void Start()
    {
        soundManager = FindObjectOfType<SoundsManager>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            soundManager.PlaySound(SoundsManager.Sounds.OpenDoor);
            player.transform.position = connectedDoor.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = true;          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
