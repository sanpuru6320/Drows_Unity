using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OrderOfActionUI : MonoBehaviour
{
    public List<RowCharacterUI> rowCharacters;

    private void Update()
    {
        //Time.timeScale = 2f;//2倍速(デバック用)

    }

    public void IndexChange()
    {
        foreach (var image in rowCharacters)
        {


            if (image.objectIndex == 3)//行動中
            {

                if (image.characterImage.sprite != null)
                {
                    rowCharacters[2].characterImage.sprite = image.characterImage.sprite;
                }

                var turns = BattleManager.instance.activeBattlers.Where(x => !(x.currentHP == 0) && x.onAction == true)
                                                  .FirstOrDefault();

                image.characterImage.sprite = turns.aliveSprite;                             

            }
            else if (image.objectIndex == 2 || image.objectIndex == 1) //行動後
            {
                CurrentSpriteChange(image);
            }
            else if (image.objectIndex == 4)//1ターン行動前
            {
                var turns = BattleManager.instance.activeBattlers.Where(x => !(x.currentHP == 0) && x.onAction == true);
                var turnBefore1 = turns.Skip(1)
                                   .FirstOrDefault() ?? BattleManager.instance.activeBattlers.FirstOrDefault(x => !(x.currentHP == 0));
                image.characterImage.sprite = turnBefore1.aliveSprite;
            }
            else if (image.objectIndex == 5)//2ターン行動前
            {
                var turns = BattleManager.instance.activeBattlers.Where(x => !(x.currentHP == 0) && x.onAction == true);
                var turnBefore2 = turns.Skip(2)
                                   .FirstOrDefault();
                var turnBefore1 = turns.Skip(1)
                                   .FirstOrDefault();

                if (turnBefore1 == null)
                {
                    turns = BattleManager.instance.activeBattlers.Where(x => !(x.currentHP == 0));
                    turnBefore2 = turns.Skip(1)
                                   .FirstOrDefault(x => !(x.currentHP == 0));
                }
                else if(turnBefore2 == null)
                {
                    turns = BattleManager.instance.activeBattlers.Where(x => !(x.currentHP == 0));
                    turnBefore2 = turns.FirstOrDefault(x => !(x.currentHP == 0));
                }
                image.characterImage.sprite = turnBefore2.aliveSprite;

            }
            else if (image.objectIndex == 6)//3ターン行動前
            {
                var turnC = BattleManager.instance.activeBattlers.Where(x => x.currentHP != 0);
                var turns = BattleManager.instance.activeBattlers.Where(x => !(x.currentHP == 0) && x.onAction == true);
                var turnBefore3 = turns.Skip(3)
                                   .FirstOrDefault();
                var turnBefore2 = turns.Skip(2)
                                   .FirstOrDefault();
                var turnBefore1 = turns.Skip(1)
                                     .FirstOrDefault();

                if(turnBefore1 == null && turnC.Count() > 2)
                {
                    turns = BattleManager.instance.activeBattlers.Where(x => !(x.currentHP == 0));
                    turnBefore3 = turns.Skip(2)
                                   .FirstOrDefault(x => !(x.currentHP == 0));
                }
                else if(turnBefore2 == null)
                {
                    turns = BattleManager.instance.activeBattlers.Where(x => !(x.currentHP == 0));
                    turnBefore3 = turns.Skip(1)
                                   .FirstOrDefault(x => !(x.currentHP == 0));
                }
                else if (turnBefore3 == null)
                {
                    turns = BattleManager.instance.activeBattlers.Where(x => !(x.currentHP == 0));
                    turnBefore3 = turns.FirstOrDefault(x => !(x.currentHP == 0));
                }

                image.characterImage.sprite = turnBefore3.aliveSprite;

                if(turnC.Count() < 3)
                {
                    var turn = turns.SkipWhile(x => !(x.currentHP == 0) && x.onAction == true)
                                    .FirstOrDefault();
                    if(turn != null)
                    {
                        image.characterImage.sprite = turn.aliveSprite;
                    }
                    
                }


            }

        }

    }

    public void CurrentSpriteChange(RowCharacterUI image)//行動後の表示
    {
        if (rowCharacters[image.objectIndex].characterImage.sprite != null)
        {
            if (image.characterImage.sprite != null && !(rowCharacters[image.objectIndex - 1].characterImage.sprite == image.characterImage.sprite))
            {
                rowCharacters[image.objectIndex - 1].characterImage.sprite = image.characterImage.sprite;
                
            }
            image.characterImage.sprite = rowCharacters[image.objectIndex].characterImage.sprite;
        }
    }


}