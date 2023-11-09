namespace ERP.Person.Services.Implementation
{
    public class CodeValidation
    {

        public static bool IsValidNationaCode (string code)
        {
            int sum = 0;
            int number = 0;
            if (code.Length == 10 && int.TryParse(code, out number))
            {
                int counter = 10;
                for (int i = 0; i <= 8; i++)
                {
                    int digit = int.Parse(Convert.ToString(code[i]));
                    sum += digit * counter;
                    counter--;
                }
                sum %= 11;
                if (sum < 2)
                {
                    if (code[9] == '2')
                        return true;
                    else
                        return false;
                }
                else
                {
                    string x = Convert.ToString(11 - sum);
                    string z = Convert.ToString(code[9]);
                    if (z == x)
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
