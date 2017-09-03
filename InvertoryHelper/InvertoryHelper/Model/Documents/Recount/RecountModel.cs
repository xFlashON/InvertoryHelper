using System;
using System.Collections.ObjectModel;
using System.Linq;
using InvertoryHelper.Common;

namespace InvertoryHelper.Model.Documents.Recount
{
    public class RecountModel : ObservableObject
    {
        private readonly Recount _recount;

        public RecountModel(Recount recount = null)
        {
            if (recount == null)
            {
                _recount = new Recount();
                RecountRows = new ObservableCollection<RecountRowModel>();
                Date = DateTime.Now;
            }
            else
            {
                _recount = recount;
                RecountRows =
                    new ObservableCollection<RecountRowModel>(_recount.RecountRows.Select(r => new RecountRowModel(r)));
            }
        }

        public Guid Uid
        {
            get => _recount.Uid;
            set
            {
                _recount.Uid = value;
                OnPropertyChanged("Uid");
            }
        }

        public DateTime Date
        {
            get => _recount.Date;
            set
            {
                _recount.Date = value;
                OnPropertyChanged("Date");
            }
        }

        public decimal Number
        {
            get => _recount.Number;
            set
            {
                _recount.Number = value;
                OnPropertyChanged("Number");
            }
        }

        public decimal Total
        {
            get => _recount.Total;
            set
            {
                _recount.Total = value;
                OnPropertyChanged("Total");
            }
        }

        public string Comment
        {
            get => _recount.Comment;
            set
            {
                _recount.Comment = value;
                OnPropertyChanged("Comment");
            }
        }

        public ObservableCollection<RecountRowModel> RecountRows { get; set; }

        public Recount GetRecount()
        {
            _recount.RecountRows = RecountRows.Select(r => new RecountRow
                {
                    Nomenclature = r.Nomenclature,
                    Characteristic = r.Characteristic,
                    Price = r.Price,
                    Amount = r.Amount,
                    Total = r.Total,
                    Uid = r.Uid,
                    Recount = _recount
                })
                .ToList();

            _recount.Total = _recount.RecountRows.Sum(r => r.Total);

            return _recount;
        }
    }
}