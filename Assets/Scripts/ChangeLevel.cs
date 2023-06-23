using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeLevel : MonoBehaviour
{
    public LevelType type;
    GameManager Gm;
    private void Awake()
    {
        var Player = GameObject.Find("Player");

        if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Player")[0]);
        }
            if (Player != null && GameObject.Find("SpawnPoint") != null)
            {
            Player.transform.position = GameObject.Find("SpawnPoint").transform.position;
            }
       
        
    }
    private void Start()
    {
         Gm = GameManager.Instance;  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Gm.level = type;
            SceneManager.LoadScene(2);
        }
	}
    
}
