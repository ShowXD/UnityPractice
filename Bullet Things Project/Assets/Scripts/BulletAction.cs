using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAction : MonoBehaviour
{
    public static float speedZ = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0, 0, speedZ));
        if (this.transform.position.z > 100.0f)
        {
            Destroy(this.gameObject);
        }
    }
}
