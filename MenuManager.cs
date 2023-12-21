using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject dataBoard, playButton, dataBoardButton;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void DataBoardButton()
    {
        DataManager.Instance.LoadData();
        dataBoard.transform.GetChild(1).GetComponent<Text>().text = "Total Bullet Shoot : " + DataManager.Instance.totalShootBullet.ToString();
        dataBoard.transform.GetChild(2).GetComponent<Text>().text = "Total Enemy Killed : " + DataManager.Instance.totalEnemyKilled.ToString();
        dataBoard.SetActive(true);
        playButton.SetActive(false);
        dataBoardButton.SetActive(false);

    }

    public void CloseButton()
    {
        dataBoard.SetActive(false);
        playButton.SetActive(true);
        dataBoardButton.SetActive(true);
    }
}
