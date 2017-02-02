using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour {
  public void exitProgramBtn() {
    Application.Quit();
  }

  public void NewGameBtn(string PlayGame) {
    SceneManager.LoadScene(PlayGame);
  }

  public void NewHelperGameBtn(string PlayGameHelper)
  {
    SceneManager.LoadScene(PlayGameHelper);
  }
}
