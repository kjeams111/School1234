using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
  public enum SLOTSTATE //���� �Է°�
    {
        ENPTY,
        FULL
    }

    public int id;
    public item itemObject;
    public SLOTSTATE state = SLOTSTATE.ENPTY; //���°� �����Ѱ� ������ EMPTY �Է�


    private void ChangeStateTo(SLOTSTATE targetState) //�ش� ������ ���°��� ��ȯ�����ִ��Լ�
    {
        state = targetState;
    }

    public void itemGrabbed()
    {
        Destroy(itemObject.gameObject); //�������� �������� ����
        ChangeStateTo(SLOTSTATE.ENPTY); //������ �� ����
    }

     public void Createitem(int id)
     {
        string itemPath = "Prefabs/Item_" + id.ToString("000"); //������ ������ ��� ����
        var itemGo = (GameObject)Instantiate(Resources.Load(itemPath));//������ ��ο� �ִ� �������� ���� 
        itemGo.transform.SetParent(this.transform); //Slot ������Ʈ ������ ����
        itemGo.transform.localPosition = Vector3.zero;      //������ġ�� Vector3 0,0,0
        itemGo.transform.localScale = Vector3.one; //���� Scale�� Vector3 1,1,1

        //���� item ������Ʈ ������ �Է�
        itemObject = itemGo.GetComponent<item>();
        itemObject = Init(id,this);

        ChangeStateTo(SLOTSTATE.FULL);



     }

    private item Init(int id, Slot slot)
    {
        throw new NotImplementedException();
    }
}
