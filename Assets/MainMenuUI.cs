using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    /* main
     * credit
     * play
     * Exit
     */
    public GameObject[] pos;
    GameObject target = null;
    private void Update()
    {
        if(target != null) SlowMove(target);
    }
    public void MainStart()
    {
        target = pos[2];
        Invoke("StartLevel", 1f);
    }
    public void MainCredits()
    {
        target = pos[1];
    }
    public void Back()
    {
        target = pos[0];
    }
    public void MainExit()
    {
        target = pos[3];
    }
   
    public void Exit()
    {
        Application.Quit();
    }
    public void StartLevel()
    {
		SceneManager.LoadScene(2);
	}
    void SlowMove(GameObject gm)
    {
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, gm.transform.position,5f * Time.deltaTime);
        Camera.main.transform.rotation = Quaternion.Lerp(Camera.main.transform.rotation, gm.transform.rotation, 5f * Time.deltaTime);
    }
}
