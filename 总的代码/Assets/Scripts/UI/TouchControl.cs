﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchControl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 m_offset;
    private RectTransform m_rt;


    void Start()
    {
        m_rt = GetComponent<RectTransform>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        int count = transform.parent.childCount - 1;//Panel移位
        transform.SetSiblingIndex(count);//Panel移位
        //开始拖拽
        // 存储点击时的鼠标坐标
        Vector3 tWorldPos;
        //屏幕空间点转换为世界空间点，该点在矩形变换的平面上。
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out tWorldPos);
        //图片中心与点击位置偏移量
        m_offset = transform.position - tWorldPos;
    }


    public void OnDrag(PointerEventData eventData)
    {
        //拖拽ing
        SetDraggedPosition(eventData);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //拖拽结束
    }


    private void SetDraggedPosition(PointerEventData eventData)
    {
        Vector3 globalMousePos;
        //UI坐标转换为世界坐标(1.目标矩阵 2.要转换的位置 3.显示这个UI的相机 4.输出V3）  
        RectTransformUtility.ScreenPointToWorldPointInRectangle(m_rt, eventData.position, eventData.pressEventCamera, out globalMousePos);
        //设置位置及偏移量
        m_rt.position = globalMousePos + m_offset;
    }

}

