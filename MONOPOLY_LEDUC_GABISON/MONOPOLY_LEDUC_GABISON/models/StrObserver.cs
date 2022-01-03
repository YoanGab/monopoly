using System;
class StrObserver : Observer
{
    private string str;

    public StrObserver(string str)
    {
        this.str = str;
    }

    public override void update()
    {
        Console.WriteLine(str);
    }
}
