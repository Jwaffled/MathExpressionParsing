using System.Collections;
using System.Collections.Generic;

namespace Tests;

public class EvaluationTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
        {
            "5 ^ 2 + 1",
            26
        };

        yield return new object[]
        {
            "(-(3!) * 2 ^ 2) - (2! * 3)",
            -30
        };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}