using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
   [SerializeField] private float deathDelayTime = 5.0f;
   
   public void LoadGameOver()
   {
      StartCoroutine(DelayOfGame());
      
   }

   public void LoadGame()
   {
      SceneManager.LoadScene("Main Gameplay");
   }

   public void LoadStartMenu()
   {
      SceneManager.LoadScene(0);
   }

   public void QuitGame()
   {
      Application.Quit();
   }

   IEnumerator DelayOfGame()
   {
      yield return new WaitForSeconds(deathDelayTime);
      SceneManager.LoadScene("Game Over");
   }
   
}
