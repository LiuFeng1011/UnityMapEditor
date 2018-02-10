using System;

public struct AHInt
{
    private static int cryptoKey = 963852;

    private int currentCryptoKey;
    private int hiddenValue;

    public int v
    {
        get { return InternalEncryptDecrypt(); }
        set { currentCryptoKey = cryptoKey; hiddenValue = EncryptDecrypt(value); }
    }

    public AHInt(int value)
    {
        currentCryptoKey = cryptoKey;
        hiddenValue = EncryptDecrypt(value);
    }

    public static void SetNewCryptoKey(int newKey)
    {
        cryptoKey = newKey;
    }

    public int GetEncrypted()
    {
        if (currentCryptoKey != cryptoKey)
        {
            hiddenValue = InternalEncryptDecrypt();
            hiddenValue = EncryptDecrypt(hiddenValue, cryptoKey);
            currentCryptoKey = cryptoKey;
        }
        return hiddenValue;
    }

    public void SetEncrypted(int encrypted)
    {
        hiddenValue = encrypted;
    }

    public static int EncryptDecrypt(int value)
    {
        return EncryptDecrypt(value, 0);
    }

    public static int EncryptDecrypt(int value, int key)
    {
        if (key == 0)
        {
            return value ^ cryptoKey;
        }
        else
        {
            return value ^ key;
        }
    }

    private int InternalEncryptDecrypt()
    {
        int key = cryptoKey;

        if (currentCryptoKey != cryptoKey)
        {
            key = currentCryptoKey;
        }

        return EncryptDecrypt(hiddenValue, key);
    }


// Operators

    public static implicit operator AHInt(int value)
    {
        return new AHInt(value);
    }

    public static implicit operator int(AHInt value)
    {
        return value.v;
    }

    public static AHInt operator ++ (AHInt input)
    {
        input.hiddenValue = EncryptDecrypt(input.InternalEncryptDecrypt() + 1);
        return input;
    }

    public static AHInt operator -- (AHInt input)
    {
        input.hiddenValue = EncryptDecrypt(input.InternalEncryptDecrypt() - 1);
        return input;
    }

    public static AHInt operator + (AHInt a, AHInt b) { return new AHInt(a.v + b.v); }
    public static AHInt operator - (AHInt a, AHInt b) { return new AHInt(a.v - b.v); }
    public static AHInt operator * (AHInt a, AHInt b) { return new AHInt(a.v * b.v); }
    public static AHInt operator / (AHInt a, AHInt b) { return new AHInt(a.v / b.v); }

    public static AHInt operator + (AHInt a, int b) { return new AHInt(a.v + b); }
    public static AHInt operator - (AHInt a, int b) { return new AHInt(a.v - b); }
    public static AHInt operator * (AHInt a, int b) { return new AHInt(a.v * b); }
    public static AHInt operator / (AHInt a, int b) { return new AHInt(a.v / b); }

    public static AHInt operator + (int a, AHInt b) { return new AHInt(a + b.v); }
    public static AHInt operator - (int a, AHInt b) { return new AHInt(a - b.v); }
    public static AHInt operator * (int a, AHInt b) { return new AHInt(a * b.v); }
    public static AHInt operator / (int a, AHInt b) { return new AHInt(a / b.v); }

    public static int operator * (AHInt a, float b) { return (int)(a.v * b); } //@

    public static bool operator <  (AHInt a, AHInt b) { return a.v <  b.v; }
    public static bool operator <= (AHInt a, AHInt b) { return a.v <= b.v; }
    public static bool operator >  (AHInt a, AHInt b) { return a.v >  b.v; }
    public static bool operator >= (AHInt a, AHInt b) { return a.v >= b.v; }

    public static bool operator <  (AHInt a, int b) { return a.v <  b; }
    public static bool operator <= (AHInt a, int b) { return a.v <= b; }
    public static bool operator >  (AHInt a, int b) { return a.v >  b; }
    public static bool operator >= (AHInt a, int b) { return a.v >= b; }

    public override bool Equals(object o)
    {
        if (o is AHInt)
        {
            AHInt b = (AHInt)o;
            return this.v == b.v;
        }
        else
        {
            return false;
        }
    }

    public bool Equals(AHInt b)
    {
        return (object)b != null && v == b.v;
    }

    public override int GetHashCode()
    {
        return v.GetHashCode();
    }

    public override string ToString()
    {
        return InternalEncryptDecrypt().ToString();
    }

    public string ToString(string format)
    {
        return InternalEncryptDecrypt().ToString(format);
    }

#if !UNITY_FLASH
    public string ToString(IFormatProvider provider)
    {
        return InternalEncryptDecrypt().ToString(provider);
    }

    public string ToString(string format, IFormatProvider provider)
    {
        return InternalEncryptDecrypt().ToString(format, provider);
    }
#endif
}
