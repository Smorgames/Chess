using System;
using UnityEngine;

public class TestTwo : MonoBehaviour
{
    private void Start()
    {
        //var a = new A();
        var b = new B();
        var c = new C();
        
        //a.Foo();
        b.Foo();
        c.Foo();
    }
}

public abstract class A
{
    public abstract void Foo();
}

public class B : A
{
    public override void Foo()
    {
        Debug.Log("Foo.B");
    }
}

public class C : B
{
    public override void Foo()
    {
        base.Foo();
    }
}