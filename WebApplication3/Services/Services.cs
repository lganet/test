using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class Services : IServices
    {
        private Dictionary<string, Item> itemModel = new Dictionary<string, Item>();

        public Item AddItem(Item item)
        {
            if (item?.Name == null)
            {
                throw new ArgumentException("Item is invalid");
            }

            if (itemModel.ContainsKey(item.Name.ToLower())) 
            {
                throw new ArgumentException("Item already exist");
            }

            itemModel.Add(item.Name.ToLower(), item);

            return item;
        }

        public List<Item> GetItem(string item)
        {

            var result = new List<Item>();

            if (String.IsNullOrWhiteSpace(item))
            {
                return itemModel.Values.ToList();
            }

            if (! itemModel.ContainsKey(item.ToLower()))
            {
                return null;
            }

            result.Add(itemModel[item.ToLower()]);

            return result;
        }
    }
}
