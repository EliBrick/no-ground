using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Loads a new scene, while also clearing level-specific inventory!*/

public class SceneLoadTrigger : MonoBehaviour
{

    [SerializeField] string loadSceneName;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject == NewPlayer.Instance.gameObject)
        {
            string gsn = SceneManager.GetActiveScene().name;
            if (gsn.Contains("Level ") && loadSceneName.Equals(""))
            {
                string name = gsn.Substring(gsn.Length - 2);
                int i = int.Parse(name);
                name = "Level " + (i+1);
                GameManager.Instance.hud.loadSceneName = name;
            }
            else
            {
                GameManager.Instance.hud.loadSceneName = loadSceneName;
            }
            GameManager.Instance.inventory.Clear();
            GameManager.Instance.hud.animator.SetTrigger("coverScreen");
            enabled = false;
        }
    }
}
