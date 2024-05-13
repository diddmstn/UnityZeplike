using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SocialPlatforms;

public class GameManager : MonoBehaviour
{
    Coroutine runningCorutine;
    [Header("CharacterSelect")]
    public CharactorAnimationController charactorAnimation;
    public GameObject nameInputPanel;
    public GameObject characterSelect;
    public Image characterImage;
    [Header("NameChange")]
    public CharacterStatHandler characterStatHandler;
    public TMP_Text nameText;
    public TMP_Text warningText;
    [Header("Time")]
    public TMP_Text timeText;
    [Header("MiniUI")]
    public GameObject miniCharacterSelect;
    public TMP_Text miniNameText;

    //public SpriteRenderer playerSprite;

    private void Start()
    {
        Time.timeScale = 0;
    }
    private void Update()
    {
        timeText.text = DateTime.Now.ToString("HH:mm");
    }
    void CheckNameCorrect(TMP_Text inputText,GameObject obj)
    {
        if (inputText.text.Length <= 2)
        {
            if (runningCorutine != null) //마지막에 입력된 기준으로 1초를 세기위해 기존에 작동하던 코루틴 멈춤
                StopCoroutine(runningCorutine);

            warningText.enabled = true;
            warningText.text = "이름이 너무 짧습니다.";
            runningCorutine = StartCoroutine(TextClose(warningText)); //1초 뒤에 꺼짐
        }
        else
        {
            characterStatHandler.characterInfo.name = inputText.text;
            obj.SetActive(false);
            characterStatHandler.UpdateStatus();

            Time.timeScale = 1;
        }
    }    
    public void ChangeName()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject.transform.parent.gameObject;
        CheckNameCorrect(nameInputPanel.activeSelf? nameText: miniNameText, obj);
    }
    IEnumerator TextClose(TMP_Text text)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        warningText.enabled = false;
    }
   public void ButtonActive()
    {
        Time.timeScale = 0;
        GameObject obj = EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject;
        bool active = obj.activeSelf;
        obj.SetActive(!active);

        if(!active==false)
        {
            obj.SetActive(false);
            Time.timeScale = 1;
        }
    }
   
    public void SelectCharacter()
    {
        GameObject obj = EventSystem.current.currentSelectedGameObject;
        Image image = obj.transform.GetChild(0).GetComponent<Image>();//누른 버튼의 정보 저장
        int num = obj.GetComponent<CharacterNum>().characterId;//누른 버튼의 정보 저장
        ChangeCharacter(image, num);
        obj.transform.parent.gameObject.SetActive(false);

        Time.timeScale  = nameInputPanel.activeSelf ?  0: 1;
        
    }
   
    void ChangeCharacter(Image image, int num)
    {
        characterImage.sprite = image.sprite; //선택한 이미지 변경
        charactorAnimation.ChangeAnimator(num); //애니메이터 변경

    }
}
