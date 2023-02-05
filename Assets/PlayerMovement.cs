using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayerMovement : MonoBehaviour
{
    /*
    int Zivot = 3;
    float Zdravlje = 100;
    string Info = "Game Over";
    bool Mrtav = false;     */

    public TextMeshProUGUI Rezultat;
    public TextMeshProUGUI HighScore;

    public Rigidbody RB;
    public int Brzina = 15;
    public int BrzinaPomicanja = 3;
    public bool isGround = false;
    int trenutniRezultat;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore",0);
        }
        HighScore.text= "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore");
        StartCoroutine(ScoreTime());
    }
    IEnumerator ScoreTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            trenutniRezultat += 1;
            Rezultat.text = "REZULTAT: " + trenutniRezultat;

            if (trenutniRezultat > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", trenutniRezultat);
            }
            HighScore.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Smrt")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (collision.gameObject.tag == "Pod")
        {
            isGround = true;
        }
    }
   
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Pod")
        {
            isGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SpeedBoost")
        {
            Brzina = 16;
            BrzinaPomicanja = 16;
            StartCoroutine(BackSpeed());
            //  Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "ReBoost")
        {
            Brzina = 6;
            BrzinaPomicanja = 7;
            StartCoroutine(BackSpeed());
            //  Destroy(other.gameObject);
        }
        if (other.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    IEnumerator BackSpeed()
    {
        yield return new WaitForSeconds(3);
        Brzina = 10;
        BrzinaPomicanja = 10;
    }

    void Update()
    {
        /* if (Zdravlje<=0)
         {
             Zivot -= 1;
            Mrtav= true;
         }
         if (Zivot <= 0)
         {
             Debug.Log(Info);
         }*/
        
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            RB.AddForce(0, 400, 0);
        }


        //transform.position.y<=-3 kad padne kocka sa terena da refresha ponovo scenu
        if (transform.position.y <= -1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        // Sintaksa koja nam daje sposobnost da se igrač kreće prema ravno
        transform.Translate(Brzina * Time.deltaTime, 0, 0, Space.Self);

        // Provjera koju smo tipku pritisnuli (u ovom slučaju "A")
        if (Input.GetKey(KeyCode.A))
        {
            // Daje nam sposobnost kretanu ka desno
            transform.Translate(0, 0, BrzinaPomicanja * Time.deltaTime, Space.Self);
        }

        // Provjera koju smo tipku pritisnuli (u ovom slučaju "D")
        if (Input.GetKey(KeyCode.D))
        {
            // Daje nam sposobnost kretanu ka ljevo
            transform.Translate(0, 0, -BrzinaPomicanja * Time.deltaTime, Space.Self);
        }
    }
}