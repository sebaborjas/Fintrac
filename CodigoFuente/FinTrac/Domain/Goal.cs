using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Exceptions;

namespace Domain
{
    public class Goal
    {
        public int Id { get; set; }

        private string _title;
        private double _maxAmount;
        private Workspace _workspace;
        public string Token { get; set; }

        public Goal()
        {
            Categories = new List<Category>();
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value == "")
                {
                    throw new EmptyFieldException();
                }
                _title = value;
            }
        }

        public double MaxAmount
        {
            get
            {
                return _maxAmount;
            }

            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException();
                }
                _maxAmount = value;
            }
        }

        public List<Category> Categories { get; set; }



        public Workspace Workspace
        {
            get
            {
                return _workspace;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                _workspace = value;
            }
        }

        public override bool Equals(object? obj)
        {
            Goal goal = (Goal)obj;
            return this.Title == goal.Title;
        }
    }
}
