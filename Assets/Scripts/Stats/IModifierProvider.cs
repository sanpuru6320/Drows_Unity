using System.Collections.Generic;

namespace RPG.Stats
{
    public interface IModifierProvider //追加ステータス効果の装備、アイテムなどに使用
    {
        IEnumerable<float> GetAdditiveModifiers(Stat stat);
        IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}