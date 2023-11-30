using PizzaParlor.Data;
using System;
using System.ComponentModel;

namespace PizzaParlor.PointOfSale
{
    public class PaymentViewModel : INotifyPropertyChanged
    {
        private Order order;
        private decimal amountGiven;

        public PaymentViewModel(Order order)
        {
            this.order = order;
            this.amountGiven = order.Total; // Initialize the amountGiven with the total order cost
        }

        public decimal Subtotal => order.Subtotal;
        public decimal Tax => order.Tax;
        public decimal Total => order.Total;

        public decimal AmountGiven
        {
            get => amountGiven;
            set
            {
                if (value >= Total)
                {
                    amountGiven = value;
                    OnPropertyChanged(nameof(AmountGiven));
                    OnPropertyChanged(nameof(Change));
                }
            }
        }

        public decimal Change => AmountGiven - Total;

        public string Receipt
        {
            get
            {
                return $"Order Summary:\n" +
                       $"Subtotal: ${Subtotal:N2}\n" +
                       $"Tax: ${Tax:N2}\n" +
                       $"Total: ${Total:N2}\n" +
                       $"Paid: ${AmountGiven:N2}\n" +
                       $"Change: ${Change:N2}";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
