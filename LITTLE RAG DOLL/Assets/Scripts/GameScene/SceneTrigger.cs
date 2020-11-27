using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    public SceneName toScene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {            
            collision.GetComponent<PlayerMovement>().resetAction();
            collision.attachedRigidbody.velocity = Vector2.zero;
            GameManager.GM.saveTemp();
            GameManager.GM.loadScene(toScene);
        }
    }
}
