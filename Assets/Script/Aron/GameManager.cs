using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.XR;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public AudioSource audio_player;

    public ArrayList cubes = new ArrayList();
    public Cube_spawner[] spawners;

    public GameObject gameplay_UI;
    public GameObject pause_UI;
    private bool pause_visibel = false;
    //public GameObject game_over_UI;

    public int max_cube_amount = 32;
    public float time = 20f;

    public TextMeshProUGUI score_tpm;
    public TextMeshProUGUI time_left_tpm;
    public TextMeshProUGUI pause_score;
    public TextMeshProUGUI pause_best_score;
    public TextMeshProUGUI pause_time;
    public TextMeshProUGUI game_over_score;
    public TextMeshProUGUI game_over_best_score;

    public Slider gameplay_music_slider;

    private int score = 0;
    private static string score_string = "Score: ";
    private static string time_string = "Time left: ";
    private static string best_score_string = "Best score: ";

    public static string gameplay_sound_PP = "gameplaySound";
    public static string menu_sound_PP = "menuSound";
    public static string difficulty_PP = "difficulty";
    public static string best_score_PP = "bestscore";

    //public TextMeshProUGUI text;

    [SerializeField]
    private XRNode xrNode = XRNode.LeftHand;

    private List<InputDevice> devices = new List<InputDevice>();

    private InputDevice device;

    private bool pauseWait = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        device = devices.FirstOrDefault();
    }

    void OnEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
    }

    // Start is called before the first frame update
    public void Start()
    {
        Time.timeScale = 1f;

        /*audio_player.volume = PlayerPrefs.GetFloat(gameplay_sound_PP);

        gameplay_music_slider.value = PlayerPrefs.GetFloat(gameplay_sound_PP);

        if (PlayerPrefs.HasKey(difficulty_PP))
        {
            switch (PlayerPrefs.GetInt(difficulty_PP))
            {
                case 0:
                    Cube_hower.instace.hower = 11;
                    break;
                case 1:
                    Cube_hower.instace.hower = 15;
                    break;
                case 2:
                    Cube_hower.instace.hower = 20;
                    break;
                case 3:
                    Cube_hower.instace.hower = 45;
                    break;
            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        SetTime();

        if (time <= 0)
        {
            //ShowGameOver();
        }

        if (cubes.Count < max_cube_amount)
        {
            int random_spawner = Random.Range(0, 4);

            spawners[random_spawner].SpawnCube();
        }

        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPauseMenu();
        }*/

        if (PlayerPrefs.GetInt(best_score_PP) < score)
        {
            PlayerPrefs.SetInt(best_score_PP,score);
        }

        if (!device.isValid)
        {
            GetDevice();
        }

        bool primarybuttonaction = false;

        if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primarybuttonaction) && primarybuttonaction)
        {
            if (!pauseWait)
            {
                if (!pause_visibel)
                {
                    ShowPauseMenu();
                    pause_visibel = true;
                }
                else
                {
                    Resume();
                    pause_visibel = false;
                }
                StartCoroutine(PauseWait());
            }
        }
    }

    IEnumerator PauseWait()
    {
        pauseWait = true;
        yield return new WaitForSecondsRealtime(1);
        pauseWait = false;
    }

    public void ShowPauseMenu()
    {
        Time.timeScale = 0f;

        //gameplay_UI.SetActive(false);
        pause_UI.SetActive(true);
        
        pause_score.text = score_string + score;
        pause_best_score.text = best_score_string + PlayerPrefs.GetInt(best_score_PP);
        pause_time.text = time_string + time.ToString("F1");
    }

    public void ShowGameOver()
    {
        Time.timeScale = 0f;

        gameplay_UI.SetActive(false);
        //game_over_UI.SetActive(true);

        ShowPauseMenu();

        //game_over_score.text = score.ToString();

        if (PlayerPrefs.HasKey(best_score_PP))
        {
            int best_score = PlayerPrefs.GetInt(best_score_PP);
            if (best_score < score)
            {
                PlayerPrefs.SetInt(best_score_PP, score);
            }
        }
        else
        {
            PlayerPrefs.SetInt(best_score_PP, score);
        }

        game_over_best_score.text = PlayerPrefs.GetInt(best_score_PP).ToString();
    }

    public void ChangeGameplayMusic_ingame()
    {
        //audio_player.volume = gameplay_music_slider.value;
        //PlayerPrefs.SetFloat(gameplay_sound_PP, gameplay_music_slider.value);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        
        //gameplay_UI.SetActive(true);
        pause_UI.SetActive(false);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private void SetTime()
    {
        time -= Time.deltaTime;
        time_left_tpm.text = "Time left: " + time.ToString("F1");
    }

    public void SetScore(int score)
    {
        time += score;
        this.score += score;
        score_tpm.text = "Score: " + this.score.ToString();
    }

    public void CubeTime(int time)
    {
        this.time += time;
        time_left_tpm.text = "Time left: " + time.ToString("F1");
    }

    public void ExitApp()
    {
        SceneManager.LoadScene(0);
    }
}
