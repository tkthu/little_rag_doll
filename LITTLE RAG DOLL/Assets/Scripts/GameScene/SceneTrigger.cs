using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public SceneName toScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.GM.loadScene(toScene);
        }
    }
}
