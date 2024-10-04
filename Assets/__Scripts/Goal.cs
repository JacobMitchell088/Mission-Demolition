using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent (typeof(Renderer))]
public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    void OnTriggerEnter(Collider other) {
        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null) {
            Goal.goalMet = true;

            Material mat = GetComponent<Renderer>().material;
            Color c = mat.color;
            c.a = 0.75f;
            mat.color = c;
            StartCoroutine(LoadGameOverScene());
        }
    }



    IEnumerator LoadGameOverScene() {
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene("_GameOver");
    }
}
