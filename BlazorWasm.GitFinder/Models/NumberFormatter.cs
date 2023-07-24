namespace BlazorWasm.GitFinder.Models;

public static class NumberFormatter
{
    public static string NumberToKilo(this int number)
    {
        string numStr = number.ToString();
        if (numStr.Length <= 3)
        {
            return numStr;
        }
        else if (numStr.Length >= 4 && numStr.Length <= 5)
        {
            return $"{numStr.Substring(0, numStr.Length - 3)}.{numStr.Substring(numStr.Length - 3, 1)}k";
        }
        else if (numStr.Length == 6)
        {
            return $"{numStr.Substring(0, numStr.Length - 3)}k";
        }
        else
        {
            return $"{numStr.Substring(0, numStr.Length - 6)}M";
        }
    }
}