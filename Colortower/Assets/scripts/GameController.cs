using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material[] pieceMaterials;
    [SerializeField] private ChastController chastPrefab;

    [SerializeField] private ReactiveDetal pieceXx8;
    [SerializeField] private ReactiveDetal pieceXx3;
    [SerializeField] private ReactiveDetal pieceXx4;
    [SerializeField] private ReactiveDetal pieceXx5;
    [SerializeField] private ReactiveDetal pieceXx6;
    [SerializeField] private ReactiveDetal pieceXx7;

    private Transform Tower;
    [SerializeField] private GameObject TowerPrefab;
    [SerializeField] private GameObject krishkaUp;
    [SerializeField] private GameObject krishkaDown;
    [SerializeField] private GameObject RotationController;

    [SerializeField] private Transform checker;

    private UImanager uiMan;

    private int cols = 8;
    private int rows = 7;
    private float scaler;
    private float deltaPos;
    private float deltaAngle;
    private ReactiveDetal piecePrefab;
    void initializeGame()
    { 
        deltaPos = (rows / 2) * 0.6f;
        if (rows % 2 == 0)
            deltaPos -= 0.3f;
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
        GameObject rotationController = Instantiate(RotationController) as GameObject;
        rotationController.transform.localScale = new Vector3(rotationController.transform.localScale.x, rotationController.transform.localScale.y / scaler, rotationController.transform.localScale.z);
        rotationController.GetComponent<RotationController>().tower = Tower;
        rotationController.GetComponent<RotationController>().angle = deltaAngle;
        //int[] counts = { 6,7, 7, 7, 7, 7, 7, 7, 1 };
        int[] counts = new int[cols + 1];
        counts[cols] = 1;
        counts[cols-1] = rows-1;
        for(int i=0;i<cols-1;i++)
        {
            counts[i] = rows;
        }
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
                    piece.transform.Rotate( 0,0, deltaAngle*j);
                    piece.transform.parent = chast.transform;
                    piece.GetComponent<ReactiveDetal>().setId(id, pieceMaterials[id],this,deltaAngle,deltaPos);
                }
            }
        }
    }
    public void SetGame(int col,int row,UImanager ui)
    {
        uiMan = ui;
        cols = col;
        rows = row;
        scaler =(float)row / 7;
        switch(cols)
        {
            case 3:deltaAngle = 120f;piecePrefab = pieceXx3; break;
            case 4:deltaAngle = 90f;piecePrefab = pieceXx4;break;
            case 5:deltaAngle = 72f;piecePrefab = pieceXx5;break;
            case 6:deltaAngle = 60f;piecePrefab = pieceXx6;break;
            case 7:deltaAngle = 360f/7f;piecePrefab = pieceXx7;break;
            case 8:deltaAngle = 45f;piecePrefab = pieceXx8; break;
        }
        initializeGame();
    }
    // Update is called once per frame
    public void CheckForWin()
    {
        int id = -1, prevId;
        bool line = true,win = false;
        for (int i = 0; i < cols; i++)
        {
            Vector3 pos = checker.position;
            pos.y -= deltaPos;
            for (int j = 0; j < rows; j++)
            {
                prevId = id;
                Collider[] cols = Physics.OverlapSphere(pos, 0.05f);
                if (cols.Length == 0)
                {
                    id = -1;
                }
                else
                { id = cols[0].GetComponent<ReactiveDetal>().id; }
                if (prevId == -1 || id == -1 || id == prevId)
                    line = true;
                else { line = false; break; }
                pos.y += 0.6f;
            }
            if (!line)
            { win = false; break; }
            else win = true;
            transform.Rotate(0, deltaAngle, 0);
            id = -1;
        }
        if(win)
        {
            uiMan.Win();
        }
    } 
}
