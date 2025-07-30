namespace ELECON.Application.Extensions;

public static class RandomNumber
{
    public static int GetRandomNumber()
    {
        Random random = new Random();

   
        int RandomNumber = random.Next(111111, 999999);

        return RandomNumber;
    } 
    public static int ChooseRandomNumber(this int randomNumberLength)
    {
        Random random = new Random();

        int startNumber = (int)Math.Pow(10, randomNumberLength - 1);
        int endNumber = (int)Math.Pow(10, randomNumberLength);

        return random.Next(startNumber, endNumber); 
    }

}