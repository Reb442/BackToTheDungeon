using UnityEngine;

public enum LevelType
{
    MainMenu,
    MainRoom,
    EASY,
    NORMAL,
    HARD,
    BOSS,
};
public class GameManager : MonoBehaviour
{
    
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance == null)
            {
            }
            return instance;
        }
    }
    public LevelType level { get; set; }
    public int LevelInt = 0;
    private void Start()
    {
        level = LevelType.MainMenu;
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

}
