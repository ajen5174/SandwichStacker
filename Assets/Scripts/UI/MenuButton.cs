using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField] Sprite volumeOnSprite = null;
    [SerializeField] Sprite volumeOffSprite = null;



    private Button volumeButton = null;
    private bool volumeOn = true;

    // Start is called before the first frame update
    void Start()
    {
        volumeButton = GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VolumeButtonClicked()
    {
        volumeButton.image.sprite = volumeOn ? volumeOffSprite : volumeOnSprite;

        volumeOn = !volumeOn;

    }

    public void LeaderBoardButtonClicked()
    {

    }

    public void EasyButtonClicked()
    {

    }

    public void MediumButtonClicked()
    {

    }

    public void HardButtonClicked()
    {

    }


}
