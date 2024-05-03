using System;

public class Wallet
{
    private int _gold = 0;

    public int Gold
    {
        get => _gold;
        private set
        {
            _gold = value;
            GoldChanged?.Invoke(Gold);
        }
    }

    public event Action<int> GoldChanged;

    public Wallet()
    {
        GoldChanged?.Invoke(Gold);
    }

    public void AddGold(int value)
    {
        if (value <= 0)
            return;

        Gold += value;
    }

    public bool TakeGold(int value)
    {
        if (value <= 0)
            return false;

        if (Gold < value)
            return false;

        Gold -= value;

        return true;
    }
}
