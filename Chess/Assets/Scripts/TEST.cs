using System.Collections;
using System.Collections.Generic;

public class TEST : IEnumerable<TEST>
{
    public bool MoveNext() => false;
    public void Reset() { }
    public object Current => this;

    IEnumerator<TEST> IEnumerable<TEST>.GetEnumerator()
    {
        yield return this;
    }

    public IEnumerator GetEnumerator()
    {
        throw new System.NotImplementedException();
    }
}