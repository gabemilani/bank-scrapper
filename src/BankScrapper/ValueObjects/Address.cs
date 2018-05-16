using BankScrapper.Utils;
using System;
using System.Text;

namespace BankScrapper.ValueObjects
{
    public class Address
    {
        public string City { get; set; }

        public string Country { get; set; }

        public string Neighborhood { get; set; }

        public string Number { get; set; }

        public string State { get; set; }

        public string Street { get; set; }

        public string Postcode { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            Action<string, string> append = (value, separator) =>
            {
                if (!value.IsNullOrEmpty())
                {
                    if (sb.Length > 0)
                        sb.Append(separator);

                    sb.Append(value);
                }
            };

            append(Street, ", ");
            append(Number, ", ");
            append(Neighborhood, ", ");
            append(City, ", ");
            append(State, " - ");
            append(Country, "/");
            append(Postcode, " - ");

            return sb.ToString();
        }

    }
}