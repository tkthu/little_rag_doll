

[System.Serializable]
public class Data 
{
    
    public int score;
    public int HPmax;
    //public  time ??
    public float[] playerPosInScene;

    public Data()
    {

        score = GameManager.GM.score;
        HPmax = GameManager.GM.player.GetComponent<PlayerHealth>().HPmax;
        playerPosInScene = new float[3]; //(scene, x, y)
        playerPosInScene = playerPosInScene;
    }
}
