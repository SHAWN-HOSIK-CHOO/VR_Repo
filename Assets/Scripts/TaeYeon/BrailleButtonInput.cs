using UnityEngine;
using TMPro;
using System;

public class BrailleButtonInput : MonoBehaviour
{
    public TextMeshProUGUI inputDisplay;    // 현재 입력 상태를 표시할 텍스트
    public TextMeshProUGUI searchDisplay;   // 최종 검색어를 표시할 텍스트
    public TextMeshProUGUI resultsDisplay;  // 결과 시간 표시 UI
    public string correctBusNumber = "123"; // 올바른 버스 번호

    public event Action OnValidBusNumberEntered; // 올바른 버스 번호 입력 시 발생하는 이벤트

    private string currentInput = ""; // 현재 입력된 점자
    private string searchQuery = "";  // 최종 검색어

    // 점자 버튼 클릭 시 호출
    public void OnBrailleButtonClick(string dotNumber)
    {
        currentInput += dotNumber; // 점 번호 추가
        UpdateInputDisplay();
    }

    // 단어 완료 버튼 (Space 역할)
    public void OnCompleteWord()
    {
        string translatedText = BrailleToText(currentInput); // 점자 -> 텍스트 변환
        searchQuery += translatedText;
        currentInput = ""; // 현재 입력 초기화
        UpdateInputDisplay();
        UpdateSearchDisplay();
    }

    // 검색 실행 버튼
    public void OnSearch()
    {
        if (searchQuery == correctBusNumber)
        {
            OnValidBusNumberEntered?.Invoke(); // 올바른 번호 입력 시 이벤트 호출
            ShowBusTimeMessage(); // 결과 메시지 표시
        }
        else
        {
            resultsDisplay.text = "Wrong bus number. Try again."; // 잘못된 번호 메시지
        }

        currentInput = "";
        searchQuery = "";
        UpdateInputDisplay();
        UpdateSearchDisplay();
    }

    private void UpdateInputDisplay()
    {
        inputDisplay.text = "Input: " + currentInput; // 현재 입력 표시
    }

    private void UpdateSearchDisplay()
    {
        searchDisplay.text = "Query: " + searchQuery; // 최종 검색어 표시
    }

    private string BrailleToText(string braille)
    {
        switch (braille)
        {
            case "1": return "1";
            case "12": return "2";
            case "14": return "3";
            case "145": return "4";
            case "15": return "5";
            case "124": return "6";
            case "1245": return "7";
            case "125": return "8";
            case "24": return "9";
            case "245": return "0";
            default:
                return "?"; // 알 수 없는 패턴
        }
    }

    private void ShowBusTimeMessage()
    {
        // 고정된 메시지 출력
        resultsDisplay.text = "101 bus arrives in 10 minutes";
    }
}
