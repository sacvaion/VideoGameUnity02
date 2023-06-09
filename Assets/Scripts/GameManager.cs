using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuPrincipal;
    public GameObject menuGameOver;
    public GameObject uiScore;
    public TextMeshProUGUI bestPoint;
    public TextMeshProUGUI pointText;

    public float velocidad = 2;
    public GameObject col;
    public GameObject piedra01;
    public GameObject piedra02;
    public Renderer fondo;
    public List<GameObject> lstCol;
    public List<GameObject> lstObstaculos=null;

    public bool gameOver = false;
    public bool start = false;

    private int points=0;
    private int maxPoints;

    // Start is called before the first frame update
    void Start()
    {
        //crear mapa
        for (int i = 0; i < 21; i++)
        {
            lstCol.Add(Instantiate(col, new Vector2(-10f + i, -3f), Quaternion.identity));
        }
        //crear piedras
        lstObstaculos.Add(Instantiate(piedra01,new Vector2(14,-2),Quaternion.identity));
        lstObstaculos.Add(Instantiate(piedra02,new Vector2(18,-2),Quaternion.identity));
        bestPoint.text = "BEST : " + GetMaxScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        //uiScore.SetActive(false) ;
        if (!start)
        {
            if (Input.GetKeyDown(KeyCode.X))
             {
                start = true;
                //uiScore.SetActive(true);
            }
        }

        if (start && gameOver)
        {
            menuGameOver.SetActive(true);
            if (Input.GetKeyDown(KeyCode.X))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if (start && !gameOver)
        {
            menuPrincipal.SetActive(false);
            fondo.material.mainTextureOffset += new Vector2(0.02f, 0) * Time.deltaTime;

            //mover mapa
            for (int i = 0; i < lstCol.Count(); i++)
            {
                if (lstCol[i].transform.position.x <= -10f)
                {
                    lstCol[i].transform.position = new Vector3(10, -3, 0);
                }
                lstCol[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }

            //mover obtaculos
            for (int i = 0; i < lstObstaculos.Count(); i++)
            {
                if (lstObstaculos[i].transform.position.x <= -10f)
                {
                    float randomObs = Random.Range(11, 18);
                    lstObstaculos[i].transform.position = new Vector3(randomObs, -2, 0);
                }
                lstObstaculos[i].transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * velocidad;
            }
        }
    }

    public void IncreasePoints()
    {
        points++;
        pointText.text ="SCORE : " + points.ToString();
        if(points >= GetMaxScore())
        {
            bestPoint.text = "BEST : "+ points.ToString();
            SetMaxScore(points);
        }
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("Max Points",0);
    }

    public void SetMaxScore(int currentPoints)
    {
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }
}
