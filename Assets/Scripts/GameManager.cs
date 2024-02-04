using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YF;

public enum VehicleType
{
    Car,
    Truck,
    MotorCycle
}
public class GameManager : MonoSingleton<GameManager>
{
    public string gameScene;
    public Button startBtn;
    public Button quitBtn;
    public Dropdown Dropdown;
    public GameObject initPanel;
    public GameObject settingPanel;
    public Button confirmBtn;
    public Color selectedColor;
    public VehicleType currentVehicleType;
    // Start is called before the first frame update
    void Start()
    {
        currentVehicleType = VehicleType.Car;
        selectedColor = Color.green;
        initPanel.SetActive(true);
        settingPanel.SetActive(false);
        startBtn.onClick.AddListener(StartGame);
        quitBtn.onClick.AddListener(QuitGame);
        Dropdown.onValueChanged.AddListener(VehicleSelect);
        confirmBtn.onClick.AddListener(ConfirmSetting);
    }

    public void VehicleSelect(int type)
    {
        currentVehicleType = (VehicleType)type;
    }

    public void StartGame()
    {
        initPanel.SetActive(false);
        settingPanel.SetActive(true);
    }

    public void QuitGame()
    {
#if UNITY_WINDOW
        Application.Quit();
# elif UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void ConfirmSetting()
    {
        SceneManager.LoadScene(gameScene);
    }

}
