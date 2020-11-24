using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    [SerializeField] GameObject content = null;

    Sprite volumeOnSprite = null;
    Sprite volumeOffSprite = null;

    Sprite pauseSprite = null;
    Sprite playSprite = null;


    private Button button = null;
    private bool volumeOn = true;
    private bool isPaused = false;

    static int playerNumber = 1;
    void Start()
    {
        button = GetComponent<Button>();
    }

    void Update()
    {
        
    }

    public void VolumeButtonClicked()
    {
        if(button != null)
		{
            if(volumeOnSprite == null)
			{
                volumeOnSprite = Resources.Load<Sprite>("Sprites/UI/VolumeOnSprite");
                volumeOffSprite = Resources.Load<Sprite>("Sprites/UI/VolumeOffSprite");
			}
            button.image.sprite = volumeOn ? volumeOffSprite : volumeOnSprite;
            volumeOn = !volumeOn;
		}

        AudioListener.volume = volumeOn ? 1.0f : 0.0f;
    }

    public void LeaderBoardButtonClicked()
    {
        Instantiate(Resources.Load("Prefabs/LeaderboardPanel"), GetComponentInParent<Canvas>().gameObject.transform);
    }

    public void EasyButtonClicked()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("GameScene");

    }

    public void MediumButtonClicked()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("GameScene");

    }

    public void HardButtonClicked()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        SceneManager.LoadScene("GameScene");

    }

    public void QuitGameClicked()
	{
        //switch back to main menu
        Screen.orientation = ScreenOrientation.Portrait;
        SceneManager.LoadScene("MainMenu");
    }

    public void PauseButtonClicked()
	{
        if (button != null)
        {
            if (playSprite == null)
            {
                playSprite = Resources.Load<Sprite>("Sprites/UI/PlaySprite");
                pauseSprite = Resources.Load<Sprite>("Sprites/UI/PauseSprite");
            }
            button.image.sprite = isPaused ? playSprite : pauseSprite;
            isPaused = !isPaused;
        }
    }

    public void InstructionButtonClicked()
	{
        Instantiate(Resources.Load("Prefabs/InstructionsPanel"), GetComponentInParent<Canvas>().gameObject.transform);
    }

    public void ClosePopupButton()
	{
        var images = GetComponentsInParent<Image>();
        foreach(var i in images)
        {
            Destroy(i.gameObject);

        }

        playerNumber = 1;

	}

    public void AddPlayerClick()
    {
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/PlayerScore"), content.transform);
        PlayerScore ps = go.GetComponent<PlayerScore>();
        if(ps)
        {
            ps.SetInfo(playerNumber, "Name Number " + playerNumber, playerNumber * 100);

            playerNumber++;
        }
    }

}
