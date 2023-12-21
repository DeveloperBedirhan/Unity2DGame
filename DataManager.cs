using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TigerForge;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    private int shootBullet, enemyKilled;
    public int totalShootBullet, totalEnemyKilled;
    EasyFileSave myFile;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            StartProcess();
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int ShootBullet
    {
        get 
        {
            return shootBullet; 
        }
        set 
        { 
            shootBullet = value;
            GameObject.Find("ShootBulletText").GetComponent<Text>().text = "SHOOT BULLET: " + shootBullet.ToString();
        }
    }

    public int EnemyKilled
    {
        get 
        { 
            return enemyKilled; 
        }
        set 
        { 
            enemyKilled = value;
            GameObject.Find("EnemyKilledText").GetComponent<Text>().text = "ENEMY KILLED: " + enemyKilled.ToString();
            WinProcess();
        }
    }

    void StartProcess()
    {
        myFile = new EasyFileSave();
        LoadData();
    }

    public void SaveData()
    {
        totalShootBullet += shootBullet;
        totalEnemyKilled += enemyKilled;
        myFile.Add("Total Shoot Bullet", totalShootBullet);
        myFile.Add("Total Enemy Killed", totalEnemyKilled);
        myFile.Save();
    }

    public void LoadData()
    {
        if (myFile.Load())
        {
            totalShootBullet = myFile.GetInt("Total Shoot Bullet");
            totalEnemyKilled = myFile.GetInt("Total Enemy Killed");
        }
    }

    public void WinProcess()
    {
        if(enemyKilled >= 5)
        {
            GameObject.Find("GameInfoText").GetComponent<Text>().text = "You Won!";
            Time.timeScale = 0;
        }
    }

    public void LoseProcess()
    {
        GameObject.Find("GameInfoText").GetComponent<Text>().text = "Game Over!";
    }
}
