using System;
using System.Diagnostics.CodeAnalysis;

namespace FSyncLib
{

    /// <summary>
    ///     Размерность периода
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum Measure
    {
        /// <summary>
        ///     Секунды (1000 миллисекунд)
        /// </summary>
        s = 1000,

        /// <summary>
        ///     Минуты
        /// </summary>
        m = 60 * s,

        /// <summary>
        ///     Часы
        /// </summary>
        h = 60 * m,

        /// <summary>
        ///     Дни
        /// </summary>
        d = 24 * h
    }

    public static class StringHelper
    {
        /// <summary>
        ///     Строка в размерность
        /// </summary>
        /// <param name="str"> Строка </param>
        /// <returns> Размерность </returns>
        public static Measure ToMeasure(this string str)
        {
            bool parseResult = Enum.TryParse(str, out Measure measure);
            var result = parseResult ? measure : Measure.s;
            return result;
        }

        /// <summary>
        ///     Строка в тип операции
        /// </summary>
        /// <param name="str"> Строка </param>
        /// <returns> Размерность </returns>
        public static OperationType ToOperationType(this string str)
        {
            bool parseResult = Enum.TryParse(str, out OperationType operationType);
            var result = parseResult ? operationType : OperationType.Copy;
            return result;
        }
    }

    /// <summary>
    ///     Тип операции
    /// </summary>
    public enum OperationType
    {
        /// <summary>
        ///     Копирование
        /// </summary>
        Copy,
        /// <summary>
        ///     Жёсткая ссылка
        /// </summary>
        HardLink
    }

}
