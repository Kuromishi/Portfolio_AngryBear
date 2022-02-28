using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Bearshooting : MonoBehaviour
{
    public GameObject m_Camera;
    public GameObject m_ball;
    public Transform m_ballSpawn;
    public float ballSpeed;
    public string changeString;
    private Rigidbody rb;
    private float bearNum=7;

    GameObject [] frogs;

    public GameObject BGImage;//BGImage=PauseImage
    public GameObject WinImage;
    public GameObject LoseImage;

    public Text changeText;

    public bool b = false;
    private float mouseX;
    private float mouseY;
    private bool isPausedForBG;
    private bool isPausedForWin;
    private bool isPausedForLose;


    public GameObject FirePoint;
    public Camera Cam;
    public float MaxLength;
    public GameObject[] Prefabs;
    public Material[] laserMaterial;

    private Ray RayMouse;
    private Vector3 direction;
    private Quaternion rotation;

    private int Prefab;
    private GameObject Instance;
    private EGA_Laser LaserScript;
    
    //Double-click protection
    private float buttonSaver = 0f;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        BGImage.SetActive(false);
        WinImage.SetActive(false);
        LoseImage.SetActive(false);
        isPausedForBG = false;
        isPausedForWin = false;
        isPausedForLose = false;
        Cursor.lockState = CursorLockMode.Locked;
        Counter(0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPausedForBG && !isPausedForWin  && !isPausedForLose)
        {
            mouseX = Input.GetAxis("Mouse X");
            mouseY = Input.GetAxis("Mouse Y");

            m_Camera.transform.Rotate(-mouseY, 0, 0);
            gameObject.transform.Rotate(0, mouseX, 0);

            if (Input.GetKeyDown(KeyCode.Q))
            {
                GamePause();
            }

            if (Input.GetMouseButtonDown(0)&& bearNum>0)//0是鼠标左键,1是鼠标右键
            {
                GameObject ball;
                ball = Instantiate(m_ball, m_ballSpawn.position, m_ballSpawn.transform.rotation);
                bearNum--;
                changeText.text = bearNum.ToString();
                Destroy(ball, 5);
            }

            if(Input.GetMouseButtonDown(1))
            {
                Destroy(Instance);
                Instance = Instantiate(Prefabs[Prefab], FirePoint.transform.position, FirePoint.transform.rotation);
                Instance.transform.parent = transform;
                LaserScript = Instance.GetComponent<EGA_Laser>();
                Ray ray1 = Cam.ScreenPointToRay(new Vector3(Screen.width/2,Screen.height/2));
                RaycastHit hit1;
                if (Physics.Raycast(ray1, out hit1))
                {
                    if (hit1.collider.gameObject.tag == "Frogs")
                    {
                        Debug.Log("检测到蛙蛙");
                        if(Prefab == 0)
                        {
                            hit1.collider.gameObject.GetComponent<SkinnedMeshRenderer>().material = laserMaterial[0];
                        }
                        if (Prefab == 1)
                        {
                            hit1.collider.gameObject.GetComponent<SkinnedMeshRenderer>().material = laserMaterial[1];
                        }
                        if (Prefab == 2)
                        {
                            hit1.collider.gameObject.GetComponent<SkinnedMeshRenderer>().material = laserMaterial[2];
                        } 
                    }
                }
            }

            if (Input.GetMouseButtonUp(1))
            {
                LaserScript.DisablePrepare();
                Destroy(Instance, 1);

            }

            //A和D键控制激光切换
            if ((Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0) && buttonSaver >= 0.4f)
            {
                buttonSaver = 0f;
                Counter(-1);
            }
            if ((Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0) && buttonSaver >= 0.4f)
            {
                buttonSaver = 0f;
                Counter(+1);
            }
            buttonSaver += Time.deltaTime;

            frogs = GameObject.FindGameObjectsWithTag("Frogs");

            if(frogs.Length == 0)
            {
                WinGame();
            }

            if (bearNum ==0 && GameObject.Find("Bip001 Pelvis") == null&& frogs.Length != 0)
            {
                LoseGame();
            }

        }

    }

    public void GamePause()
    {
        if (BGImage.activeSelf)
        {
            BGImage.SetActive(false);
            Time.timeScale = 1;
            isPausedForBG = false;
        }

        else if (!BGImage.activeSelf)
        {
            BGImage.SetActive(true);
            Time.timeScale = 0;
            isPausedForBG = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void StartInterface()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }
  
    public void WinGame()
    {

         if (!WinImage.activeSelf)
        {
            WinImage.SetActive(true);
            Time.timeScale = 0;
            isPausedForWin = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void LoseGame()
    {

         if (!LoseImage.activeSelf)
        {
            LoseImage.SetActive(true);
            Time.timeScale = 0;
            isPausedForLose = true;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    void Counter(int count)
    {
        Prefab += count;
        if (Prefab > Prefabs.Length - 1)
        {
            Prefab = 0;
        }
        else if (Prefab < 0)
        {
            Prefab = Prefabs.Length - 1;
        }
    }
}
