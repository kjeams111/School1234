using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletone : MonoBehaviour
{
   public static Singletone instance { get; private set; } // �ν��Ͻ��� ������ ����


    private void Awake()
    {
        if(instance == null)   //instance�� Null�϶� 
        {
            instance = this;
            DontDestroyOnLoad(gameObject); //���� ������Ʈ�� Scene�� ��ȯ�ǰ� �ı����� ���� 
        }
        else
        {
            Destroy(gameObject);  //1���� ������Ű�� ���� ������ ��ü�� �ı��Ѵ�
        }
    }


    public int playerScore = 0;  //������ �÷��̾� ���ھ�
    

    public void inscreaseSocre(int amount) //�Լ��� ���ؼ� ���ھ ������Ų��
    {
        playerScore += amount;
    }
}
