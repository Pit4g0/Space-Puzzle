//using System.Net.Sockets;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private GameManager gameManager;
    private SoundsManager soundManager;

    private bool playerCollision = false;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        soundManager = FindObjectOfType<SoundsManager>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log(playerCollision);
            gameManager.ShowWinPanel();
            soundManager.PlaySound(SoundsManager.Sounds.Finish);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            playerCollision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.transform.CompareTag("Player"))
        {
            playerCollision = false;
        }
    }

}
