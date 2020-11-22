using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGrabShoot : MonoBehaviour
{
    private LineRenderer line; // Reference to LineRenderer
    private Vector3 startPos;    // Start position of line
    private Vector3 endPos;    // End position of line
    private BoxCollider2D col;
    private Vector2 offset = Vector2.zero;
    public bool isRetracting = false;
    public float grabLength = 2.5f;
    public float armSpeed = 10f;
    public AudioClip playerGrab;
    public AudioClip playerShoot;

    private GameObject currentBullet;
    private Transform bulletHolder;
    private void Start()
    {
        line = GetComponent<LineRenderer>();
        col = gameObject.AddComponent<BoxCollider2D>();
        col.isTrigger = true;
        bulletHolder = GameManager.GM.GameUI.transform.Find("CurrentBullet");
    }
    private void Update()
    {
        startPos = GameManager.GM.player.transform.position + new Vector3(offset.x, offset.y, 0);
        endPos = transform.position;

        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);

        resizeBoxCollider();
    }

    public void grabOrShoot(Vector2 offset, Vector2 grabDir, Vector2 shootDir)
    {
        setOffset(offset);
        if (currentBullet == null && !gameObject.activeSelf)
        {
            gameObject.SetActive(true);
            StartCoroutine(grabing(grabDir));
            AudioManager.instance.PlaySound(playerGrab, transform.position);
        }
        else if ( currentBullet != null )
        {
            shoot(shootDir);
            AudioManager.instance.PlaySound(playerShoot, transform.position);
        }
    }

    IEnumerator grabing(Vector2 dir)
    {
        // keo dai tay
        while (!isRetracting)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(grabLength * dir.x, offset.y), armSpeed * Time.deltaTime);
            Vector3 v = Vector3.zero - transform.localPosition;
            if (v.magnitude >= grabLength)
                isRetracting = true;
            yield return new WaitForFixedUpdate();
        }
        // rut tay lai
        while (isRetracting)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, Vector2.zero + offset, armSpeed * Time.deltaTime);
            Vector3 v = transform.localPosition - (Vector3.zero + (Vector3)offset);
            if (v.magnitude <= 0)
                isRetracting = false;
            yield return new WaitForFixedUpdate();
        }
        // xoa child cua tay
        foreach (Transform child in transform)
        {
            changeCurrentBullet(child.gameObject);
            Destroy(child.gameObject);
        }
        gameObject.SetActive(false);
    }

    public void shoot(Vector2 dir)
    {
        currentBullet.transform.position = GameManager.GM.player.transform.position + new Vector3(offset.x, offset.y, 0);
        if (currentBullet.tag == "StraightBullet")
        {
            currentBullet.SetActive(true);
            currentBullet.GetComponent<StraightBulletMovement>().SetDirection(new Vector2(dir.x, 0));//ban ngang
        }
        if (currentBullet.tag == "BounceBullet")
        {
            currentBullet.GetComponent<BounceBulletMovement>().SetDirection(new Vector2(dir.x, 0));
            currentBullet.GetComponent<BounceBulletMovement>().activate();
        }
        bulletHolder.gameObject.SetActive(false);
        currentBullet = null;      
    }

    public void setOffset(Vector2 offset)
    {
        this.offset = offset;
        transform.localPosition = offset;
    }

    private void changeCurrentBullet(GameObject go)
    {
        bulletHolder.gameObject.SetActive(true);
        if (go.tag == "BounceBullet")
        {
            currentBullet = GameManager.GM.poolingManager.getPlayerBounceBullets();
            bulletHolder.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Bullets/bouncing_fire_player");

        }else if (go.CompareTag("StraightBullet"))
        {
            currentBullet = GameManager.GM.poolingManager.getPlayerStraightBullets();
            bulletHolder.GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/Bullets/fire_player__SpriteSheet");
        }
    }

    private void resizeBoxCollider()
    {
        float lineLength = Vector3.Distance(startPos, endPos); // length of line
        col.size = new Vector3(lineLength, 0.1f, 1f); // size of collider is set where X is length of line, Y is width of line, Z will be set as per requirement
        if(0 < transform.localPosition.x)
            col.offset = new Vector2(- lineLength/2, 0); // setting position of collider object
        else
            col.offset = new Vector2(lineLength/2, 0); // setting position of collider object
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("EneBullets") && !isRetracting)
        {
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
