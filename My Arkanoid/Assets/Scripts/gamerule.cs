using UnityEngine;
using System.Collections;

public class gamerule : MonoBehaviour {
	
    // Can be modify Variables
	public float m_x_speed = 3f;
	public float m_y_speed = 3f;
    public GameObject m_prefab_ball;
    public GameObject m_padding_bar;

    // Judge Variable
	float m_x_positive = 1;
	float m_y_positive = 1;
    int m_WinOrLose = 0;

    void Start () {
        // Set the ball position
		gameObject.transform.position = new Vector3(0f,-3.4f,0);	
		
		for(float y = 1;y <= 5;y = y + 0.6f )
        {
		    for(float x =- 4;x <= 5;x = x + 1.2f)
            {
                GameObject t_cube = Instantiate(m_prefab_ball, new Vector3(x,y, 0), Quaternion.identity) as GameObject;
                t_cube.name="Wall";
				t_cube.GetComponent<Renderer> ().material.color = new Color(x*50/255f, y*50/255f, 1f);	
		    }
		}
	}

	void Update () {
		if(gameObject.transform.position.y <- 6.0f)
        {
			m_WinOrLose = 2;
		}

		if(mScore >= 8*7)
        {
			m_WinOrLose = 1;
		}

		float t_Hvalue = Input.GetAxis ("Horizontal");
 
        m_padding_bar.transform.position = new Vector3 (m_padding_bar.transform.position.x+(t_Hvalue*10.0f*Time.deltaTime),-4.5f, 0);
		
		Vector3 t_v = m_padding_bar.transform.position;

		if(t_v.x > 5.52f)
        {
 		    t_v.x = 5.52f;
		}

		if(t_v.x<-5.65f)
        {
		    t_v.x=-5.65f;
		}
		
		m_padding_bar.transform.position=t_v;

	    Vector3 t_BallVector3=gameObject.transform.position; 
		     
		t_BallVector3.x = t_BallVector3.x+m_x_positive*(m_x_speed*Time.deltaTime);
		t_BallVector3.y = t_BallVector3.y+m_y_positive*(m_y_speed*Time.deltaTime);
		gameObject.transform.position = t_BallVector3;
	}

	 void OnTriggerEnter(Collider other)
    {
		if(other.transform.name == "Wall")
        {
			Destroy(other.gameObject);
			mScore = mScore+1;
			m_y_positive =- m_y_positive;
		}
		
		if(other.transform.name == "Padding_bar" || other.transform.name == "Block_top" )
        {
			 m_y_positive =- m_y_positive;
		}
		if(other.transform.name == "Block_right" || other.transform.name == "Block_left"   )
        {
			 m_x_positive =- m_x_positive;
		}
    }
	
    // Score variable
	int mScore = 0;	
    
    // Setting GUI
	void OnGUI () {
      GUI.Label(new Rect(20,20,400,400)," Score:" + mScore);

		if(m_WinOrLose != 0)
        {
			m_x_speed = 0;
	        m_y_speed = 0;
		}
		if(m_WinOrLose == 1)
        {
     		 GUI.Label(new Rect(100,20,400,400)," You win");
		}

		if(m_WinOrLose == 2)
        {
     		 GUI.Label(new Rect(100,20,400,400)," You Lose");
		}
    }
}
