using System.Collections.Generic;


namespace ESW_Shelter.Models
{
    public class ProductIndexViewModel
    {
        public List<Product> Products;
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList AnimalTypes;
        public Microsoft.AspNetCore.Mvc.Rendering.SelectList ProductTypes;
        public string AnimalType { get; set; }
        public string ProductType { get; set; }
        public string SearchString { get; set; }
    }
}
