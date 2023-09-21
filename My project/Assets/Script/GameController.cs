using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Slot[] slots;
    

    private Vector3 target;
    private ItemInfo carryingItem; //이동 시키고 있는 아이템 정보

    private Dictionary<int, Slot> slotDictionary;  //슬롯 정보값 관리하는 자료 구조

    void Start()
    {
        slotDictionary = new Dictionary<int, Slot>(); //슬롯 딕셔너리 초기화

        for(int i = 0; i < slots.Length; i++) //각 슬롯의 ID를 설정하고 딕셔너리에 추가 
        {
            slots[i].id  = i;
            slotDictionary.Add(i, slots[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //마우스 버튼을 눌렀을 때
        {
            SendRayCast();
        } 

        if(Input.GetMouseButton(0) && carryingItem) //마우스 버튼 누름 상태에세 아이템 선택 및 이동 처리
        {
            OnItemSelected();
        }

        if(Input.GetMouseButtonUp(0)) //마우스 버튼떼기 이벤트 처리
        {
            SendRayCast();
        }

        if(Input.GetKeyDown(KeyCode.Space)) //스페이스 키를 눌렀을때 랜덤 아이템 배치
        {
            PlaceRandomItem();
        }
    }

    void SendRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //화면의 마우스 좌표를 통해서 월드 상의 레이케스팅
        RaycastHit hit;

        if(Physics.Raycast(ray , out hit)) //hit 된 것이 있을 경우
        {
            var slot = hit.transform.GetComponent<Slot>(); //hit 된 slot Component 를 가져온다
            if(slot.state == Slot.SLOTSTATE.FULL && carryingItem == null)  //선택한 슬롯에서 아이템을 잡고 이동하는경우
            {
                string itemPath = "Prefabs/Item_Grabbed" + slot.itemObject.id.ToString("000"); //잡을 아이템 생성
                var itemGO = (GameObject)Instantiate(Resources.Load(itemPath)); //해당 경로를 통해서 생성
                itemGO.transform.position = slot.transform.position; //Slot 위치로 생성하게 설정
                itemGO.transform.localScale = Vector3.one * 2;// 크기는 2배로
                carryingItem = itemGO.GetComponent<ItemInfo>(); //잡은 아이템을 carryingitem에 입력
                carryingItem.InitDummy(slot.id, slot.itemObject.id); //정보값 까지 생성

                slot.itemGrabbed();
            }

            else if(slot.state == Slot.SLOTSTATE.ENPTY && carryingItem != null)// 빈 슬롯에 아이템을 배치
            {
                slot.Createitem(carryingItem.itemld);
                Destroy(carryingItem.gameObject);
            }
            else if (slot.state == Slot.SLOTSTATE.FULL && carryingItem != null) //아이템끼리 같은 슬롯위에 있을때
            {
                if(slot.itemObject.id == carryingItem.itemld)
                {
                    OnItemMergedWithTarget(slot.id);
                }
                else
                {
                    OnItemCarryFail();
                }
            }

        }
        else
        {
            if(!carryingItem)
            {
                return;
            }
            OnItemCarryFail();
        }

    }

    //아이템을 선택하고 마우스 위치로 이동

    void OnItemSelected()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition); //월드 좌표에서 마우스 포지션값을 가져와서 target 입력
        target.z = 0;

        var delta = 10 * Time.deltaTime;  //이동 속도 조절

        delta += Vector3.Distance(transform.position, target);
        carryingItem.transform.position = Vector3.MoveTowards(carryingItem.transform.position, target, delta);
    }

    //아이템여 슬롯과 병합 될 때 함수
    void OnItemMergedWithTarget(int targetSlotId) //아이템이 슬롯과 병합 될 때 함수
    {
        var slot = GetSlotbByid(targetSlotId); //기존 슬롯에 있는 오브젝트를 가져와서 파괴
        Destroy(slot.itemObject.gameObject);
        slot.Createitem(carryingItem.itemld + 1); //병합 되었으므로 다음오브젝트를 생성
        Destroy(carryingItem.gameObject);  //들고있던 더미 오브젝트를 파괴
    }

    void OnItemCarryFail() //아이템 배치 실패시 실행
    {
        var Slot = GetSlotbByid(carryingItem.slotld); //해당슬롯에 다시 생성
        Slot.Createitem(carryingItem.itemld);
        Destroy(carryingItem.gameObject);
        
    }

    void PlaceRandomItem() //  랜덤한 슬롯에 아이템 배치
    {
        if(AllSlotsOccupied())
        {
            Debug.Log("슬롯이 다 차있음 => 생성 불가");
            return;
        }


        var rand = UnityEngine.Random.Range(0, slots.Length);
        var slot = GetSlotbByid(rand); //rand 받은 index 값을 통해서 slot 객체를 가져온다


        while(slot.state == Slot.SLOTSTATE.FULL)
        {
            rand = UnityEngine.Random.Range(0, slots.Length);
            slot = GetSlotbByid(rand); 
        }

        slot.Createitem(0);  //빈 슬롯을 발견하면 0번째 아이템을 생성

    }


    bool AllSlotsOccupied()
    {
        foreach(var slot in slots) //슬롯 배열을 하나씩 확인하면서 foreach 문 반복
        {
            if(slot.state == Slot.SLOTSTATE.ENPTY) //슬롯 배열레 빈 자리가 있으면
            {
                return false;  //중간에 false를 리턴
            }
        }
        return true;  //다 차있으므로 true를 리턴
    }
    //슬롯 id슬롯을 검색
    Slot GetSlotbByid(int id)
    {
        return slotDictionary[id]; //딕셔너리에 담겨있는 Slot class 반환
    }
}
