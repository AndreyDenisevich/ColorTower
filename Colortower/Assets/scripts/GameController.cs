using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Material[] pieceMaterials;
    [SerializeField] private ChastController chastPrefab;
    [SerializeField] private ReactiveDetal piecePrefab;
    [SerializeField] private Transform Tower;
    [SerializeField] private Transform[] checkers;
    private int rows = 7;
    void Start()
    {
        int[] counts = { 6,7, 7, 7, 7, 7, 7, 7, 1 };
        for (int i = 0; i < 7; i++)
        {
            GameObject chast = Instantiate(chastPrefab.gameObject) as GameObject;
            chast.transform.position = transform.position;
            chast.transform.Translate(0, i * 0.6f, 0);
            chast.transform.parent = Tower;
            for (int j = 0; j < 8; j++)
            {
                int id;
                do
                {
                    id = Random.Range(0, 9);
                } while (counts[id] == 0);
                counts[id]--;
                if (id != 8)
                {
                    GameObject piece = Instantiate(piecePrefab.gameObject) as GameObject;
                    piece.transform.position = chast.transform.position;
                    piece.transform.Rotate(45 * j, 0, 0);
                    piece.transform.parent = chast.transform;
                    piece.GetComponent<ReactiveDetal>().setId(id, pieceMaterials[id]);
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (CheckForWin())
            Debug.Log("You Win");
    }
    public bool CheckForWin()
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
        return win;
    } 
}
