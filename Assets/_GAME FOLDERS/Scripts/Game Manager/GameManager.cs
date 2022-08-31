using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Ball[] ball;
    [SerializeField] private GameObject[] hookCenters;

    [SerializeField] private AudioSource[] sounds; // 0 :Defeat 1 :Win 2:KEsme
    public int availableBallCount; // Sahip olunan toplam top sayisi
    public int targetObjectCount; //devrilmesi istenilen hedef obje sayisi

    [SerializeField] private GameObject[] panels; //0: Pause 1:Lose 2:Win

    [SerializeField] private ParticleSystem[] particalSystem;
    [SerializeField] private ParticleSystem winParticalSystem;
    int _particalCount;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit.collider != null)
            {
                sounds[2].Play();
                ParticalController(hit.transform);
                if (hit.collider.CompareTag("Center_1"))
                {
                    /*AllChainOnDisable(hit,"Center_1");*/
                    BallChainOnDisable(hit, 0, "Center_1");
                }
                else if (hit.collider.CompareTag("Center_2"))
                {
                    /*AllChainOnDisable(hit,"Center_2");*/
                    BallChainOnDisable(hit, 0, "Center_2");
                }
                else if (hit.collider.CompareTag("Center_3"))
                {
                    /*AllChainOnDisable(hit,"Center_4");*/
                    BallChainOnDisable(hit, 1, "Center_3");
                }
                else if (hit.collider.CompareTag("Center_4"))
                {
                    /*AllChainOnDisable(hit,"Center_4");*/
                    BallChainOnDisable(hit, 1, "Center_4");
                }
            }
        }
    }
    // Topun uzerindeki zincirin yok edilmesi
    private void BallChainOnDisable(RaycastHit2D hit, int ballValue, string centerName)
    {
        hit.collider.gameObject.SetActive(false);
        ball[ballValue].hingeController[centerName].enabled = false;
    }
    // Tum zincirlerin kapatilmasi icin 
    private void AllChainOnDisable(RaycastHit2D hit, string centerName)
    {
        hit.collider.gameObject.SetActive(false);
        foreach (var item in hookCenters)
        {
            if (item.GetComponent<RopeManager>().centerName == centerName)
            {
                foreach (var item2 in item.GetComponent<RopeManager>().connectObjects)
                {
                    item2.SetActive(false);
                }
            }
        }
    }

    public void FallBall()
    {
        availableBallCount--;
        if (availableBallCount == 0)
        {
            if (targetObjectCount > 0)
            {
                sounds[0].Play();
                panels[1].SetActive(true);
                Debug.Log("DEFEAT GAME");
            }
            else if (targetObjectCount == 0)
            {
                sounds[1].Play();
                panels[2].SetActive(true);
                WinLoseParticalEffect();
                Debug.Log("WIN GAME");
            }
        }
        else
        {
            if (targetObjectCount == 0)
            {
                sounds[1].Play();
                panels[2].SetActive(true);
                WinLoseParticalEffect();
                Debug.Log("WIN GAME");
            }
        }
    }
    public void FallTargetObject()
    {
        targetObjectCount--;
        if (availableBallCount == 0 && targetObjectCount == 0)
        {
            sounds[1].Play();
            panels[2].SetActive(true);
            WinLoseParticalEffect();
            Debug.Log("WIN GAME");
        }
        else if (availableBallCount == 0 && targetObjectCount > 0)
        {
            sounds[0].Play();
            panels[1].SetActive(true);
            Debug.Log("DEFEAT GAME");
        }
    }
    private void WinLoseParticalEffect()
    {
        winParticalSystem.gameObject.SetActive(true);
        winParticalSystem.Play();
    }
    private void ParticalController(Transform hit)
    {
        particalSystem[_particalCount].transform.position = hit.transform.position;
        particalSystem[_particalCount].gameObject.SetActive(true);
        particalSystem[_particalCount].Play();

        if (_particalCount == particalSystem.Length)
        {
            _particalCount = 0;
        }
        else
        {
            _particalCount++;
        }
    }
    public void OnClickButtonController(string button)
    {
        switch (button)
        {
            case "Pause":
                panels[0].SetActive(true);
                break;
            case "Resume":
                panels[0].SetActive(false);
                break;
            case "Menu":
                SceneManager.LoadScene(0);
                break;
            case "Restart":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            case "Next":
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
        }
    }
}