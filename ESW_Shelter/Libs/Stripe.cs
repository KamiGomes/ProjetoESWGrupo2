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
        async public void CreateCustomer(Models.Users user)
        {
            Dictionary<string, string> metadata = new Dictionary<string, string>() {
                { "userId", user.UserID.ToString()}
            };

            CustomerCreateOptions customerOpts = new CustomerCreateOptions
            {
                Email = user.Email,
                Metadata = metadata
            };

            Stripe.CustomerService customerService = new Stripe.CustomerService();
            Stripe.Customer newCustomer = await customerService.CreateAsync(customerOpts);
        }

    }
}
