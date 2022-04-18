﻿using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace JLPlugin.Data
{
    [System.Serializable]
    public class placeCards
    {
        public string runOnCondition;
        public slotData slot;
        public card card;

        public static IEnumerator PlaceCards(SigilData abilitydata)
        {
            yield return new WaitForSeconds(0.3f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
            yield return new WaitForSeconds(0.3f);

            foreach (placeCards placecardinfo in abilitydata.placeCards)
            {
                if (SigilData.ConvertArgument(placecardinfo.runOnCondition, abilitydata) == "false")
                {
                    continue;
                }

                Singleton<ViewManager>.Instance.SwitchToView(View.Default, false, false);
                CardSlot slot = slotData.GetSlot(placecardinfo.slot, abilitydata);
                if (slot != null)
                {
                    if (slot.Card != null)
                    {
                        slot.Card.ExitBoard(0, new Vector3(0, 0, 0));
                    }
                    if (slot.Card == null)
                    {
                        CardInfo cardinfo = card.getCard(placecardinfo.card, abilitydata);
                        if (cardinfo != null)
                        {
                            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardinfo, slot);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(0.3f);
            yield break;
        }
    }
}