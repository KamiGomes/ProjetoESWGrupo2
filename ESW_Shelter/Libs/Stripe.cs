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

    }
}
