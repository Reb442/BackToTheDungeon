using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Awake()
    {
            switch (GameManager.Instance.level)
            {
                case LevelType.MainMenu:
                    StartCoroutine(SceneChange(1));
                    break;
                case LevelType.MainRoom:
                    StartCoroutine(SceneChange(4));
                    break;
                case LevelType.EASY:
                    int num = Random.Range(5, 7);
                    StartCoroutine(SceneChange(num));
                    break;
                case LevelType.NORMAL:
                    StartCoroutine(SceneChange(7));
                    break;
                case LevelType.HARD:
                    StartCoroutine(SceneChange(8));
                    break;
            }
    }

    
    /*     2 EASY
         * 3 4 NORMAL
         * 5 HARD(CORE)
         */
    IEnumerator SceneChange(int index)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index);
        while (!asyncLoad.isDone)
        {
            
            yield return null;
            
        }
        
    }
}
