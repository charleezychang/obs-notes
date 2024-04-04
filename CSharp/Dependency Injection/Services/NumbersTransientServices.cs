public class NumbersTransientService : INumbersTransientService
{
    private int _number = new Random().Next(0, 100);

    public int GetNumber()
    {
        return _number;
    }
}