using DataLayer.API;
using DataLayer.Implementations;
using DataLayer.Implementations.Events;

namespace Tests
{
    public class RandomFiller : IDataFiller
    {
        public void Fill(IDataContext context)
        {
            Random random = new Random();
            int insertionsCount = random.Next(10, 100);

            for (int i = 0; i < insertionsCount; i++)
            {
                IUser user = new User(GetRandomString(6), GetRandomString(10), GetRandomEmail(), GetRandomNumber<double>(9), GetRandomPhoneNumber(), null);
                IProduct product = new Book(GetRandomString(6), GetRandomNumber<double>(2), GetRandomString(6) + " " + GetRandomString(10), GetRandomString(6) + " " + GetRandomString(10), GetRandomNumber<int>(3), GetRandomDate());
                IState state = new State(product, GetRandomNumber<int>(2) + 1);

                context.Users.Add(user);
                context.Products.Add(product);
                context.States.Add(state);

                double happening = random.NextDouble();

                try
                {
                    if (happening <= 0.5)
                    {
                        context.Events.Add(new Delivery(user, state, GetRandomNumber<int>(2) + 1));
                    }

                    if (happening <= 0.75)
                    {
                        context.Events.Add(new Borrow(user, state));

                        if (happening <= 0.5)
                        {
                            context.Events.Add(new Return(user, state));
                        }
                    }
                }
                catch (Exception)
                {
                    // throw new Exception("Error", e);
                }
            }
        }

        public static string GetRandomString(int lengthOfRandomString)
        {
            Random random = new Random();

            // const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string chars = "abcdefghijklmnopqrstuvwxyz";

            char[] randomCharsSet = new char[lengthOfRandomString];

            for (int i = 0; i < lengthOfRandomString; i++)
            {
                randomCharsSet[i] = chars[random.Next(chars.Length)];
            }

            return new string(randomCharsSet);
        }

        public static string GetRandomEmail()
        {
            return $"{GetRandomString(6)}@{GetRandomString(6)}.com";
        }

        public static T GetRandomNumber<T>(int lengthOfRandomNumber)
        {
            if (lengthOfRandomNumber < 1)
            {
                throw new ArgumentException("Length of random number must be greater than 0");
            }

            Random random = new Random();

            dynamic maxValue = Math.Pow(10, lengthOfRandomNumber) - 1;

            dynamic randomNumber = random.Next(0, (int)maxValue);

            return (T)Convert.ChangeType(randomNumber, typeof(T));
        }

        public static int GetRandomPhoneNumber()
        {
            return new Random().Next(100000000, 1000000000); // 9-digit random number
        }

        public static DateTime GetRandomDate()
        {
            Random random = new Random();
            DateTime start = new DateTime(1900, 1, 1);

            int range = (DateTime.Today - start).Days;

            int randomDays = random.Next(range);
            int randomHours = random.Next(24);
            int randomMinutes = random.Next(60);
            int randomSeconds = random.Next(60);

            return start.AddDays(randomDays).AddHours(randomHours).AddMinutes(randomMinutes).AddSeconds(randomSeconds);
        }
    }
}