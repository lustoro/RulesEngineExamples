using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using RulesEngine.Models;
using RulesEnginePerformanceExample;
using System.Diagnostics;
using System.Dynamic;
using System.Linq.Dynamic.Core;


var rules = new List<Rule>();
const int numberOfRules = 70000;

for (int i = 0; i <= numberOfRules; i++)
{
    var rule = new Rule
    {
        RuleExpressionType = RuleExpressionType.LambdaExpression,
        RuleName = $"Rule{i}",
        Expression = $"employee.Name == \"Employee{i}\" AND employee.Age = {i}"
    };
    rules.Add(rule);
}


var workflow = new Workflow()
{
    WorkflowName = "EmployeeWorkflow",
    RuleExpressionType = RuleExpressionType.LambdaExpression,
    Rules = rules
};


var converter = new ExpandoObjectConverter();
var employees = new List<ExpandoObject>();

const int numOfFacts = 6000;

for (int i = 0; i <= numOfFacts; i++)
{
    var employee = new Employee() { Name = $"Employee{i}", Age = i, Salary = 1 * 10 };
    var employeeJson = JsonConvert.DeserializeObject<ExpandoObject>(employee.ToString(), converter);
    employees.Add(employeeJson);
}

Workflow[] workflowArray = { workflow };
var sw = new Stopwatch();
sw.Start();
var rulesEngine = new RulesEngine.RulesEngine(workflowArray);

foreach (var employee in employees)
{
    var ruleParam = new RuleParameter(nameof(employee), employee);
    var resultList = await rulesEngine.ExecuteAllRulesAsync("EmployeeWorkflow", ruleParam);
    var successResult = resultList.FirstOrDefault(r => r.IsSuccess);
    if (successResult != null)
    {
        Console.WriteLine(successResult.Rule.RuleName);
    }
}
sw.Stop();
Console.WriteLine(sw.Elapsed);

