using System;
class StrObserver : Observer
{
    private readonly string message;

    public StrObserver(string message)
    {
        this.message = message;
    }

    public override void Update()
    {
        Console.WriteLine(message);
    }
}
