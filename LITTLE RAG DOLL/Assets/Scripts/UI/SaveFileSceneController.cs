﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveFileSceneController : MonoBehaviour
{
    /*
     * khi mới vào chơi (khi nhấn Play), người dùng phải chọn chổ để lưu tiến trình (lưu HPmax, spirits,....).
     * Rồi trong lúc chơi, khi người chơi đi ngang qua checkpoint (bình đôm đốm), 
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
                string strTime = (minutesInt < 10) ? "0" + minutesInt : minutesInt.ToString();
                strTime += ":";
                strTime += (secondsInt < 10) ? "0" + secondsInt : secondsInt.ToString();
                strTime += ":";
                strTime += (seconds100Int < 10) ? "0" + seconds100Int : seconds100Int.ToString();
                SaveSlots[i].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "" + strTime;

                //hiện số spirits

                //hiện số HPmax

            }
            else
            {
                SaveSlots[i].transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Empty Slot " + i;
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
            // hiện bảng hỏi: có chắc muốn tạo file mới ? create, cancel
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
            updateSaveFileUI();
        }
        else
        {
            string path = Application.persistentDataPath + "/GameData_" + filenumber + ".dat";
            Debug.LogWarning("Save file not found in " + path);
        }


    }
}