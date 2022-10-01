public static class ShuffleList
{
    public static T[] Shuffle<T>(this T[] arr)
    {
        var result = arr.ToArray();
        var rng = Random.Shared;

        int n = result.Length;

        while (n > 1)
        {
            n--;

            var k = rng.Next(n + 1);
            var value = result[k];
            
            result[k] = result[n];
            result[n] = value;
        }

        return result;
    }
}