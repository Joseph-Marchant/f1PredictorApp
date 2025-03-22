namespace F1PredictionTracker.Services;

public class UserGetService(PredictionValidationService predictionValidationService)
{
    public string GetUser()
    {
        var name = string.Empty;
        while (name == string.Empty)
        {
            name = this.GetInput("Name: ", "Please enter a name: ");
            if (predictionValidationService.ValidateUser(name))
            {
                break;
            }
            
            if (this.ConfirmAddNewUser(name))
            {
                break;
            }

            name = string.Empty;
        }

        return name;
    }
    
    private string GetInput(string inputPrompt, string nullResponse)
    {
        var input = string.Empty;
        Console.Write(inputPrompt);
        while (string.IsNullOrWhiteSpace(input))
        {
            input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine(nullResponse);
            }
        }

        return input;
    }

    private bool ConfirmAddNewUser(string name)
    {
        var repsonse = string.Empty;
        var prompt = $"Add new user {name}? Y/N: ";
        while (string.IsNullOrWhiteSpace(repsonse))
        {
            repsonse = this.GetInput(prompt, prompt);
            if (repsonse.ToUpper() != "Y" && repsonse.ToUpper() != "N")
            {
                repsonse = string.Empty;
            }
        }
        
        return repsonse.ToUpper() == "Y";
    }
}
