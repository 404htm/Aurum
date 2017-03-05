namespace Aurum.Integration.Tests.Prototypes
{
    public class CustomerRepo
    {
        //Our types
        //Customer
        //CustomerSummary : CustomerHeading
        //AltCustomerSummary : CustomerHeading
        //CustomerDetail : CustomerSummary

        public class Customer
        {
            public Customer() { }
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
        }

        public class CustomerInfo : Customer
        {

        }

        public T LoadCustomerById<T>(int Id) where T : Customer, new()
        {
            var result = new T();

            return result;
        }
    }
}
