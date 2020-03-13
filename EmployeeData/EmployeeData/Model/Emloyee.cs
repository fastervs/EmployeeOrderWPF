using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EmployeeData.Models
{
    class Emloyee: DependencyObject
    {
        public string Name
        {
            get { return (string)GetValue(Namepr); }
            set { SetValue(Namepr, value); }
        }

        public static readonly DependencyProperty Namepr =
            DependencyProperty.Register("Name", typeof(string), typeof(Emloyee), new PropertyMetadata(""));

        public string Department
        {
            get { return (string)GetValue(Departmentpr); }
            set { SetValue(Departmentpr, value); }
        }

        public static readonly DependencyProperty Departmentpr =
            DependencyProperty.Register("Department", typeof(string), typeof(Emloyee), new PropertyMetadata(""));

        public string Gender
        {
            get { return (string)GetValue(Genderpr); }
            set { SetValue(Genderpr, value); }
        }

        public static readonly DependencyProperty Genderpr =
            DependencyProperty.Register("Gender", typeof(string), typeof(Emloyee), new PropertyMetadata(""));
    }
}
