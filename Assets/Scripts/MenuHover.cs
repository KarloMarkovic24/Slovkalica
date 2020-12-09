using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button button;

    private float startTime;
    private bool startTimeInit;
    private int secondsToLoad;
    private Image circle;

    private void Awake()
    {
        secondsToLoad = PlayerPrefs.GetInt("HoverTime", 2);
        circle = gameObject.GetComponent<Image>();
    }
    private void Update()
    {
        if (startTimeInit)
        {
            circle.fillAmount += Time.deltaTime / secondsToLoad;
            if ((Time.time - startTime) > secondsToLoad)
            {
                button.onClick.Invoke();
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        startTime = Time.time;
        startTimeInit = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        startTime = 0;
        startTimeInit = false;
        circle.fillAmount = 0;
    }

    public bool Clicked { get; set; }
}
