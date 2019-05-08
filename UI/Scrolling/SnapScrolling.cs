using System;
using UnityEngine;
using UnityEngine.UI;

public enum Axis
{
    HORIZONTAL,
    VERTICAL
}

public enum View
{
    GRID,
    STRING
}

public class SnapScrolling : MonoBehaviour
{
    [Header("View")]
    public Axis orientation;
    public View view;

    [Header("Effects")]
    public bool Scalable;
    [Header("Controllers")]
    //[Range(1, 50)]
    //public int panCount;
    [Range(0, 500)]
    public int panOffset;
    [Range(0f, 20f)]
    public float snapSpeed;
    [Range(0f, 10f)]
    public float scaleOffset;
    [Range(1f, 20f)]
    public float scaleSpeed;
    [Header("Common options")]
    public GameObject panPrefab;
    public ScrollRect scrollRect;
    public IContent DataList; // LevelsSO
    [Header("Grid View")]
    public int cols;
    private float currentY;
    //public int rows;
    [Header("String View")]
    public IntVariable selectedPanID;



    private int panCount;
    private GameObject[] instPans;
    private Vector2[] pansPos;
    private Vector2[] pansScale;

    private RectTransform contentRect;
    private Vector2 contentVector;

    //private int selectedPanID;
    private bool isScrolling;

    private void Start()
    {
        panCount = DataList.GetListCount();
        contentRect = GetComponent<RectTransform>();
        if (view == View.GRID)
            contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, CalculateSide(panOffset, Math.Round((double)(DataList.GetListCount() / cols), MidpointRounding.AwayFromZero), panPrefab.GetComponent<RectTransform>().sizeDelta.y) + 200); 
        instPans = new GameObject[panCount];
        pansPos = new Vector2[panCount];
        pansScale = new Vector2[panCount];
        for (int i = 0; i < panCount; i++)
        {
            instPans[i] = Instantiate(panPrefab, transform, false);
            instPans[i].GetComponent<PrefabHandler>().InjectData(DataList.GetItem(i));
            if (i == 0)
            {
                currentY = instPans[0].transform.localPosition.y;
                continue;
            }

            // Move pannels right with some offset
            if (view == View.STRING)
            {
                instPans[i].transform.localPosition = new Vector2(instPans[i - 1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset,
                    instPans[i].transform.localPosition.y);
                pansPos[i] = -instPans[i].transform.localPosition;
            }
            else if (view == View.GRID)
            {
                if (i % cols == 0)
                    currentY = currentY - panOffset - panPrefab.GetComponent<RectTransform>().sizeDelta.y;
                if(i > cols - 1)
                    instPans[i].transform.localPosition = new Vector2(instPans[i % cols].transform.localPosition.x, currentY);
                else
                    instPans[i].transform.localPosition = new Vector2(instPans[i -1].transform.localPosition.x + panPrefab.GetComponent<RectTransform>().sizeDelta.x + panOffset, currentY);

            }

        }
    }

    private void FixedUpdate()
    {
        if (view == View.STRING)
        {
            StringViewUpdate();
        }
        else if (view == View.GRID)
        {
            GridViewUpdate();
        }
    }

    public void Scrolling(bool scroll)
    {
        isScrolling = scroll;
        if (scroll) scrollRect.inertia = true;
    }

    private void GridViewUpdate()
    {

    }

    private void StringViewUpdate()
    {
        if (contentRect.anchoredPosition.x >= pansPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= pansPos[pansPos.Length - 1].x && !isScrolling)
            scrollRect.inertia = false;
        float nearestPos = float.MaxValue;
        for (int i = 0; i < panCount; i++)
        {
            float distance = Mathf.Abs(contentRect.anchoredPosition.x - pansPos[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedPanID.value = i;
            }
            if (Scalable)
            {
                float scale = Mathf.Clamp(1 / (distance / panOffset) * scaleOffset, 0.5f, 1f);
                pansScale[i].x = Mathf.SmoothStep(instPans[i].transform.localScale.x, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                pansScale[i].y = Mathf.SmoothStep(instPans[i].transform.localScale.y, scale + 0.3f, scaleSpeed * Time.fixedDeltaTime);
                instPans[i].transform.localScale = pansScale[i];
            }
        }
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
        if (scrollVelocity < 400 && !isScrolling) scrollRect.inertia = false;
        if (isScrolling || scrollVelocity > 400) return;
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, pansPos[selectedPanID.value].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
    }

    float CalculateSide(float offset, double amount, float panSide)
    {
        return (float)((amount - 1) * offset + amount * panSide);
    }
}