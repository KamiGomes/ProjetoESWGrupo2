using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stripe;
using ESW_Shelter.Models;


namespace ESW_Shelter.Libs
{
    public class StripeLib
    {

        async public Task<string> CreateCustomer(Users user)
        {

            CustomerCreateOptions customerOpts = new CustomerCreateOptions
            {
                Email = user.Email
            };

            Stripe.CustomerService customerService = new Stripe.CustomerService();
            Stripe.Customer newCustomer = await customerService.CreateAsync(customerOpts);

            return newCustomer.Id;
        }

        public Stripe.Plan[] GetPlans()
        {
            var service = new Stripe.PlanService();
            var options = new Stripe.PlanListOptions { };

            return service.List(options).ToArray<Stripe.Plan>();
        }

        public string Subscribe(string customerId, string planId)
        {

            try
            {
                var items = new List<SubscriptionItemOption>
                {
                    new SubscriptionItemOption
                    {
                        PlanId = planId,
                        Quantity = 1
                    }
                };

                var options = new SubscriptionCreateOptions
                {
                    CustomerId = customerId,
                    Items = items
                };

                Console.WriteLine(options.ToString());


                var service = new SubscriptionService();
                Subscription subscription = service.Create(options);

                return subscription.Id;
            } catch (Exception ex)
            {
                Console.WriteLine("########################################################################");
                Console.WriteLine("########################################################################");
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Message);
                Console.WriteLine("########################################################################");
                Console.WriteLine("########################################################################");
                return "false";
            }
        }

        public Stripe.StripeList<Stripe.Card> GetCards(string customerId)
        {
            Stripe.CardService service = new CardService();
            Stripe.StripeList<Stripe.Card> cards = service.List(customerId);

            return cards;
        }

        public void SetCard(string customerId, string number, int month, int year, string cvc)
        {
            Stripe.TokenService tokenService = new Stripe.TokenService();
            var options = new Stripe.TokenCreateOptions()
            { 
                Card = new CreditCardOptions()
                {
                    Number = number,
                    ExpMonth = month,
                    ExpYear = year,
                    Cvc = cvc
                }
            };

            Stripe.Token token = tokenService.Create(options);
                
            var customerOptions = new CustomerUpdateOptions() {
                SourceToken = token.Id
            };

            var customerService = new CustomerService();
            Stripe.Customer customer = customerService.Update(customerId, customerOptions);
        }
    }
}
