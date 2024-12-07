using Microsoft.AspNetCore.Mvc;

public class CalculatorController : Controller
{
    // Главная страница с выбором методов
    public IActionResult Index()
    {
        return View();
    }

    // 1. Manual Parsing (Single Action)
    public IActionResult ManualParsingSingle()
    {
        return View();
    }
    [HttpPost]
    public IActionResult ManualParsingSingle(string number1, string number2, string operation)
    {
        double n1, n2;
        var model = new CalcModel();

        if (double.TryParse(number1, out n1) && double.TryParse(number2, out n2))
        {
            model.Number1 = n1;
            model.Number2 = n2;
            model.Operation = operation;

            try
            {
                model.Result = PerformOperation(n1, n2, operation);
            }
            catch (Exception ex)
            {
                model.ErrorMessage = ex.Message;
            }
        }
        else
        {
            model.ErrorMessage = "Invalid input!";
        }

        return View("Result", model);
    }

    // 2. Manual Parsing (Separate Action)
    [HttpGet]
    public IActionResult ManualParsingSeparate()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ManualParsingSeparate(double number1, double number2, string operation)
    {
        var model = new CalcModel
        {
            Number1 = number1,
            Number2 = number2,
            Operation = operation
        };

        try
        {
            model.Result = PerformOperation(number1, number2, operation);
        }
        catch (Exception ex)
        {
            model.ErrorMessage = ex.Message;
        }

        return View("Result", model);
    }

    // 3. Model Binding (Parameters)
    [HttpGet]
    public IActionResult ModelBindingParams()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ModelBindingParams(double number1, double number2, string operation)
    {
        var model = new CalcModel
        {
            Number1 = number1,
            Number2 = number2,
            Operation = operation
        };

        try
        {
            model.Result = PerformOperation(number1, number2, operation);
        }
        catch (Exception ex)
        {
            model.ErrorMessage = ex.Message;
        }

        return View("Result", model);
    }

    // 4. Model Binding (Separate Model)
    [HttpGet]
    public IActionResult ModelBindingModel()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ModelBindingModel(CalcModel model)
    {
        try
        {
            model.Result = PerformOperation(model.Number1, model.Number2, model.Operation);
        }
        catch (Exception ex)
        {
            model.ErrorMessage = ex.Message;
        }

        return View("Result", model);
    }

    // Вспомогательный метод для выполнения операций
    private double PerformOperation(double number1, double number2, string operation)
    {
        return operation switch
        {
            "+" => number1 + number2,
            "-" => number1 - number2,
            "*" => number1 * number2,
            "/" => number2 == 0 ? throw new DivideByZeroException("Cannot divide by zero!") : number1 / number2,
            _ => throw new InvalidOperationException("Invalid operation!")
        };
    }
}