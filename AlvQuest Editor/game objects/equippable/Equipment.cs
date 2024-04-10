using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlvQuest_Editor
{
    /// <summary>
    /// Предмет снаряжения, который можно одеть в соответсвующий ему слот
    /// </summary>
    public class Equipment
    {
        #region _____________________ПОЛЯ_____________________
        //название предмета
        public string Name { get; private set; }
        //Часть тела, на которую можно надеть предмет
        public EBodyPart BodyPart { get; private set; }
        //список эффектов, реализующий действие снаряжения
        public List<BaseEffect> Effects { get; set; } = [];
        #endregion

        #region _____________________КОНСТРУКТОР_____________________

        /// <summary>
        /// Стандартный конструктор предмета снаряжения
        /// </summary>
        /// <param name="bodyPart">Часть тела, на которую можно надеть предмет</param>
        /// <param name="name">Название предмета</param>
        public Equipment(string name, EBodyPart bodyPart, List<BaseEffect> effects = null)
        {
            Name = name;
            BodyPart = bodyPart;
            Effects = effects ?? [];
        }
        #endregion

        #region _____________________МЕТОДЫ_____________________
        public Equipment Clone()
        {
            List<BaseEffect> cloneEffects = [];
            foreach (var originalEffect in Effects)
            {
                cloneEffects.Add(originalEffect.Clone());
            }

            return new Equipment(
                Name,
                BodyPart,
                cloneEffects
                );
        }
        #endregion
    }
}
