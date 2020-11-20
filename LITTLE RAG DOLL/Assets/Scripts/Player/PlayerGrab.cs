using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrab : MonoBehaviour
{
    private LineRenderer line; // Reference to LineRenderer
    private Vector3 startPos;    // Start position of line
    private Vector3 endPos;    // End position of line
    private BoxCollider2D col;
    [HideInInspector] public bool caughted = false;
    [HideInInspector] public Vector2 offset = Vector2.zero;
    private bool isRetracting = false;
    public float grabLength = 2.5f;
    public float armSpeed = 10f;

    private PlayerMovement pm;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        pm = transform.parent.GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        
        startPos = GameManager.GM.player.transform.position + new Vector3(offset.x, offset.y,0);
        endPos = transform.position + new Vector3(offset.x,offset.y,0);

        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);       

        resizeBoxCollider();


        if (!isRetracting && Vector2.Distance(Vector2.zero, transform.localPosition) < grabLength)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(grabLength, 0) + offset, armSpeed * Time.deltaTime);
        }
        else
        {
            isRetracting = true;
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero , armSpeed * Time.deltaTime);
            if (transform.localPosition.magnitude <= 0)
            {
                isRetracting = false;
                foreach (Transform child in transform)
                {
                    changeCurrentBullet(child.gameObject);
                    Destroy(child.gameObject);
                }
                gameObject.SetActive(false);
            }
        }

    }

    public void setOffset(Vector2 offset)
    {
        this.offset = offset;
        transform.localPosition = offset;
    }

    private void changeCurrentBullet(GameObject go)
    {
        GameManager.GM.GameUI
                .transform
                .Find("CurrentBullet")
                .gameObject.SetActive(true);
        if (go.tag == "BounceBullet")
        {
            pm.currentBullet = GameManager.GM.poolingManager.getPlayerBounceBullets();
            GameManager.GM.GameUI
                .transform
                .Find("CurrentBullet")
                .GetComponent<Image>()
                .overrideSprite = Resources.Load<Sprite>("Sprites/Bullets/bouncing_fire_player");

        }
        if (go.CompareTag("StraightBullet"))
        {
            pm.currentBullet = GameManager.GM.poolingManager.getPlayerStraightBullets();
            GameManager.GM.GameUI
                .transform
                .Find("CurrentBullet")
                .GetComponent<Image>()
                .overrideSprite = Resources.Load<Sprite>("Sprites/Bullets/fire_player__SpriteSheet");
        }
    }

    private void resizeBoxCollider()
    {
        float lineLength = Vector3.Distance(startPos, endPos); // length of line
        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        col.offset = new Vector2(- lineLength/2, 0) + new Vector2(offset.x, offset.y); // setting position of collider object
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("EneBullets") && !caughted)
        {
            caughted = true;
            isRetracting = true;
            GameObject caughtedBullet = null;
            transform.position = collision.transform.position;

            switch (collision.tag)
            {
                case "BounceBullet":
                    caughtedBullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullets/BounceBullet_Dummy"));
                    break;
                case "StraightBullet":
                    caughtedBullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullets/StraightBullet_Dummy"));
                    break;
                case "Pistil":
                    caughtedBullet = Instantiate(Resources.Load<GameObject>("Prefabs/Bullets/Pistil_Dummy"));
                    break;
            }
            caughtedBullet.transform.position = transform.position;
            caughtedBullet.transform.SetParent(transform);
            if(collision.transform.parent.parent == null || collision.transform.parent.parent.tag != "Helmet")
                collision.gameObject.SetActive(false);
        }
        
    }
}
