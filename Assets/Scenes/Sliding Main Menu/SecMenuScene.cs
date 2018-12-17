using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecMenuScene : MonoBehaviour
{
    [Range(1, 50)]
    [Header("Controllers")]
    //no of swipe screen
    public int panCount;
    [Range(0, 500)]
    public int panOffset;
    // how fast it will snap
    [Range(0, 20)]
    public int snapSpeed;
    [Range(0, 6)]
    public int scaleOffset;
    [Range(0, 20)]
    public int scaleSpeed;
    
    [Header("Other object")]
    public GameObject panPrefab;
//to add the image in start
    [SerializeField]
    private Sprite[] imageForContent;
    public ScrollRect scrollRect;


    private GameObject[] instPans;
    private Vector2[] panPos, panScale;


    private RectTransform contentRect;
    private Vector2 contentVector;
    //use to scene change when play button pressed
    public int selectedPanID;
    public bool isScroling;
    public Text forMobileText;
    public static bool forMobile;
    public GameObject credit;
    public AudioSource music;
    bool isPlaying = true;
    // Use this for initialization
   void OnAwake()
    {
        if (isPlaying)
        {
            music.Play();
            isPlaying = false;
        }
    }
    void Start()
    {

        forMobileText.text = "Desktop";
        contentRect = GetComponent<RectTransform>();
        instPans = new GameObject[panCount];
        panPos = new Vector2[panCount];
        panScale = new Vector2[panCount];
        for (int i = 0; i < panCount; i++)
        {

            instPans[i] = Instantiate(panPrefab, transform, false);
//add the sprite to the content
            instPans[i].GetComponent<Image>().sprite = imageForContent[i];

            if (i == 0) continue;
            //if it is starting just go back to loop
            //for i=1 localPos = 
            //x=previousLocalPos.x + Size of currentRect+ panOffset
            //y = current.LocalPos.Y
            instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
                instPans[i].transform.localPosition.y);
            panPos[i] = -instPans[i].transform.localPosition;

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {   // if it is last then no inertia
        if (contentRect.anchoredPosition.x >= panPos[0].x && !isScroling || contentRect.anchoredPosition.x <= panPos[panPos.Length - 1].x && !isScroling)
        {
            scrollRect.inertia = false;
        }
        float nearestpos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - panPos[i].x);
            if (distance < nearestpos)
            {
                nearestpos = distance;
                selectedPanID = i;
            }
            float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1);
            panScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x + 0.3f, scale, scaleSpeed * Time.fixedDeltaTime);
            panScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y + 0.3f, scale, scaleSpeed * Time.fixedDeltaTime);
            instPans[i].transform.localScale = panScale[i];
        }
        float scrolVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrolVelocity < 400 && !isScroling) scrollRect.inertia = false;

        if (isScroling || scrolVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, panPos[selectedPanID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }
    public void Scrolling(bool Scroll)
    {
        isScroling = Scroll;
        if (Scroll) scrollRect.inertia = true;
    }
    public void onToggleMobile(Toggle t)
    {
        if (t.isOn)
        {
            forMobileText.text = "Phone";
            forMobile = true;
        }
        else
        {
            forMobileText.text = "Desktop";
            forMobile = false;
        }
    }
    public void onClickCredit()
    {
       GameObject obj =  Instantiate(credit, transform.position, Quaternion.identity)as GameObject;
        Destroy(obj, 1);
    }
}