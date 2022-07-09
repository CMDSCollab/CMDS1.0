using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionConvert
{
    public static Vector2 WorldPointToScreenPoint(Vector3 worldPoint)
    {
        /// <summary>
        /// ��������ת��Ϊ��Ļ����
        /// </summary>
        /// <param name="worldPoint">��Ļ����</param>
        /// <returns></returns>
        // Camera.main ���������
        Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);
        return screenPoint;
    }

    public static Vector3 ScreenPointToWorldPoint(Vector2 screenPoint, float planeZ)
    {
        /// <summary>
        /// ��Ļ����ת��Ϊ��������
        /// </summary>
        /// <param name="screenPoint">��Ļ����</param>
        /// <param name="planeZ">��������� Z ƽ��ľ���</param>
        /// <returns></returns>
        // Camera.main ���������
        Vector3 position = new Vector3(screenPoint.x, screenPoint.y, planeZ);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(position);
        return worldPoint;
    }

    public static Vector2 UIPointToScreenPoint(Vector3 worldPoint)
    {
        // RectTransformUtility.WorldToScreenPoint
        // RectTransformUtility.ScreenPointToWorldPointInRectangle
        // RectTransformUtility.ScreenPointToLocalPointInRectangle
        // ������������ת���ķ���ʹ�� Camera �ĵط�
        // �� Canvas renderMode Ϊ RenderMode.ScreenSpaceCamera��RenderMode.WorldSpace ʱ ���ݲ��� canvas.worldCamera
        // �� Canvas renderMode Ϊ RenderMode.ScreenSpaceOverlay ʱ ���ݲ��� null

        // UI ����ת��Ϊ��Ļ����
        // RectTransform��target
        // worldPoint = target.position;
        Camera uiCamera = UIManager.GetInstance().UICamera;

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(uiCamera, worldPoint);
        return screenPoint;
    }

    public static Vector3 ScreenPointToUIPoint(RectTransform rt, Vector2 screenPoint)
    {
        // ��Ļ����ת��Ϊ UGUI ����
        Vector3 globalMousePos;
        //UI��Ļ����ת��Ϊ��������
        Camera uiCamera = UIManager.GetInstance().UICamera;

        // �� Canvas renderMode Ϊ RenderMode.ScreenSpaceCamera��RenderMode.WorldSpace ʱ uiCamera ����Ϊ��
        // �� Canvas renderMode Ϊ RenderMode.ScreenSpaceOverlay ʱ uiCamera ����Ϊ��
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, screenPoint, uiCamera, out globalMousePos);
        // ת����� globalMousePos ʹ�����淽����ֵ
        // target Ϊ��Ҫʹ�õ� UI RectTransform
        // rt ������ target.GetComponent<RectTransform>(), Ҳ������ target.parent.GetComponent<RectTransform>()
        // target.transform.position = globalMousePos;
        return globalMousePos;
    }

    public static Vector2 ScreenPointToUILocalPoint(RectTransform parentRT, Vector2 screenPoint)
    {
        // ��Ļ����ת��Ϊ UGUI RectTransform �� anchoredPosition
        Vector2 localPos;
        Camera uiCamera = UIManager.GetInstance().UICamera;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRT, screenPoint, uiCamera, out localPos);
        // ת����� localPos ʹ�����淽����ֵ
        // target Ϊ��Ҫʹ�õ� UI RectTransform
        // parentRT �� target.parent.GetComponent<RectTransform>()
        // ���ֵ target.anchoredPosition = localPos;
        return localPos;
    }
}
