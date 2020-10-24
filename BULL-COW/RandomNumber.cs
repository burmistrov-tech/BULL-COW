using System;
using System.Collections.Generic;
using System.Linq;

namespace BULL_COW
{
    class RandomNumber
    {
        public RandomNumber(int count)
        {
            Value = Generate(count);
        }

        public int[] Value { get; }

        public int Length => Value.Length;

        public (BULL_COW, int)[] CompareTo(int[] value)
        {
            if (Length != value.Length)
                throw new ArgumentOutOfRangeException($"The length of the values is {Length}. The length cannot be different");

            var result = new List<(BULL_COW, int)>();
            for (int i = 0; i < Length; i++)
            {
                if (Value[i] == value[i])
                {
                    result.Add((BULL_COW.BULL, value[i]));
                }
                else
                {
                    int idxOf = Array.IndexOf(Value, value[i]);
                    if (idxOf != -1)
                        result.Add((BULL_COW.COW, value[i]));
                }
            }

            return result.ToArray();
        }

        private int[] Generate(int count) =>
                Enumerable.Range(0, 10)
                          .OrderBy(x => Guid.NewGuid())
                          .SkipWhile(x => x == 0)
                          .Take(count)
                          .ToArray();
    }
    public enum BULL_COW
    {
        BULL,
        COW
    }
}
