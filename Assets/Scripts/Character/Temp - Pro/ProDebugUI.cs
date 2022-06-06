using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProDebugUI : MonoBehaviour
{
    [SerializeField] private Transform textsParent;//����errorText�ĸ��ڵ㣬Ҫ����VerticalLayoutGroup���
    [SerializeField] private Text errorTextPrefab;
    [SerializeField] private Text warningNumberText;// X Warning(s)
    [SerializeField] private Text errorNumberText;// Y Error(s)
    [SerializeField] private Text codeRedundancyText;// Z Redundant Code(s)
    private Programmer programmer;
    private List<Text> errorTexts;

    public void SetUp(Programmer programmer)
    {
        this.programmer = programmer;
        errorTexts = new List<Text>(textsParent.GetComponentsInChildren<Text>());

        // ���Debug UI��û���㹻��errorTexts�������ɵ���Ӧ������
        if (errorTexts.Count < Programmer.MAX_ERROR_COUNT)
        {
            int difference = Programmer.MAX_ERROR_COUNT - errorTexts.Count;
            for (int i = 0; i < difference; i++)
            {
                Text newErrorText = Instantiate(errorTextPrefab, textsParent, false);
                errorTexts.Add(newErrorText);
            }
        }

        GetComponent<RectTransform>().anchoredPosition = new Vector3(-400f, 200f);
    }

    private void FixedUpdate()
    {
        UpdateNumberTexts();
        VisualizeErrors();
    }


    private void UpdateNumberTexts()
    {
        warningNumberText.text = 0 + " Warning(s)";
        errorNumberText.text = programmer.currentErrors.Count + " Error(s)";
        codeRedundancyText.text = programmer.codeRedundancy + " Redundant Code(s)";
    }

    private void VisualizeErrors()
    {
        // ������Text�������
        foreach (var errorText in errorTexts)
        {
            errorText.text = "";
        }

        // ����ҵ�ǰ��Error�������θ�ֵ��Text�ϣ�ʵ����ʾ
        int index = 0;
        foreach (var error in programmer.currentErrors)
        {
            if (errorTexts[index] != null)
            {
                errorTexts[index].text = "Error: " + error.errorName;
            }
            else
            {
                Debug.LogError("Wrong capacity. Are there enough text objects on the debug UI ?");
                break;
            }
            index++;
        }
    }

}
