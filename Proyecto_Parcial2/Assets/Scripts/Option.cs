using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Option : MonoBehaviour
{
    public int OptionId;
    public string OptionName;

    public void Start()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }

    void Update()
    {
        
    }

    public void Updatetext()
    {
        transform.GetChild(0).GetComponent<TMP_Text>().text = OptionName;
    }
    public void SelectOptions()
    {
        LevelManager.Instance.setPlayerAnswer(OptionId);
        LevelManager.Instance.CheckPlayerState();
    }
}
