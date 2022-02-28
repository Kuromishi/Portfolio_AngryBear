using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    public float m_jumpheight;
    public float m_moveSpeed;//public 可视
    public GameObject m_Camera;
    public GameObject m_ball;
    public Transform m_ballSpawn;
    public float ballSpeed;
    public string changeString;
    private Rigidbody rb;
    private Ball[] allBalls;

    public GameObject BGImage;

    public Text changText;
    [SerializeField]//让n_num在界面也可视
    private float n_num;
    public bool b = false;
    private float mouseX;
    private float mouseY;
    private bool isPaused;
    void Awake()
    {
        Debug.Log("Awaken");
        Debug.Log("Test string");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start Yay!");
        BGImage.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");
            rb.velocity = transform.TransformVector(new Vector3(-(m_moveSpeed * Input.GetAxis("Horizontal")), rb.velocity.y, -(m_moveSpeed * Input.GetAxis("Vertical"))));
            //Debug.Log("Update");
            if (Input.GetKeyDown(KeyCode.Space) && b == true)//getkeydown按下才有反应
            {
                Test(3f);
                rb.velocity = new Vector3(rb.velocity.x, m_jumpheight, rb.velocity.z);//给物理系统增加速度，add 3 to the y//y轴是jumpheight
            }

            m_Camera.transform.Rotate(-mouseY, 0, 0);//绕着x轴，上下转
            gameObject.transform.Rotate(0, mouseX, 0);//绕着y轴 ，偏移的程度

            if (Input.GetMouseButton(0))//0是鼠标左键,1是鼠标右键
            {
                Debug.Log("MouseClick");
            }

            //if(Input.GetKey(KeyCode.S))
            //{
            //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, m_moveSpeed);
            //}
            //if (Input.GetKey(KeyCode.D))
            //{
            //    rb.velocity = new Vector3(-m_moveSpeed, rb.velocity.y, rb.velocity.z);
            //}
            //if (Input.GetKey(KeyCode.W))
            //{
            //    rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -m_moveSpeed);
            //}
            //if (Input.GetKey(KeyCode.A))
            //{
            //    rb.velocity = new Vector3(m_moveSpeed, rb.velocity.y, rb.velocity.z);
            //}

            if (Input.GetKeyDown(KeyCode.Q))
            {
                GamePause();
            }

            if (Input.GetMouseButtonDown(0))//0是鼠标左键,1是鼠标右键
            {
                GameObject ball;
                ball = Instantiate(m_ball, m_ballSpawn.position, gameObject.transform.rotation);
                //ball.GetComponent<Rigidbody>().AddForce(-ball.transform.forward * ballSpeed);
                //ball.GetComponent<Rigidbody>().AddForce(ball.transform.up * ballSpeed);
                Destroy(ball, 5);
            }

            allBalls = FindObjectsOfType<Ball>();//return all Ball Objects in the scene

            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(allBalls.Length);
            }

            if (allBalls.Length > 10)
            {
                Debug.Log("You Win!");
            }
        }
       
    }

    public void GamePause()
    {
        if(BGImage.activeSelf)
        {
            BGImage.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
        }

        else if (!BGImage.activeSelf)
        {
            BGImage.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
        }
    }

    public void ChangeScene()
    {
        //SceneManager.LoadScene("Test");
        //SceneManager.LoadScene(1);//1=Test scene
        int sceneNum = SceneManager.GetActiveScene().buildIndex;
        //sceneNum++;
        SceneManager.LoadScene(++sceneNum);//buildsetting

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Terrain")
        {
            b = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            b = false;
        }
    }

    private void Test(float num)
    {
        Debug.Log(num);
    }
}
