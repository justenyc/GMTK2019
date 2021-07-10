using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public Player_Controller playerController;
    public GameObject spawnManager;
    public GameObject exampleEnemy;
    public GameObject tutorialCanvas;

    // Start is called before the first frame update
    public void StartEverything()
    {
        playerController.enabled = true;
        spawnManager.SetActive(true);
        Destroy(exampleEnemy);
        Destroy(tutorialCanvas);
        Destroy(this.gameObject);
    }
}
