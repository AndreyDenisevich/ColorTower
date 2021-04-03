using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material[] pieceMaterials;
    [SerializeField] private ChastController chastPrefab;
    [SerializeField] private ReactiveDetal piecePrefab;

    private Transform Tower;
    [SerializeField] private GameObject TowerPrefab;
    [SerializeField] private GameObject krishkaUp;
    [SerializeField] private GameObject krishkaDown;
    [SerializeField] private GameObject RotationControllerDown;
    [SerializeField] private GameObject RotationControllerUp;

    [SerializeField] private Transform[] checkers;
    private int cols = 8;
    private int rows = 7;
    private float scaler;
    void initializeGame()
    { 
        float deltaPos = (rows / 2) * 0.6f;
        GameObject tow = Instantiate(TowerPrefab) as GameObject;
        tow.transform.position = transform.position;
        Tower = tow.transform;
        tow.transform.localScale = new Vector3(tow.transform.localScale.x, tow.transform.localScale.y*scaler, tow.transform.localScale.x);
        GameObject krishka = Instantiate(krishkaUp) as GameObject;
        krishka.transform.position = new Vector3(transform.position.x, transform.position.y + deltaPos+0.45f, transform.position.z);
        krishka.transform.parent = Tower;
        krishka = Instantiate(krishkaDown) as GameObject;
        krishka.transform.position = new Vector3(transform.position.x, transform.position.y - deltaPos-0.45f, transform.position.z);
        krishka.transform.parent = Tower;
        GameObject rotationController = Instantiate(RotationControllerDown) as GameObject;
        rotationController.transform.localScale = new Vector3(rotationController.transform.localScale.x, rotationController.transform.localScale.y / scaler, rotationController.transform.localScale.z);
        rotationController.GetComponent<RotationController>().tower = Tower;
        rotationController = Instantiate(RotationControllerUp) as GameObject;
        rotationController.transform.localScale = new Vector3(rotationController.transform.localScale.x, rotationController.transform.localScale.y / scaler, rotationController.transform.localScale.z);
        rotationController.GetComponent<RotationController>().tower = Tower;
        //int[] counts = { 6,7, 7, 7, 7, 7, 7, 7, 1 };
        int[] counts = new int[cols + 1];
        counts[cols] = 1;
        counts[cols-1] = rows-1;
        for(int i=0;i<cols-1;i++)
        {
            counts[i] = rows;
        }
       
        float deltaAngle = 360 / cols;
        for (int i = 0; i < rows; i++)
        {
            GameObject chast = Instantiate(chastPrefab.gameObject) as GameObject;
            chast.transform.position = new Vector3(transform.position.x, transform.position.y - deltaPos, transform.position.z);
            chast.transform.Translate(0, i * 0.6f, 0);
            chast.transform.parent = Tower;
            for (int j = 0; j < cols; j++)
            {
                int id;
                do
                {
                    id = Random.Range(0, cols+1);
                } while (counts[id] == 0);
                counts[id]--;
                if (id != cols)
                {
                    GameObject piece = Instantiate(piecePrefab.gameObject) as GameObject;
                    piece.transform.position = chast.transform.position;
                    piece.transform.Rotate(deltaAngle * j, 0, 0);
                    piece.transform.parent = chast.transform;
                    piece.GetComponent<ReactiveDetal>().setId(id, pieceMaterials[id],this);
                }
            }
        }
    }
    public void SetGame(int col,int row)
    {
        cols = col;
        rows = row;
        scaler =(float)row / 7;
        initializeGame();
    }
    // Update is called once per frame
    public void CheckForWin()
    {
        int id = 0, prevId;
        bool line = true,win = false;
        for (int i = 0; i < checkers.Length; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                prevId = id;
                Vector3 pos = checkers[i].position;
                pos.y += j * 0.6f;
                Collider[] cols = Physics.OverlapSphere(pos, 0.05f);
                if (cols.Length == 0)
                {
                    id = 0;
                }
                else
                { id = cols[0].GetComponent<ReactiveDetal>().id; }
                if (prevId == 0 || id == 0 || id == prevId)
                    line = true;
                else { line = false; break; }
            }
            if (!line)
            { win = false; break; }
            else win = true;
            id = 0;
        }
        if(win)
        {
            Debug.Log("you win!");
        }
    } 
}
