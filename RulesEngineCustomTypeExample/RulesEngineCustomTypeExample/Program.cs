using Newtonsoft.Json.Converters;
using RulesEngine.Models;
using RulesEngineCustomTypeExample;

var rules = new List<Rule>();
for (int i = 1; i <= 10; i++)
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

var employees = new List<Employee>();

for (int i = 1; i <= 20; i++)
{
    var employee = new Employee() { Name = $"Employee{i}", Age = i, Salary = i * 10 };
    employees.Add(employee);
}

Workflow[] workflowArray = { workflow };
var rulesEngine = new RulesEngine.RulesEngine(workflowArray);


foreach (var employee in employees)
{
    var ruleParam = new RuleParameter(nameof(employee), employee);
    var resultList = await rulesEngine.ExecuteAllRulesAsync("EmployeeWorkflow", ruleParam);
    var successResult = resultList.FirstOrDefault(r => r.IsSuccess);
    // successResult always null here
    if (successResult != null)
    {
        Console.WriteLine(successResult.Rule.RuleName);
    }
}