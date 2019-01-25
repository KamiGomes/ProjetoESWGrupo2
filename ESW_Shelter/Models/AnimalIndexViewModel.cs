using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESW_Shelter.Models
{
    public class AnimalIndexViewModel
    {

        public List<Animal> Animals;
        public List<Images> Pictures;
        public List<Product> Products;
        public List<AnimalRace> AnimalRaces;
        public List<AnimalType> AnimalTypes;
        public List<Users> UsersNames;
        public List<AnimalProduct> AnimalProduct;
        public List<AnimalUsers> AnimalUsers;
        public string AnimalRace { get; set; }
        public string AnimalType { get; set; }
        public string ProductType { get; set; }
        public string SearchString { get; set; }
    }
}
