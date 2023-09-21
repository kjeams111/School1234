using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Slot[] slots;
    

    private Vector3 target;
    private ItemInfo carryingItem; //�̵� ��Ű�� �ִ� ������ ����

    private Dictionary<int, Slot> slotDictionary;  //���� ������ �����ϴ� �ڷ� ����

    void Start()
    {
        slotDictionary = new Dictionary<int, Slot>(); //���� ��ųʸ� �ʱ�ȭ

        for(int i = 0; i < slots.Length; i++) //�� ������ ID�� �����ϰ� ��ųʸ��� �߰� 
        {
            slots[i].id  = i;
            slotDictionary.Add(i, slots[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) //���콺 ��ư�� ������ ��
        {
            SendRayCast();
        } 

        if(Input.GetMouseButton(0) && carryingItem) //���콺 ��ư ���� ���¿��� ������ ���� �� �̵� ó��
        {
            OnItemSelected();
        }

        if(Input.GetMouseButtonUp(0)) //���콺 ��ư���� �̺�Ʈ ó��
        {
            SendRayCast();
        }

        if(Input.GetKeyDown(KeyCode.Space)) //�����̽� Ű�� �������� ���� ������ ��ġ
        {
            PlaceRandomItem();
        }
    }

    void SendRayCast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //ȭ���� ���콺 ��ǥ�� ���ؼ� ���� ���� �����ɽ���
        RaycastHit hit;

        if(Physics.Raycast(ray , out hit)) //hit �� ���� ���� ���
        {
            var slot = hit.transform.GetComponent<Slot>(); //hit �� slot Component �� �����´�
            if(slot.state == Slot.SLOTSTATE.FULL && carryingItem == null)  //������ ���Կ��� �������� ��� �̵��ϴ°��
            {
                string itemPath = "Prefabs/Item_Grabbed" + slot.itemObject.id.ToString("000"); //���� ������ ����
                var itemGO = (GameObject)Instantiate(Resources.Load(itemPath)); //�ش� ��θ� ���ؼ� ����
                itemGO.transform.position = slot.transform.position; //Slot ��ġ�� �����ϰ� ����
                itemGO.transform.localScale = Vector3.one * 2;// ũ��� 2���
                carryingItem = itemGO.GetComponent<ItemInfo>(); //���� �������� carryingitem�� �Է�
                carryingItem.InitDummy(slot.id, slot.itemObject.id); //������ ���� ����

                slot.itemGrabbed();
            }

            else if(slot.state == Slot.SLOTSTATE.ENPTY && carryingItem != null)// �� ���Կ� �������� ��ġ
            {
                slot.Createitem(carryingItem.itemld);
                Destroy(carryingItem.gameObject);
            }
            else if (slot.state == Slot.SLOTSTATE.FULL && carryingItem != null) //�����۳��� ���� �������� ������
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

    //�������� �����ϰ� ���콺 ��ġ�� �̵�

    void OnItemSelected()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���� ��ǥ���� ���콺 �����ǰ��� �����ͼ� target �Է�
        target.z = 0;

        var delta = 10 * Time.deltaTime;  //�̵� �ӵ� ����

        delta += Vector3.Distance(transform.position, target);
        carryingItem.transform.position = Vector3.MoveTowards(carryingItem.transform.position, target, delta);
    }

    //�����ۿ� ���԰� ���� �� �� �Լ�
    void OnItemMergedWithTarget(int targetSlotId) //�������� ���԰� ���� �� �� �Լ�
    {
        var slot = GetSlotbByid(targetSlotId); //���� ���Կ� �ִ� ������Ʈ�� �����ͼ� �ı�
        Destroy(slot.itemObject.gameObject);
        slot.Createitem(carryingItem.itemld + 1); //���� �Ǿ����Ƿ� ����������Ʈ�� ����
        Destroy(carryingItem.gameObject);  //����ִ� ���� ������Ʈ�� �ı�
    }

    void OnItemCarryFail() //������ ��ġ ���н� ����
    {
        var Slot = GetSlotbByid(carryingItem.slotld); //�ش罽�Կ� �ٽ� ����
        Slot.Createitem(carryingItem.itemld);
        Destroy(carryingItem.gameObject);
        
    }

    void PlaceRandomItem() //  ������ ���Կ� ������ ��ġ
    {
        if(AllSlotsOccupied())
        {
            Debug.Log("������ �� ������ => ���� �Ұ�");
            return;
        }


        var rand = UnityEngine.Random.Range(0, slots.Length);
        var slot = GetSlotbByid(rand); //rand ���� index ���� ���ؼ� slot ��ü�� �����´�


        while(slot.state == Slot.SLOTSTATE.FULL)
        {
            rand = UnityEngine.Random.Range(0, slots.Length);
            slot = GetSlotbByid(rand); 
        }

        slot.Createitem(0);  //�� ������ �߰��ϸ� 0��° �������� ����

    }


    bool AllSlotsOccupied()
    {
        foreach(var slot in slots) //���� �迭�� �ϳ��� Ȯ���ϸ鼭 foreach �� �ݺ�
        {
            if(slot.state == Slot.SLOTSTATE.ENPTY) //���� �迭�� �� �ڸ��� ������
            {
                return false;  //�߰��� false�� ����
            }
        }
        return true;  //�� �������Ƿ� true�� ����
    }
    //���� id������ �˻�
    Slot GetSlotbByid(int id)
    {
        return slotDictionary[id]; //��ųʸ��� ����ִ� Slot class ��ȯ
    }
}
