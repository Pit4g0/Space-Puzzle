using UnityEngine;

public class TutorialScreen : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            gameManager.LoadSelectedLevel(2);
        }
    }
}
