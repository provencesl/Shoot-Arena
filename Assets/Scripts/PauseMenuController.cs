using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
    
public class PauseMenuController : MonoBehaviour {

    [SerializeField] private GameObject pauseMenuTextField;
    [SerializeField] private Button restartLevelButton, mainMenuButton, exitGameButton;

    private HashSet<GameObject> pauseMenuItems;
    private bool menuActive = false;

    public IEnumerator EndMatch(string victoriousPlayerName) {
        yield return new WaitForSeconds(2);
        Time.timeScale = 0;
        ShowEndingMenu(victoriousPlayerName);
    }

    void Start() {

        Time.timeScale = 1;

        pauseMenuItems = new HashSet<GameObject>();

        foreach(GameObject menuItem in GameObject.FindGameObjectsWithTag("PauseMenu")) {
            pauseMenuItems.Add(menuItem);
        }
        
        ToggleMenuItems();

        restartLevelButton.onClick.AddListener(Reload);
        mainMenuButton.onClick.AddListener(() => LoadLevel("Arena1"));
        exitGameButton.onClick.AddListener(() => Application.Quit());
        
    }

    // Update is called once per frame
    void Update() {
        CheckInput();
    }

    void CheckInput() {
        if(Input.GetButtonDown("Pause") && !menuActive) {
            ShowPauseMenu();
        } else if(Input.GetButtonDown("Pause") && menuActive) {
            ResumeGame();
        }
    }

    void ResumeGame() {
        menuActive = false;
        ToggleMenuItems();
        Time.timeScale = 1;
    }

    void ShowPauseMenu() {
        Time.timeScale = 0;
        SetPauseMenuText("Paused");
        menuActive = true;
        ToggleMenuItems();
    }

    void ShowEndingMenu(string victoriousPlayerName) {
        SetPauseMenuText(victoriousPlayerName + "  Wins !");
        menuActive = true;
        ToggleMenuItems();
    }

    void ToggleMenuItems() {
        foreach(GameObject obj in pauseMenuItems) {
            obj.SetActive(menuActive);
        }
    }

    void SetPauseMenuText(string title) {
        pauseMenuTextField.GetComponent<Text>().text = title;
    }

    void Reload(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void LoadLevel(string level){
		SceneManager.LoadScene(level);
	}
}