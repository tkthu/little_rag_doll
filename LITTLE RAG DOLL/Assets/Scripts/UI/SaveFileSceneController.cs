using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileSceneController : MonoBehaviour
{
    /*
     * khi mới vào chơi (khi nhấn Play), người dùng phải chọn chổ để lưu tiến trình (lưu HPmax, spirits,....).
     * Rồi trong lúc chơi, khi người chơi đi ngang qua checkpoint, 
     * game sẽ tự động lưu tiến trình của người chơi vào đúng cái chỗ đó.
     */

    public GameObject[] SaveSlots;

    private void Start()
    {
        updateSaveFileUI();
    }
    private void updateSaveFileUI()
    {
        for (int i = 0; i < SaveSlots.Length; i++)
        {
            if (SaveSystem.saveFileExist(i))//file cần tải có tồn tại
            {
                GameData gameData = SaveSystem.loadData(i);
                //hiện thời điểm lưu
                float timerTime = gameData.stopTime;
                int minutesInt = (int)timerTime / 60;
                int secondsInt = (int)timerTime % 60;
                int seconds100Int = (int)(Mathf.Floor((timerTime - (secondsInt + minutesInt * 60)) * 100));

                Text MinutesText = SaveSlots[i].transform.Find("GameUI").transform.Find("Time").transform.Find("Minutes").GetComponent<Text>();
                MinutesText.text = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
                Text SecondText = SaveSlots[i].transform.Find("GameUI").transform.Find("Time").transform.Find("Second").GetComponent<Text>();
                SecondText.text = (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
                Text Seconds100Text = SaveSlots[i].transform.Find("GameUI").transform.Find("Time").transform.Find("Seconds100").GetComponent<Text>();
                Seconds100Text.text = (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();

                //hiện số spirits
                float spirit = gameData.score;
                Text Score = SaveSlots[i].transform.Find("GameUI").transform.Find("Spirit").transform.Find("Spirit").GetComponent<Text>();
                Score.text = "" + spirit;
                //hiện số HPmax
                int HPmax = gameData.HPmax;
                Text heart = SaveSlots[i].transform.Find("GameUI").transform.Find("Heart").transform.Find("HPmax").GetComponent<Text>();
                heart.text = "" + HPmax;
            }
            else
            {// đây là update lại giao diện của một slot (khi nhấn xóa)
                SaveSlots[i].transform.Find("Text").GetComponent<Text>().text = "Empty Slot " + i;
                SaveSlots[i].transform.Find("GameUI").gameObject.SetActive(false);

            }

        }
    }
    public void loadSaveFile(int filenumber)
    {
        
        if (SaveSystem.saveFileExist(filenumber))//file cần tải có tồn tại => load file
        {
            GameData gameData = SaveSystem.loadData(filenumber);
            GameManager.GM.setGameData(gameData);
            GameManager.GM.startGame();
        }
        else//file cần tải không tồn tại => tạo mới
        {
            SaveSystem.saveData(filenumber);
            GameData gameData = SaveSystem.loadData(filenumber);
            GameManager.GM.setGameData(gameData);
            GameManager.GM.startGame();
        }

            
    }
    public void deleteSaveFile(int filenumber)
    {
        
        if (SaveSystem.saveFileExist(filenumber))//file cần xóa có tồn tại
        {
            // hiện bảng hỏi: chắc ko ? delete, cancel
            SaveSystem.deleteData(filenumber);
            updateSaveFileUI();// đây
        }
        else
        {
            string path = Application.persistentDataPath + "/GameData_" + filenumber + ".dat";
            Debug.LogWarning("Save file not found in " + path);
        }


    }
}
