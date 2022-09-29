using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DropZone : MonoBehaviour, IDropHandler
{

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop to"+gameObject.name);

        Draggable d = eventData.pointerDrag.GetComponent<Draggable>();
        GameObject card = d.objectCard;
        CardDisplay cardType= d.GetComponent<CardDisplay>();

        if (d != null && Player.turno==true)
        {
            if (PlayerStats.mana >= 0)
            {

                if (cardType.nameText.text == "CASCO REFORJADO")
                {
                    if (PlayerStats.mana >= 1)
                    {
                        d.parentToReturnTo = this.transform;
                        PlayerStats.armor += 2;
                        PlayerStats.mana -= 1;
                        Destroy(card);
                    }
                }
                else if (cardType.nameText.text == "CONTRAATAQUE")
                {
                    if (PlayerStats.mana >= 1)
                    {
                        d.parentToReturnTo = this.transform;
                        PlayerStats.counterDmg += 2;
                        PlayerStats.durCounter += 1;
                        PlayerStats.mana -= 1;
                        Destroy(card);
                    }
                }
                else if (cardType.nameText.text == "ESTOCADA")
                {
                    if (PlayerStats.mana >= 2)
                    {
                        d.parentToReturnTo = this.transform;
                        PlayerStats.dmgCaused += 2;
                        EnemyStats.hemo += 1;
                        EnemyStats.durHemo += 2;
                        PlayerStats.mana -= 2;
                        Player.action = true;
                        Destroy(card);
                    }
                }
                else if (cardType.nameText.text == "GOLPE RAPIDO")
                {
                    if (PlayerStats.mana >= 1)
                    {
                        d.parentToReturnTo = this.transform;
                        PlayerStats.dmgCaused += 2;
                        PlayerStats.mana -= 1;
                        Player.action = true;
                        Destroy(card);
                    }
                }
                else if (cardType.nameText.text == "IRA DEL GUERRERO")
                {
                    if (PlayerStats.mana >= 1)
                    {
                        d.parentToReturnTo = this.transform;
                        PlayerStats.strength += 1;
                        PlayerStats.durStrength += 2;
                        PlayerStats.mana -= 1;
                        Destroy(card);
                    }
                }
                else if (cardType.nameText.text == "MANO DIVINA")
                {
                    if (PlayerStats.mana >= 2)
                    {
                        d.parentToReturnTo = this.transform;
                        PlayerStats.health += 3;
                        if (PlayerStats.health > 20)
                        {
                            PlayerStats.health = 20;
                        }
                        PlayerStats.mana -= 2;
                        Destroy(card);
                    }
                }
                
            }
        }
    }
}
