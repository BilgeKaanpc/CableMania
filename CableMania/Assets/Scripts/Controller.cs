using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public GameManager _GameManager;
    public int collusionIndex;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] HitColl = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);
        for (int i = 0; i < HitColl.Length; i++)
        {
            if (HitColl[i].CompareTag("wire"))
            {
                _GameManager.ColControl(collusionIndex,false);
            }
            else
            {
                _GameManager.ColControl(collusionIndex, true);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale / 2);
    }
}
