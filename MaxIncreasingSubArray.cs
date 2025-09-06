namespace pusula_may;

public class MaxIncreasingSubArray
{
    public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
    {
        if (numbers.Count == 0)
        {
            return "[]";
        }
        List<int> maxSubArray = new List<int>();
        List<int> currentSubArray = new List<int> { numbers[0] };
        
        for (int i = 1; i < numbers.Count; i++)
        {
            if (numbers[i] > numbers[i - 1])
            {
                currentSubArray.Add(numbers[i]);
            }
            else
            {
                if (currentSubArray.Count > maxSubArray.Count)
                {
                    maxSubArray = new List<int>(currentSubArray);
                }
                currentSubArray = new List<int> { numbers[i] };
            }
        }
        if (currentSubArray.Count > maxSubArray.Count)
        {
            maxSubArray = currentSubArray;
        }
        return System.Text.Json.JsonSerializer.Serialize(maxSubArray);
    }
}