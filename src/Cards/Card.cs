using System;
using System.Security.Cryptography; 
using System.Text;
namespace lilium.src.Cards
{
    public class Card
    {
        const int YEAR_ACTUAL = 2021;
        #region field of class
        private string _number;
        private int _nip;
        private string _nameClient;
        private string _lastnameClient;
        private string _brand;
        private string _country;
        private int _expiresMonth;
        private int _expiresYear;
        private string _funding;
        private string _lastDigits;
        private string _fingerPrint;
        #endregion

        #region constructors
        Card(string nameClient, string lastnameClient, string country, int brandOption, int fundingOption)
        {
            _nameClient = nameClient;
            _lastnameClient = lastnameClient;
            _country = country; 
            _brand = BrandCard(brandOption);
            _number = GenerateCardNumber(_brand);
            _fingerPrint = GenerateFingerprint(_number);
            _funding = SetFunding(fundingOption);
            _nip = 9999;
            _lastDigits = Last4Digits(_number, 4);
        }
        #endregion

        #region methods
        private string GenerateCardNumber(string brand)
        {
            var randomGenerator = new Random();
            int prefix, block_1, block_2, block_3;
            string accountNumber;
            switch(brand)
            {
                case "American Express":
                    prefix = randomGenerator.Next(34, 38);
                    break;
                case "Mastercard":
                    prefix = randomGenerator.Next(51, 56);
                    break;
                case "Visa":
                    prefix = randomGenerator.Next(4539, 4917);
                    break;
                default:
                    Console.WriteLine($"Your brand {brand} is not suppported by Lilium ATM Network.");
                    prefix = 0;
                    break;
            }
            block_1 = randomGenerator.Next(1000,10_000);
            block_2 = randomGenerator.Next(1000,10_000);
            block_3 = randomGenerator.Next(1000,10_000);
            _expiresYear = randomGenerator.Next(YEAR_ACTUAL, YEAR_ACTUAL+3);
            _expiresMonth = randomGenerator.Next(1,13);
            return accountNumber = prefix.ToString() + block_1.ToString() + block_2.ToString() + block_3.ToString();
        }
        private string BrandCard(int option)
        {
            string brandChoose;
            switch(option)
            {
                case 1:
                    brandChoose = "American Express";
                    break;
                case 2:
                    brandChoose = "Mastercard";
                    break;
                case 3:
                    brandChoose = "Visa";
                    break;
                default:
                    Console.WriteLine("The card brand is nor supported by Lilium ATM Network");
                    brandChoose = "Unknown";
                    break;
            }
            return brandChoose;
        }

        private string GenerateFingerprint(string numberCard)
        {
            string fingerprintCard;
            using(SHA256 sha256Hash = SHA256.Create())
            {
                fingerprintCard = GetHash(sha256Hash, numberCard);
            }
            return fingerprintCard;
        }
        private static string GetHash(HashAlgorithm algorithm, string input)
        {
            // convert the input string to a byte array and compute hash
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
            //Create a new string builder to collect the bytes and and create a string
            var sBuilder = new StringBuilder();
            //Loop through each byte of the hashed data and format it as a hexadecimal
            for(int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            //return hexadeicmal string
            return sBuilder.ToString();
            
        }
        private static bool VerifyHash(HashAlgorithm algorithm, string input, string hash)
        {
            //Hash the input
            var hashOfInput = GetHash(algorithm, input);
            //Create a Stringcomparer and compare hashes
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }
        private string SetFunding(int fundingOpt)
        {
            string fundingSet;
            switch(fundingOpt)
            {
                case 1:
                    fundingSet = "credit";
                    break;
                case 2: 
                    fundingSet = "debit";
                    break;
                default:
                    fundingSet = "unknown";
                    Console.WriteLine($"Your option {fundingOpt} doesn't exist in Lilium ATM System");
                    break;
            }
            return fundingSet;
        }
        private string Last4Digits(string source, int tail_lenght)
        {
            if(tail_lenght >= source.Length)
                return source;
            return source.Substring(source.Length-tail_lenght);
        }
        #endregion

        #region propiertes
        public  string Number
        {
            get => _number;
            set => _number = value;
        }
        public int Nip
        {
            get => _nip;
            set => _nip = value;
        }
        public string NameClient
        {
            get => _nameClient;
            set => _nameClient = value;
        }
        public string LastnameClient 
        {
            get => _lastnameClient;
            set => _lastnameClient = value;
        }
        public string Brand 
        {
            get => _brand;
        }
        public void SetBrand(int brandOption)
        {
            _brand = BrandCard(brandOption);
        }
        public string Country
        {
            get => _country;
            set => _country = value;
        }
        public int ExpiresMonth
        {
            get => _expiresMonth;
            set => _expiresMonth = value;
        }
        public int ExpiresYear
        {
            get => _expiresYear;
            set => _expiresYear = value;
        }
        public string Funding
        {
            get => _funding;
        }
        public void SetFunging(int fundingOption)
        {
            _funding = SetFunding(fundingOption);
        }
        public string LastDigits
        {
            get => _lastDigits;
        }
        public void SetLastDigits(string number)
        {
            _lastDigits = Last4Digits(number, 4);
        }
        public string FingerPrint
        {
            get => _fingerPrint;
        }
        public void SetFingerPrint(string cardNumber)
        {
            var sha256Hash = SHA256.Create();
            _fingerPrint = GetHash(sha256Hash, cardNumber);
        }
        #endregion
        
    }
}