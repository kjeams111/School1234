using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
  public enum SLOTSTATE //슬롯 입력값
    {
        ENPTY,
        FULL
    }

    public int id;
    public item itemObject;
    public SLOTSTATE state = SLOTSTATE.ENPTY; //상태값 선언한것 정의후 EMPTY 입력


    private void ChangeStateTo(SLOTSTATE targetState) //해당 슬롯의 상태값을 변환시켜주는함수
    {
        state = targetState;
    }

    public void itemGrabbed()
    {
        Destroy(itemObject.gameObject); //슬롯위의 아이템을 삭제
        ChangeStateTo(SLOTSTATE.ENPTY); //슬롯은 빈 상태
    }

     public void Createitem(int id)
     {
        string itemPath = "Prefabs/Item_" + id.ToString("000"); //생성할 아이템 경로 생성
        var itemGo = (GameObject)Instantiate(Resources.Load(itemPath));//아이템 경로에 있는 프리팹을 생성 
        itemGo.transform.SetParent(this.transform); //Slot 오브젝트 하위로 설정
        itemGo.transform.localPosition = Vector3.zero;      //로컬위치는 Vector3 0,0,0
        itemGo.transform.localScale = Vector3.one; //로컬 Scale은 Vector3 1,1,1

        //생성 item 컴포넌트 데이터 입력
        itemObject = itemGo.GetComponent<item>();
        itemObject = Init(id,this);

        ChangeStateTo(SLOTSTATE.FULL);



     }

    private item Init(int id, Slot slot)
    {
        throw new NotImplementedException();
    }
}
