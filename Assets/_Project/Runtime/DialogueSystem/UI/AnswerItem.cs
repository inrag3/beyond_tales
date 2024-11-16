using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnswerItem : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler, IPointerClickHandler
{
    [Tooltip("Ссылка на текстовое поле")]
    [SerializeField]
    private Text text;
    [Tooltip("Ссылка на картинку, подсвечиваемую при выборе")]
    [SerializeField]
    private Image HighlightImage;
    /// <summary>
    /// Ссылка на скрипт панели ответов
    /// </summary>
    private AnswersPanel panel;
    /// <summary>
    /// Порядковый номер ответа в списке
    /// </summary>
    private int number;
    // Start is called before the first frame update
    void Start()
    {
        HighlightImage.gameObject.SetActive(false);
    }
    /// <summary>
    /// Панель вызывает эту функцию, чтобы передать вней порядковый номер и ссылку на себя
    /// </summary>
    public void InitializeData(AnswersPanel panel, int num)
    {
        this.panel = panel;
        number = num;
    }

    public void SetAnswer(Answer answer)
    {
        text.text = answer.text;
    }
    /// <summary>
    /// Вызывается панелью в момент, когда игрок наводит на этот вариант ответа
    /// </summary>
    /// <param name="isSelected"></param>
    public void SetSelected(bool isSelected)
    {
        HighlightImage.gameObject.SetActive(isSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        panel.OnHowerItem(number);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //HighlightImage.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            panel.OnSelectItem(number);
        }
    }
}
