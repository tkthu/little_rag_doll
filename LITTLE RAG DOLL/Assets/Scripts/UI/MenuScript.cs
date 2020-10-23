using UnityEngine;
using System.Collections;
using TMPro;

public class MenuScript : MonoBehaviour
{

	Transform menuPanel;
	Event keyEvent;
    TextMeshProUGUI buttonText;
	KeyCode newKey;

	bool waitingForKey;


	void Start()
	{
		menuPanel = transform.Find("Panel");
		menuPanel.gameObject.SetActive(true);
		waitingForKey = false;
		for (int i = 0; i < menuPanel.childCount; i++)
		{
			if (menuPanel.GetChild(i).name == "btn_up")
				menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.up.ToString();
			else if (menuPanel.GetChild(i).name == "btn_down")
				menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.down.ToString();
			else if (menuPanel.GetChild(i).name == "btn_left")
				menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.left.ToString();
			else if (menuPanel.GetChild(i).name == "btn_right")
				menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.right.ToString();
			else if (menuPanel.GetChild(i).name == "btn_jump")
				menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.jump.ToString();
			else if (menuPanel.GetChild(i).name == "btn_map")
				menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.interact.ToString();
			else if (menuPanel.GetChild(i).name == "btn_attack")
				menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.attack.ToString();
			else if (menuPanel.GetChild(i).name == "btn_eatShoot")
				menuPanel.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = GameManager.GM.eatShoot.ToString();
		}
	}


	void Update()
	{
		//Escape key will open or close the panel
		if (Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
			menuPanel.gameObject.SetActive(true);
		else if (Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
			menuPanel.gameObject.SetActive(false);
	}

    void OnGUI()
    {
        keyEvent = Event.current;

        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode;
            waitingForKey = false;
        }
    }

    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    public void SendText(TextMeshProUGUI text)
    {
        buttonText = text;
    }

    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }


    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey(); //Executes endlessly until user presses a key

        switch (keyName)
        {
            case "up":
                GameManager.GM.up = newKey; //Set forward to new keycode
                buttonText.text = GameManager.GM.up.ToString(); //Set button text to new key
                PlayerPrefs.SetString("upKey", GameManager.GM.up.ToString()); //save new key to PlayerPrefs
                break;
            case "down":
                GameManager.GM.down = newKey; //set backward to new keycode
                buttonText.text = GameManager.GM.down.ToString(); //set button text to new key
                PlayerPrefs.SetString("downKey", GameManager.GM.down.ToString()); //save new key to PlayerPrefs
                break;
            case "left":
                GameManager.GM.left = newKey; //set left to new keycode
                buttonText.text = GameManager.GM.left.ToString(); //set button text to new key
                PlayerPrefs.SetString("leftKey", GameManager.GM.left.ToString()); //save new key to playerprefs
                break;
            case "right":
                GameManager.GM.right = newKey; //set right to new keycode
                buttonText.text = GameManager.GM.right.ToString(); //set button text to new key
                PlayerPrefs.SetString("rightKey", GameManager.GM.right.ToString()); //save new key to playerprefs
                break;
            case "jump":
                GameManager.GM.jump = newKey; //set jump to new keycode
                buttonText.text = GameManager.GM.jump.ToString(); //set button text to new key
                PlayerPrefs.SetString("jumpKey", GameManager.GM.jump.ToString()); //save new key to playerprefs
                break;
            case "interact":
                GameManager.GM.interact = newKey; //set jump to new keycode
                buttonText.text = GameManager.GM.interact.ToString(); //set button text to new key
                PlayerPrefs.SetString("interactKey", GameManager.GM.interact.ToString()); //save new key to playerprefs
                break;
            case "attack":
                GameManager.GM.attack = newKey; //set jump to new keycode
                buttonText.text = GameManager.GM.attack.ToString(); //set button text to new key
                PlayerPrefs.SetString("attackKey", GameManager.GM.attack.ToString()); //save new key to playerprefs
                break;
            case "eatShoot":
                GameManager.GM.eatShoot = newKey; //set jump to new keycode
                buttonText.text = GameManager.GM.eatShoot.ToString(); //set button text to new key
                PlayerPrefs.SetString("eatShootKey", GameManager.GM.eatShoot.ToString()); //save new key to playerprefs
                break;
        }

        yield return null;
    }

}
