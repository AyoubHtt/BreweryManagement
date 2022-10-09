using Xunit.Abstractions;
using Xunit.Sdk;

namespace FunctionalTests.TestPriorityOrder;

public class PriorityOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        string assemblyName = typeof(TestPriorityAttribute).AssemblyQualifiedName!;

        var sortedMethods = new SortedDictionary<int, List<TTestCase>>();

        foreach (TTestCase testCase in testCases)
        {
            int priority = testCase.TestMethod
                                .Method
                                .GetCustomAttributes(assemblyName)
                                .FirstOrDefault()?
                                .GetNamedArgument<int>(nameof(TestPriorityAttribute.Priority)) ?? default;

            GetOrCreate(sortedMethods, priority).Add(testCase);
        }

        IEnumerable<TTestCase> orderedTestCases = sortedMethods.Keys.SelectMany(priority => sortedMethods[priority].OrderBy(testCase => testCase.TestMethod.Method.Name));

        foreach (TTestCase testCase in orderedTestCases)
        {
            yield return testCase;
        }
    }

    private static TValue GetOrCreate<TKey, TValue>(IDictionary<TKey, TValue> dictionary, TKey key)
        where TKey : struct
        where TValue : new()
    {
        if (dictionary.TryGetValue(key, out TValue? result)) return result;

        dictionary[key] = new TValue();

        return dictionary[key];
    }
}