using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : GenericSingletone<GameManager>
{
    public int playerScore = 0;  //������ �÷��̾� ���ھ�


    public void inscreaseSocre(int amount) //�Լ��� ���ؼ� ���ھ ������Ų��
    {
        playerScore += amount;
    }
}
