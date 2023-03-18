using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float velocidad = 2;
    public GameObject col;
    public Renderer fondo;
    public List<GameObject> lstCol;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 21; i++)
        {
            lstCol.Add(Instantiate(col, new Vector2(-10f + i, -3f), Quaternion.identity));
        }
    }

    // Update is called once per frame
    void Update()
    {
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
    }
}
