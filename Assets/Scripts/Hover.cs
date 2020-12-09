using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image redCircle, greenCircle;
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
        if (startTimeInit && Clickable)
        {
            circle.fillAmount += Time.deltaTime / secondsToLoad;
            if ((Time.time - startTime) > secondsToLoad)
            {
                Clicked = true;
                GameManager.click = true;
                NumberManager.click = true;
                EquationManager.click = true;
            }
        }

        if (Green)        
            greenCircle.fillAmount += Time.deltaTime;
        if (Red)
            redCircle.fillAmount += Time.deltaTime;
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

    public bool Clickable { get; set; } = true;
    public bool Clicked { get; set; }
    public bool Green { get; set; }
    public bool Red { get; set; }
}
