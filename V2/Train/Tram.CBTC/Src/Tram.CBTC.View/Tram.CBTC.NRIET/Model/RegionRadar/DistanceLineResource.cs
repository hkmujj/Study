using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism;

namespace Tram.CBTC.NRIET.Model.RegionRadar
{
    public class DistanceLineResource
    {
        public static readonly DistanceLineResource Instance = new DistanceLineResource();

        private DistanceLineResource()
        {
            ItemCollection = new ObservableCollection<DistanceLineItem>()
            {
            };
            
            //总共可用高度：404，总距离：200m，平均每10m: 404 / 200 /10 = 20.2
            ItemCollection.Add(new DistanceLineItem() { Location = 0, Lenght = 12, Text = "200", Value = 200 });
            ItemCollection.Add(new DistanceLineItem() {Location = 101, Lenght = 12, Text = "150", Value = 150});
            ItemCollection.Add(new DistanceLineItem() { Location = 202, Lenght = 12, Text = "100", Value = 100 });
            ItemCollection.Add(new DistanceLineItem() { Location = 242.4, Lenght = 5, Text = "80", Value = 80 });
            ItemCollection.Add(new DistanceLineItem() { Location = 282.8, Lenght = 5, Text = "60", Value = 60 });
            ItemCollection.Add(new DistanceLineItem() { Location = 303, Lenght = 10, Text = "50", Value = 50 });
            ItemCollection.Add(new DistanceLineItem() { Location = 323.2, Lenght = 5, Text = "40", Value = 40 });
            ItemCollection.Add(new DistanceLineItem() { Location = 343.4, Lenght = 5, Text = "30", Value = 30 });
            ItemCollection.Add(new DistanceLineItem() { Location = 363.6, Lenght = 5, Text = "20", Value = 20 });
            ItemCollection.Add(new DistanceLineItem() { Location = 383.8, Lenght = 5, Text = "10", Value = 10 });
            ItemCollection.Add(new DistanceLineItem() {Location = 404, Lenght = 5, Text = "0m", Value = 0});


            var order = ItemCollection.OrderByDescending(o => o.Value).ToList();
            ItemCollection.Clear();
            ItemCollection.AddRange(order);
           
        }

        

        

        public ObservableCollection<DistanceLineItem> ItemCollection { private set; get; }
    }
}